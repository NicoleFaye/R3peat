using System;
using System.Collections.Generic;
using System.Threading;
using WindowsInput;

namespace R3peat
{
    class MouseMovement : IAction
    {

        private InputSimulator Input;
        private List<MouseMovementStep> MouseMovementSteps;
        private readonly ICoordinateConversion CoordinateConversion = new WPFCoordinateConversion();
        public void Run()
        {
            foreach (MouseMovementStep mouseMovementStep in this.MouseMovementSteps)
            {
                ushort DestinationAbsoluteX = mouseMovementStep.GetDestinationAbsoluteX();
                ushort DestinationAbsoluteY = mouseMovementStep.GetDestinationAbsoluteY();


                int variance = mouseMovementStep.GetVariance();
                if (variance > ushort.MaxValue) variance = ushort.MaxValue;
                else if (variance < 0) variance = 0;

                //Account for variance
                Random random = new Random();
                int deltaX = Convert.ToUInt16(random.Next(variance * -1, variance));
                int deltaY = Convert.ToUInt16(random.Next(variance * -1, variance));

                ushort AbsoluteYPixelStepSize = CoordinateConversion.GetAbsoluteYPixelStepSize();
                ushort AbsoluteXPixelStepSize = CoordinateConversion.GetAbsoluteXPixelStepSize();

                //convert deltas from pixels to absolute value
                int finalDeltaX = deltaX * (int)AbsoluteXPixelStepSize;
                int finalDeltaY = deltaY * (int)AbsoluteYPixelStepSize;

                if (checkForOverflow(DestinationAbsoluteX, finalDeltaX))
                {
                    if (finalDeltaX < 0) DestinationAbsoluteX = 0;
                    else DestinationAbsoluteX = ushort.MaxValue;
                }
                else
                {
                    DestinationAbsoluteX = (ushort)((int)DestinationAbsoluteX + finalDeltaX);
                }

                if (checkForOverflow(DestinationAbsoluteY, finalDeltaY))
                {
                    if (finalDeltaY < 0) DestinationAbsoluteY = 0;
                    else DestinationAbsoluteY = ushort.MaxValue;
                }
                else
                {
                    DestinationAbsoluteY = (ushort)((int)DestinationAbsoluteY + finalDeltaY);
                }

                this.Input.Mouse.MoveMouseTo(DestinationAbsoluteX, DestinationAbsoluteY);
                Thread.Sleep(mouseMovementStep.GetPauseMillisecondDuration());
            }
        }
        public MouseMovement(InputSimulator Input, List<MouseMovementStep> Steps)
        {
            this.Input = Input;
            this.MouseMovementSteps = Steps;
        }

        private bool checkForOverflow(ushort startingValue, int delta)
        {
            bool willOverflow = false;
            if (startingValue > 0 && delta > 0)
            {
                if (delta > (ushort.MaxValue - startingValue)) willOverflow = true;
            }

            if (delta + (int)startingValue < 0)
            {
                willOverflow = true;
            }
            return willOverflow;
        }
    }
}
