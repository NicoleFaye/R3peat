using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;

namespace R3peat
{
    public class KeyCombination : Action
    {
        private InputSimulator _inputSimulator;
        public List<VirtualKeyCode> KeyCodes { get; set; }

        public KeyCombination(InputSimulator inputSimulator, string name, List<VirtualKeyCode> keyCodes)
        {
            _inputSimulator = inputSimulator;
            Name = name;
            KeyCodes = keyCodes;
        }

        public override void Run()
        {
            _inputSimulator.Keyboard.ModifiedKeyStroke(KeyCodes.Take(KeyCodes.Count - 1), KeyCodes.Last());
        }
    }

}
