namespace R3peat
{
    public interface ICoordinateConversion
    {
        ushort PixelYToAbsoluteY(int PixelY);

        ushort PixelXToAbsoluteX(int PixelX);

        ushort GetAbsoluteXPixelStepSize();

        ushort GetAbsoluteYPixelStepSize();
    }
}
