using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace R3peat
{
    public class MouseClick : Action
    {
        private InputSimulator _inputSimulator;
        public MouseButton MouseButton { get; set; }

        public MouseClick(InputSimulator inputSimulator, string name, MouseButton mouseButton)
        {
            _inputSimulator = inputSimulator;
            Name = name;
            MouseButton = mouseButton;
        }

        public override void Run()
        {
            switch (MouseButton)
            {
                case MouseButton.Left:
                    _inputSimulator.Mouse.LeftButtonClick();
                    break;
                case MouseButton.Right:
                    _inputSimulator.Mouse.RightButtonClick();
                    break;
                default:
                    break;
            }
        }
    }

    public enum MouseButton
    {
        Left,
        Right,
    }

}
