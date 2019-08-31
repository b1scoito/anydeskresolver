using System;
using System.Drawing;

namespace anydeskresolver
{
    class Logger
    {
        static string logger_name = "discordresolver";

        static string get_time()
        {
            return DateTime.Now.ToString("h:mm:ss");
        }

        public Color hex(string hex)
        {
            return Color.FromArgb(Convert.ToInt32(hex.Replace("#", ""), 16));
        }

        static void print(string format, Color color)
        {
            Colorful.Console.Write(format, color);
        }

        public void info(string format)
        {
            print("[ ", Color.White);
            print(get_time(), hex("#15dfff"));
            print(" ] ", Color.White);
            print(logger_name, Color.Magenta);
            print(" INFO", hex("#00ff00"));
            print(" -> ", Color.White);
            print(format, Color.White);
            print("\n", Color.White);
        }

        public void debug(string format)
        {
            print("[ ", Color.White);
            print(get_time(), hex("#15dfff"));
            print(" ] ", Color.White);
            print(logger_name, Color.Magenta);
            print(" DEBUG", hex("#ffff00"));
            print(" -> ", Color.White);
            print(format, Color.White);
            print("\n", Color.White);
        }

        public void error(string format)
        {
            print("[ ", Color.White);
            print(get_time(), hex("#15dfff"));
            print(" ] ", Color.White);
            print(logger_name, Color.Magenta);
            print(" ERROR", hex("#ff5555"));
            print(" -> ", Color.White);
            print(format, Color.White);
            print("\n", Color.White);
        }

        public void custom(string format, string custom, Color custom_color)
        {
            print("[ ", Color.White);
            print(get_time(), hex("#15dfff"));
            print(" ] ", Color.White);
            print(logger_name, Color.Magenta);
            print($" {custom}", custom_color);
            print(" -> ", Color.White);
            print(format, Color.White);
            print("\n", Color.White);
        }
    }
}
