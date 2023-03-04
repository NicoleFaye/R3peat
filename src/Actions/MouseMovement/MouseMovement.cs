using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using WindowsInput;

namespace R3peat
{
    public class MouseMovement : Action
    {
        private InputSimulator Input;
        public ObservableCollection<MouseMovementStep> MouseMovementSteps { get; set; }
        private readonly ICoordinateConversion CoordinateConversion = new WPFCoordinateConversion();
        public NameIncrementer MouseMovementStepNameIncrementer;
        
        override public void Run()
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

                double AbsoluteYPixelStepSize = CoordinateConversion.GetAbsoluteYPixelStepSize();
                double AbsoluteXPixelStepSize = CoordinateConversion.GetAbsoluteXPixelStepSize();

                //convert deltas from pixels to absolute value
                int finalDeltaX = (int)((double)deltaX * AbsoluteXPixelStepSize);
                int finalDeltaY = (int)((double)deltaY * AbsoluteYPixelStepSize);

                if (CheckForOverflow(DestinationAbsoluteX, finalDeltaX))
                {
                    if (finalDeltaX < 0) DestinationAbsoluteX = 0;
                    else DestinationAbsoluteX = ushort.MaxValue;
                }
                else
                {
                    DestinationAbsoluteX = (ushort)((int)DestinationAbsoluteX + finalDeltaX);
                }

                if (CheckForOverflow(DestinationAbsoluteY, finalDeltaY))
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
        public MouseMovement(InputSimulator Input,String Name, ObservableCollection<MouseMovementStep> Steps)
        {
            this.Name = Name;
            this.Input = Input;
            this.MouseMovementSteps = Steps;
            this.MouseMovementStepNameIncrementer = new NameIncrementer("Mouse Movement Step");
        }

        private bool CheckForOverflow(ushort startingValue, int delta)
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
