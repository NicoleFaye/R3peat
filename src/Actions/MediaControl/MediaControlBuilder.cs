using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace R3peat
{

    public class MediaControlBuilder
    {
        private InputSimulator _inputSimulator;
        private string _name;
        private MediaControlType _controlType;
        private NameIncrementer NameIncrementer;

        public MediaControlBuilder(InputSimulator inputSimulator)
        {
            _inputSimulator = inputSimulator;
            NameIncrementer = new NameIncrementer("Media Control");
        }

        public MediaControlBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public MediaControlBuilder SetControlType(MediaControlType controlType)
        {
            _controlType = controlType;
            return this;
        }

        public MediaControl Build()
        {
            return new MediaControl(_inputSimulator, _name??NameIncrementer.Next(), _controlType);
        }
    }

}
