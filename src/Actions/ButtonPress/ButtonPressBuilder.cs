using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsInput;

namespace R3peat
{

    public class ButtonPressBuilder
    {
        private InputSimulator _inputSimulator;
        private string _name;
        private Key _key;
        private NameIncrementer NameIncrementer;

        public ButtonPressBuilder(InputSimulator inputSimulator)
        {
            _inputSimulator = inputSimulator;
            NameIncrementer = new NameIncrementer("Button Press");
        }

        public ButtonPressBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public ButtonPressBuilder SetKey(Key key)
        {
            _key = key;
            return this;
        }

        public ButtonPress Build()
        {
            return new ButtonPress(_inputSimulator, _name ?? NameIncrementer.Next(), _key);
        }
    }

}
