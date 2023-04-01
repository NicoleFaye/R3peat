using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace R3peat
{
    public class TextEntry : Action
    {
        private InputSimulator _inputSimulator;
        public string Text { get; set; }

        public TextEntry(InputSimulator inputSimulator, string name, string text)
        {
            _inputSimulator = inputSimulator;
            Name = name;
            Text = text;
        }

        public override void Run()
        {
            _inputSimulator.Keyboard.TextEntry(Text);
        }
    }
}
