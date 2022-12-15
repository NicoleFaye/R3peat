using System;
using System.Windows.Forms;

namespace R3peat
{
    public class CoordinateConversion:ICoordinateConversion
    {
        public ushort PixelYToAbsoluteY(int PixelY)
        {
            int minPixelY = SystemInformation.VirtualScreen.Y;
            //ensures that negative coordinates are accounted for
            int AdjustedPixelY = PixelY + (-1 * minPixelY);
            int screenHeight = SystemInformation.VirtualScreen.Height;
            if (PixelY > minPixelY + screenHeight || PixelY < minPixelY)
            {
                throw new ArgumentOutOfRangeException();
            }
            ushort AbsoluteY = (ushort)(((double)AdjustedPixelY) * (((double)ushort.MaxValue) / ((double)(screenHeight - 1))));
            return AbsoluteY;
        }

        public ushort PixelXToAbsoluteX(int PixelX)
        {
            int minPixelX = SystemInformation.VirtualScreen.X;
            //ensures that negative coordinates are accounted for
            int AdjustedPixelX = PixelX + (-1 * minPixelX);
            int screenWidth = SystemInformation.VirtualScreen.Width;
            if (PixelX > minPixelX + screenWidth || PixelX < minPixelX)
            {
                throw new ArgumentOutOfRangeException();
            }
            ushort AbsoluteX = (ushort)(((double)AdjustedPixelX) * (((double)ushort.MaxValue) / ((double)(screenWidth - 1))));
            return AbsoluteX;
        }

        public ushort GetAbsoluteXPixelStepSize()
        {
            int screenWidth = SystemInformation.VirtualScreen.Width;
            ushort AbsoluteX = (ushort)(((double)ushort.MaxValue) / ((double)(screenWidth - 1)));
            return AbsoluteX;
        }

        public ushort GetAbsoluteYPixelStepSize()
        {
            int screenHeight = SystemInformation.VirtualScreen.Height;
            ushort AbsoluteY = (ushort)(((double)ushort.MaxValue) / ((double)(screenHeight - 1)));
            return AbsoluteY;
        }
    }
}
