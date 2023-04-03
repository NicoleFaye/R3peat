using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    using WindowsInput;
    using WindowsInput.Native;

    public class MediaControl : Action
    {
        private InputSimulator _inputSimulator;
        public MediaControlType ControlType { get; set; }

        public MediaControl(InputSimulator inputSimulator, string name, MediaControlType controlType)
        {
            _inputSimulator = inputSimulator;
            Name = name;
            ControlType = controlType;
        }

        public override void Run()
        {
            VirtualKeyCode keyCode = GetKeyCodeForMediaControlType(ControlType);
            _inputSimulator.Keyboard.KeyPress(keyCode);
        }

        private VirtualKeyCode GetKeyCodeForMediaControlType(MediaControlType controlType)
        {
            switch (controlType)
            {
                case MediaControlType.PlayPause:
                    return VirtualKeyCode.MEDIA_PLAY_PAUSE;
                case MediaControlType.Stop:
                    return VirtualKeyCode.MEDIA_STOP;
                case MediaControlType.Next:
                    return VirtualKeyCode.MEDIA_NEXT_TRACK;
                case MediaControlType.Previous:
                    return VirtualKeyCode.MEDIA_PREV_TRACK;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum MediaControlType
    {
        PlayPause,
        Stop,
        Next,
        Previous,
    }

}
