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
        public ClickType ClickAction { get; set; }

        public MouseClick(InputSimulator inputSimulator, string name, MouseButton mouseButton, ClickType clickAction)
        {
            _inputSimulator = inputSimulator;
            Name = name;
            MouseButton = mouseButton;
            ClickAction = clickAction;
        }

        public override void Run()
        {
            switch (MouseButton)
            {
                case MouseButton.Left:
                    PerformLeftClickAction();
                    break;
                case MouseButton.Right:
                    PerformRightClickAction();
                    break;
                default:
                    break;
            }
        }

        private void PerformLeftClickAction()
        {
            switch (ClickAction)
            {
                case ClickType.Click:
                    _inputSimulator.Mouse.LeftButtonClick();
                    break;
                case ClickType.Down:
                    _inputSimulator.Mouse.LeftButtonDown();
                    break;
                case ClickType.Up:
                    _inputSimulator.Mouse.LeftButtonUp();
                    break;
                default:
                    break;
            }
        }

        private void PerformRightClickAction()
        {
            switch (ClickAction)
            {
                case ClickType.Click:
                    _inputSimulator.Mouse.RightButtonClick();
                    break;
                case ClickType.Down:
                    _inputSimulator.Mouse.RightButtonDown();
                    break;
                case ClickType.Up:
                    _inputSimulator.Mouse.RightButtonUp();
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

    public enum ClickType
    {
        Click,
        Down,
        Up,
    }


}
