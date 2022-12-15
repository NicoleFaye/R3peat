using System;
using System.Collections.Generic;
using System.Windows;

namespace R3peat
{
    public class WPFCoordinateConversion:ICoordinateConversion
    {
        public ushort PixelYToAbsoluteY(int PixelY)
        {
            int minPixelY = (int)SystemParameters.VirtualScreenTop;
            //ensures that negative coordinates are accounted for
            int AdjustedPixelY = PixelY + (-1 * minPixelY);
            int screenHeight = (int)SystemParameters.VirtualScreenHeight;
            if (PixelY > minPixelY + screenHeight || PixelY < minPixelY)
            {
                throw new ArgumentOutOfRangeException();
            }
            ushort AbsoluteY = (ushort)(((double)AdjustedPixelY) * (((double)ushort.MaxValue) / ((double)(screenHeight - 1))));
            return AbsoluteY;
        }

        public ushort PixelXToAbsoluteX(int PixelX)
        {
            int minPixelX = (int)SystemParameters.VirtualScreenLeft;
            //ensures that negative coordinates are accounted for
            int AdjustedPixelX = PixelX + (-1 * minPixelX);
            int screenWidth = (int)SystemParameters.VirtualScreenWidth;
            if (PixelX > minPixelX + screenWidth || PixelX < minPixelX)
            {
                throw new ArgumentOutOfRangeException();
            }
            ushort AbsoluteX = (ushort)(((double)AdjustedPixelX) * (((double)ushort.MaxValue) / ((double)(screenWidth - 1))));
            return AbsoluteX;
        }

        public ushort GetAbsoluteXPixelStepSize()
        {
            int screenWidth = (int)SystemParameters.VirtualScreenWidth;
            ushort AbsoluteX = (ushort)(((double)ushort.MaxValue) / ((double)(screenWidth - 1)));
            return AbsoluteX;
        }

        public ushort GetAbsoluteYPixelStepSize()
        {
            int screenHeight = (int)SystemParameters.VirtualScreenHeight;
            ushort AbsoluteY = (ushort)(((double)ushort.MaxValue) / ((double)(screenHeight - 1)));
            return AbsoluteY;
        }
    }
}
