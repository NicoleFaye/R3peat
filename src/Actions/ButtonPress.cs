using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsInput.Native;
using WindowsInput;

namespace R3peat
{
    public class ButtonPress : Action
    {
        private InputSimulator _inputSimulator;
        public VirtualKeyCode KeyCode { get; set; }

        public ButtonPress(InputSimulator inputSimulator, string name, Key key)
        {
            _inputSimulator = inputSimulator;
            Name = name;
            KeyCode = (VirtualKeyCode)KeyInterop.VirtualKeyFromKey(key);
        }

        public override void Run()
        {
            _inputSimulator.Keyboard.KeyPress(KeyCode);
        }
    }
}
