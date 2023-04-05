using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace R3peat
{
    public class Screenshot : Action
    {
        public string SavePath { get; set; }

        public Screenshot(string name, string savePath = "" )
        {
            Name = name;
            SavePath = savePath;
        }

        public override void Run()
        {
            using (Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(0, 0, 0, 0, bmp.Size);
                }

                    bmp.Save(SavePath, ImageFormat.Png);
            }
        }
    }
}
