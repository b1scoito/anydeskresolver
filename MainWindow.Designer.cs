namespace anydeskresolver
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbCtx2 = new System.Windows.Forms.Label();
            this.btnResolve = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnCopy = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txtIPAddr = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.pnTop = new System.Windows.Forms.Panel();
            this.lbTitle2 = new System.Windows.Forms.Label();
            this.lbTitle1 = new System.Windows.Forms.Label();
            this.pnctx = new System.Windows.Forms.Panel();
            this.lbinfo = new System.Windows.Forms.Label();
            this.pnGeoIP = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.btnLOC = new MaterialSkin.Controls.MaterialRaisedButton();
            this.pnGeoIP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // lbCtx2
            // 
            this.lbCtx2.AutoSize = true;
            this.lbCtx2.ForeColor = System.Drawing.Color.Gray;
            this.lbCtx2.Location = new System.Drawing.Point(242, 48);
            this.lbCtx2.Name = "lbCtx2";
            this.lbCtx2.Size = new System.Drawing.Size(173, 56);
            this.lbCtx2.TabIndex = 26;
            this.lbCtx2.Text = "Needs AnyDesk opened \r\nwith connection Incoming.\r\n(If Victim has Direct Connectio" +
    "ns\r\n Turned Off this tool won\'t work.)";
            this.lbCtx2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // btnResolve
            // 
            this.btnResolve.AutoSize = true;
            this.btnResolve.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnResolve.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResolve.Depth = 0;
            this.btnResolve.Icon = null;
            this.btnResolve.Location = new System.Drawing.Point(341, 166);
            this.btnResolve.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Primary = true;
            this.btnResolve.Size = new System.Drawing.Size(79, 36);
            this.btnResolve.TabIndex = 24;
            this.btnResolve.Text = "Resolve";
            this.btnResolve.UseVisualStyleBackColor = true;
            this.btnResolve.Click += new System.EventHandler(this.BtnResolve_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.AutoSize = true;
            this.btnCopy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopy.Depth = 0;
            this.btnCopy.Enabled = false;
            this.btnCopy.Icon = null;
            this.btnCopy.Location = new System.Drawing.Point(11, 86);
            this.btnCopy.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Primary = true;
            this.btnCopy.Size = new System.Drawing.Size(155, 36);
            this.btnCopy.TabIndex = 25;
            this.btnCopy.Text = "Copy to Clipboard";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // txtIPAddr
            // 
            this.txtIPAddr.Depth = 0;
            this.txtIPAddr.Enabled = false;
            this.txtIPAddr.Hint = "";
            this.txtIPAddr.Location = new System.Drawing.Point(11, 57);
            this.txtIPAddr.MaxLength = 32767;
            this.txtIPAddr.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtIPAddr.Name = "txtIPAddr";
            this.txtIPAddr.PasswordChar = '\0';
            this.txtIPAddr.SelectedText = "";
            this.txtIPAddr.SelectionLength = 0;
            this.txtIPAddr.SelectionStart = 0;
            this.txtIPAddr.Size = new System.Drawing.Size(155, 23);
            this.txtIPAddr.TabIndex = 23;
            this.txtIPAddr.TabStop = false;
            this.txtIPAddr.Text = "0.0.0.0";
            this.txtIPAddr.UseSystemPasswordChar = false;
            // 
            // pnTop
            // 
            this.pnTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(66)))), ((int)(((byte)(54)))));
            this.pnTop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnTop.Location = new System.Drawing.Point(11, 5);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(65, 5);
            this.pnTop.TabIndex = 22;
            // 
            // lbTitle2
            // 
            this.lbTitle2.AutoSize = true;
            this.lbTitle2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(66)))), ((int)(((byte)(54)))));
            this.lbTitle2.Location = new System.Drawing.Point(7, 28);
            this.lbTitle2.Name = "lbTitle2";
            this.lbTitle2.Size = new System.Drawing.Size(69, 20);
            this.lbTitle2.TabIndex = 21;
            this.lbTitle2.Text = "Resolver";
            // 
            // lbTitle1
            // 
            this.lbTitle1.AutoSize = true;
            this.lbTitle1.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle1.ForeColor = System.Drawing.Color.White;
            this.lbTitle1.Location = new System.Drawing.Point(8, 12);
            this.lbTitle1.Name = "lbTitle1";
            this.lbTitle1.Size = new System.Drawing.Size(68, 19);
            this.lbTitle1.TabIndex = 20;
            this.lbTitle1.Text = "AnyDesk";
            // 
            // pnctx
            // 
            this.pnctx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(66)))), ((int)(((byte)(54)))));
            this.pnctx.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnctx.Location = new System.Drawing.Point(3, 3);
            this.pnctx.Name = "pnctx";
            this.pnctx.Size = new System.Drawing.Size(5, 77);
            this.pnctx.TabIndex = 2;
            // 
            // lbinfo
            // 
            this.lbinfo.AutoSize = true;
            this.lbinfo.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbinfo.ForeColor = System.Drawing.Color.White;
            this.lbinfo.Location = new System.Drawing.Point(13, 7);
            this.lbinfo.Name = "lbinfo";
            this.lbinfo.Size = new System.Drawing.Size(98, 15);
            this.lbinfo.TabIndex = 6;
            this.lbinfo.Text = "Not Resolved Yet";
            // 
            // pnGeoIP
            // 
            this.pnGeoIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.pnGeoIP.Controls.Add(this.pnctx);
            this.pnGeoIP.Controls.Add(this.lbinfo);
            this.pnGeoIP.Location = new System.Drawing.Point(12, 127);
            this.pnGeoIP.Name = "pnGeoIP";
            this.pnGeoIP.Size = new System.Drawing.Size(278, 75);
            this.pnGeoIP.TabIndex = 27;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = global::anydeskresolver.Properties.Resources.fechar;
            this.btnClose.Location = new System.Drawing.Point(396, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 25);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 28;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnLOC
            // 
            this.btnLOC.AutoSize = true;
            this.btnLOC.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLOC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLOC.Depth = 0;
            this.btnLOC.Icon = null;
            this.btnLOC.Location = new System.Drawing.Point(172, 86);
            this.btnLOC.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLOC.Name = "btnLOC";
            this.btnLOC.Primary = true;
            this.btnLOC.Size = new System.Drawing.Size(47, 36);
            this.btnLOC.TabIndex = 29;
            this.btnLOC.Text = "loc";
            this.btnLOC.UseVisualStyleBackColor = true;
            this.btnLOC.Visible = false;
            this.btnLOC.Click += new System.EventHandler(this.BtnLOC_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ClientSize = new System.Drawing.Size(432, 210);
            this.Controls.Add(this.btnLOC);
            this.Controls.Add(this.lbCtx2);
            this.Controls.Add(this.btnResolve);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtIPAddr);
            this.Controls.Add(this.pnTop);
            this.Controls.Add(this.lbTitle2);
            this.Controls.Add(this.lbTitle1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnGeoIP);
            this.Font = new System.Drawing.Font("Roboto", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Discord Resolver";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.pnGeoIP.ResumeLayout(false);
            this.pnGeoIP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbCtx2;
        private MaterialSkin.Controls.MaterialRaisedButton btnResolve;
        private MaterialSkin.Controls.MaterialRaisedButton btnCopy;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtIPAddr;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Label lbTitle2;
        private System.Windows.Forms.Label lbTitle1;
        private System.Windows.Forms.Panel pnctx;
        private System.Windows.Forms.Label lbinfo;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.Panel pnGeoIP;
        private MaterialSkin.Controls.MaterialRaisedButton btnLOC;
    }
}

