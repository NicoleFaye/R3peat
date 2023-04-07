using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace R3peat
{

    public class KeyCombinationBuilder
    {
        private InputSimulator _inputSimulator;
        private string _name;
        private List<VirtualKeyCode> _keyCodes;
        private NameIncrementer NameIncrementer;

        public KeyCombinationBuilder(InputSimulator inputSimulator)
        {
            _inputSimulator = inputSimulator;
            _keyCodes = new List<VirtualKeyCode>();
            NameIncrementer = new NameIncrementer("KeyCombination");
        }

        public KeyCombinationBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public KeyCombinationBuilder AddKey(VirtualKeyCode keyCode)
        {
            _keyCodes.Add(keyCode);
            return this;
        }

        public KeyCombinationBuilder ClearKeys()
        {
            _keyCodes.Clear();
            return this;
        }

        public KeyCombination Build()
        {
            string finalName = _name ?? NameIncrementer.Next();
            return new KeyCombination(_inputSimulator, finalName, new List<VirtualKeyCode>(_keyCodes));
        }
    }
}
