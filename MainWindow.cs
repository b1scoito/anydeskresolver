using MaterialSkin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace anydeskresolver
{
    public partial class MainWindow : Form
    {
        #region Shadow
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        private const int WM_NCLBUTTONDBLCLK = 0x00A3;
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private bool m_aeroEnabled;
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled) cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS() { bottomHeight = 1, leftWidth = 0, rightWidth = 0, topHeight = 0 };
                        DwmExtendFrameIntoClientArea(Handle, ref margins);
                    }
                    break;
                default: break;
            }
            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                m.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion
        static Logger log = new Logger();
        public static string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Proxy = null;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            log.info("Initialize Component");
            CheckForIllegalCrossThreadCalls = false;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red700, Primary.Red700, Primary.Red700, Accent.Red700, TextShade.WHITE);
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void BtnResolve_Click(object sender, EventArgs e)
        {
            Console.Clear();
            txtIPAddr.Text = "0.0.0.0";
            lbispname.Text = "Not Resolved Yet";
            Resolve();
        }
        private void BtnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtIPAddr.Text);
        }
        public void Resolve()
        {
            Network.TcpActiveConnections = Network.GetAllTcpConnections();
            foreach (TcpProcessRecord c in Network.TcpActiveConnections)
            {
                if (c.LocalAddress.ToString() != "127.0.0.1" && c.State == MibTcpState.ESTABLISHED)
                {
                    if (c.ProcessName == "AnyDesk")
                    {
                        var ipinfo = JsonConvert.DeserializeObject<ipinfo>(Get($"https://ipinfo.io/{c.RemoteAddress}/json"));
                        log.custom($"{c.LocalAddress}:{c.LocalPort} <-> {c.RemoteAddress}:{c.RemotePort} S: {c.State} PID: {c.ProcessId} PN: {c.ProcessName} ORG: {ipinfo.org}", "ANYDESK", log.hex("#FC4236"));
                        if (!ipinfo.org.ToLower().Contains("google llc") && !ipinfo.org.ToLower().Contains("cloudflare, inc.") && !ipinfo.org.ToLower().Contains("amazon.com, inc."))
                        {
                            log.debug($"IP Found: {c.RemoteAddress} CITY: {ipinfo.city} REG: {ipinfo.region} COUNTRY: {ipinfo.country} ORG: {ipinfo.org}");
                            txtIPAddr.Text = c.RemoteAddress.ToString();
                            btnCopy.Enabled = true;
                            lbispname.Text = $"{ipinfo.org} ({ipinfo.country})";
                        }
                    }
                }
            }
        }
    }
    public class ipinfo
    {
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string loc { get; set; }
        public string org { get; set; }
    }
    public static class Network
    {
        private const int AF_INET = 2;
        public static List<TcpProcessRecord> TcpActiveConnections = null;
        public static List<UdpProcessRecord> UdpActiveConnections = null;

        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int pdwSize,
            bool bOrder, int ulAf, TcpTableClass tableClass, uint reserved = 0);
        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint GetExtendedUdpTable(IntPtr pUdpTable, ref int pdwSize,
            bool bOrder, int ulAf, UdpTableClass tableClass, uint reserved = 0);

        public static List<TcpProcessRecord> GetAllTcpConnections()
        {
            int bufferSize = 0;
            List<TcpProcessRecord> tcpTableRecords = new List<TcpProcessRecord>();

            uint result = GetExtendedTcpTable(IntPtr.Zero, ref bufferSize, true, AF_INET,
                TcpTableClass.TCP_TABLE_OWNER_PID_ALL);

            IntPtr tcpTableRecordsPtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                result = GetExtendedTcpTable(tcpTableRecordsPtr, ref bufferSize, true,
                    AF_INET, TcpTableClass.TCP_TABLE_OWNER_PID_ALL);

                if (result != 0)
                    return new List<TcpProcessRecord>();
                MIB_TCPTABLE_OWNER_PID tcpRecordsTable = (MIB_TCPTABLE_OWNER_PID)
                                        Marshal.PtrToStructure(tcpTableRecordsPtr,
                                        typeof(MIB_TCPTABLE_OWNER_PID));
                IntPtr tableRowPtr = (IntPtr)((long)tcpTableRecordsPtr +
                                        Marshal.SizeOf(tcpRecordsTable.dwNumEntries));
                for (int row = 0; row < tcpRecordsTable.dwNumEntries; row++)
                {
                    MIB_TCPROW_OWNER_PID tcpRow = (MIB_TCPROW_OWNER_PID)Marshal.
                        PtrToStructure(tableRowPtr, typeof(MIB_TCPROW_OWNER_PID));
                    tcpTableRecords.Add(new TcpProcessRecord(
                                          new IPAddress(tcpRow.localAddr),
                                          new IPAddress(tcpRow.remoteAddr),
                                          BitConverter.ToUInt16(new byte[2] {
                                              tcpRow.localPort[1],
                                              tcpRow.localPort[0] }, 0),
                                          BitConverter.ToUInt16(new byte[2] {
                                              tcpRow.remotePort[1],
                                              tcpRow.remotePort[0] }, 0),
                                          tcpRow.owningPid, tcpRow.state));
                    tableRowPtr = (IntPtr)((long)tableRowPtr + Marshal.SizeOf(tcpRow));
                }
            }
            catch (OutOfMemoryException outOfMemoryException)
            {
                MessageBox.Show(outOfMemoryException.Message, "Out Of Memory",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Exception",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Marshal.FreeHGlobal(tcpTableRecordsPtr);
            }
            return tcpTableRecords != null ? tcpTableRecords.Distinct()
                .ToList<TcpProcessRecord>() : new List<TcpProcessRecord>();
        }
        public static List<UdpProcessRecord> GetAllUdpConnections()
        {
            int bufferSize = 0;
            List<UdpProcessRecord> udpTableRecords = new List<UdpProcessRecord>();

            uint result = GetExtendedUdpTable(IntPtr.Zero, ref bufferSize, true,
                AF_INET, UdpTableClass.UDP_TABLE_OWNER_PID);

            IntPtr udpTableRecordPtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                result = GetExtendedUdpTable(udpTableRecordPtr, ref bufferSize, true,
                    AF_INET, UdpTableClass.UDP_TABLE_OWNER_PID);

                if (result != 0)
                    return new List<UdpProcessRecord>();

                MIB_UDPTABLE_OWNER_PID udpRecordsTable = (MIB_UDPTABLE_OWNER_PID)
                    Marshal.PtrToStructure(udpTableRecordPtr, typeof(MIB_UDPTABLE_OWNER_PID));
                IntPtr tableRowPtr = (IntPtr)((long)udpTableRecordPtr +
                    Marshal.SizeOf(udpRecordsTable.dwNumEntries));

                for (int i = 0; i < udpRecordsTable.dwNumEntries; i++)
                {
                    MIB_UDPROW_OWNER_PID udpRow = (MIB_UDPROW_OWNER_PID)
                        Marshal.PtrToStructure(tableRowPtr, typeof(MIB_UDPROW_OWNER_PID));
                    udpTableRecords.Add(new UdpProcessRecord(new IPAddress(udpRow.localAddr), new IPAddress(udpRow.remoteAddr), BitConverter.ToUInt16(new byte[2] { udpRow.localPort[1], udpRow.localPort[0] }, 0), BitConverter.ToUInt16(new byte[2] { udpRow.remotePort[1], udpRow.remotePort[0] }, 0), udpRow.owningPid));
                    tableRowPtr = (IntPtr)((long)tableRowPtr + Marshal.SizeOf(udpRow));
                }
            }
            catch (OutOfMemoryException outOfMemoryException)
            {
                MessageBox.Show(outOfMemoryException.Message, "Out Of Memory",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Exception",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Marshal.FreeHGlobal(udpTableRecordPtr);
            }
            return udpTableRecords != null ? udpTableRecords.Distinct()
                .ToList<UdpProcessRecord>() : new List<UdpProcessRecord>();
        }
    }
    public enum Protocol
    {
        TCP,
        UDP
    }
    public enum TcpTableClass
    {
        TCP_TABLE_BASIC_LISTENER,
        TCP_TABLE_BASIC_CONNECTIONS,
        TCP_TABLE_BASIC_ALL,
        TCP_TABLE_OWNER_PID_LISTENER,
        TCP_TABLE_OWNER_PID_CONNECTIONS,
        TCP_TABLE_OWNER_PID_ALL,
        TCP_TABLE_OWNER_MODULE_LISTENER,
        TCP_TABLE_OWNER_MODULE_CONNECTIONS,
        TCP_TABLE_OWNER_MODULE_ALL
    }
    public enum UdpTableClass
    {
        UDP_TABLE_BASIC,
        UDP_TABLE_OWNER_PID,
        UDP_TABLE_OWNER_MODULE
    }
    public enum MibTcpState
    {
        CLOSED = 1,
        LISTENING = 2,
        SYN_SENT = 3,
        SYN_RCVD = 4,
        ESTABLISHED = 5,
        FIN_WAIT1 = 6,
        FIN_WAIT2 = 7,
        CLOSE_WAIT = 8,
        CLOSING = 9,
        LAST_ACK = 10,
        TIME_WAIT = 11,
        DELETE_TCB = 12,
        NONE = 0
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_TCPROW_OWNER_PID
    {
        public MibTcpState state;
        public uint localAddr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] localPort;
        public uint remoteAddr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] remotePort;
        public int owningPid;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_TCPTABLE_OWNER_PID
    {
        public uint dwNumEntries;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct,
            SizeConst = 1)]
        public MIB_TCPROW_OWNER_PID[] table;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDPROW_OWNER_PID
    {
        public uint localAddr;
        public uint remoteAddr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] localPort;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] remotePort;
        public int owningPid;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDPTABLE_OWNER_PID
    {
        public uint dwNumEntries;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct,
            SizeConst = 1)]
        public UdpProcessRecord[] table;
    }
    [StructLayout(LayoutKind.Sequential)]
    public class TcpProcessRecord
    {
        [DisplayName("Local Address")]
        public IPAddress LocalAddress { get; set; }
        [DisplayName("Local Port")]
        public ushort LocalPort { get; set; }
        [DisplayName("Remote Address")]
        public IPAddress RemoteAddress { get; set; }
        [DisplayName("Remote Port")]
        public ushort RemotePort { get; set; }
        [DisplayName("State")]
        public MibTcpState State { get; set; }
        [DisplayName("Process ID")]
        public int ProcessId { get; set; }
        [DisplayName("Process Name")]
        public string ProcessName { get; set; }

        public TcpProcessRecord(IPAddress localIp, IPAddress remoteIp, ushort localPort,
            ushort remotePort, int pId, MibTcpState state)
        {
            LocalAddress = localIp;
            RemoteAddress = remoteIp;
            LocalPort = localPort;
            RemotePort = remotePort;
            State = state;
            ProcessId = pId;
            if (Process.GetProcesses().Any(process => process.Id == pId))
            {
                ProcessName = Process.GetProcessById(ProcessId).ProcessName;
            }
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public class UdpProcessRecord
    {
        [DisplayName("Local Address")]
        public IPAddress LocalAddress { get; set; }
        [DisplayName("Remote Address")]
        public IPAddress RemoteAddress { get; set; }
        [DisplayName("Local Port")]
        public uint LocalPort { get; set; }
        [DisplayName("Remote Port")]
        public uint RemotePort { get; set; }
        [DisplayName("Process ID")]
        public int ProcessId { get; set; }
        [DisplayName("Process Name")]
        public string ProcessName { get; set; }

        public UdpProcessRecord(IPAddress localAddress, IPAddress remoteAddress, uint localPort, uint remotePort, int pId)
        {
            LocalAddress = localAddress;
            RemoteAddress = remoteAddress;
            LocalPort = localPort;
            RemotePort = remotePort;
            ProcessId = pId;
            if (Process.GetProcesses().Any(process => process.Id == pId))
                ProcessName = Process.GetProcessById(ProcessId).ProcessName;
        }
    }
}
