using R3peat;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using WindowsInput.Native;
using WindowsInput;
using System.Threading;

namespace R3peat
{
    public class Hotkey : INotifyPropertyChanged, IDisposable
    {
        private readonly IntPtr _hWnd;
        private readonly int _id;
        private string _hotkeyString;
        private HotkeyMode _hotkeyMode;
        private Key _key;
        private ModifierKeys _modifierKeys;
        private bool _isRegistered;
        public ObservableCollection<Action> Actions { get; set; } = new ObservableCollection<Action>();
        private bool _active;
        private Thread _loopThread;
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
                if (_active)
                {
                    this.Register();
                }
                else
                {
                    this.Unregister();
                }
                OnPropertyChanged();
            }
        }

        public Key Key
        {
            get => _key;
            set
            {
                if (_key != value)
                {
                    _key = value;
                    OnPropertyChanged();
                    UpdateHotkeyString();
                }
            }
        }

        public ModifierKeys ModifierKeys
        {
            get => _modifierKeys;
            set
            {
                if (_modifierKeys != value)
                {
                    _modifierKeys = value;
                    OnPropertyChanged();
                    UpdateHotkeyString();
                }
            }
        }

        public bool IsRegistered
        {
            get => _isRegistered;
            private set
            {
                if (_isRegistered != value)
                {
                    _isRegistered = value;
                    OnPropertyChanged();
                }
            }
        }

        private void UpdateHotkeyString()
        {
            HotkeyString = ToString();
        }
        public Hotkey(Key key = Key.None, ModifierKeys modifierKeys = ModifierKeys.None, HotkeyMode hotkeyMode = HotkeyMode.SingleExecution)
        {
            Key = key;
            ModifierKeys = modifierKeys;
            _id = GetHashCode();
            _hWnd = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            HotkeyMode = hotkeyMode;
            HotkeyString = ToString();

            var source = HwndSource.FromHwnd(_hWnd);
            source.AddHook(WndProc);
        }

        public string HotkeyString
        {
            get => _hotkeyString;
            set
            {
                if (_hotkeyString != value)
                {
                    _hotkeyString = value;
                    OnPropertyChanged();
                }
            }
        }

        public HotkeyMode HotkeyMode
        {
            get => _hotkeyMode;
            set
            {
                if (_hotkeyMode != value)
                {
                    _hotkeyMode = value;
                    OnPropertyChanged();
                }
            }
        }


        public bool Register()
        {
            if (!IsRegistered)
            {
                int virtualKeyCode = KeyInterop.VirtualKeyFromKey(Key);
                uint fsModifiers = (uint)ModifierKeys;

                IsRegistered = RegisterHotKey(_hWnd, _id, fsModifiers, (uint)virtualKeyCode);
            }

            return IsRegistered;
        }

        public bool Unregister()
        {
            if (IsRegistered)
            {
                IsRegistered = !UnregisterHotKey(_hWnd, _id);
            }

            return !IsRegistered;
        }

        public void Dispose()
        {
            Unregister();
            var source = HwndSource.FromHwnd(_hWnd);
            source.RemoveHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {

            const int WM_HOTKEY = 0x0312;

            if (msg == WM_HOTKEY && wParam.ToInt32() == _id && this.Active)
            {
                OnHotkeyPressed();
                Console.WriteLine("HotkeyPressed");
                handled = true;
            }
            return IntPtr.Zero;
        }

        protected virtual void OnHotkeyPressed()
        {
            //pass the key press hopefully
            InputSimulator inputSimulator = new InputSimulator();
            this.Unregister();
            if (ModifierKeys == ModifierKeys.None)
            {
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)KeyInterop.VirtualKeyFromKey(Key));
            }
            else if (ModifierKeys == ModifierKeys.Shift)
            {
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)KeyInterop.VirtualKeyFromKey(Key));
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
            }
            this.Register();

            executeRun();

        }

        //TODO
        protected void executeRun() {
            //need to determine how to handle


            if (this.HotkeyMode == HotkeyMode.SingleExecution)
            {

                //single execution default?
                foreach (Action a in this.Actions)
                {
                    a.Run();
                }

            }
            else if (this.HotkeyMode == HotkeyMode.RepeatWhilePressed)
            {

            }
            else if (this.HotkeyMode == HotkeyMode.Toggle) { }



        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        public override string ToString()
        {
            String keyString = Key.ToString();
            if (keyString.Contains("Oem"))
            {
                if (keyString == "Oem1")
                {
                    keyString = "SemiColon";
                }
                else if (keyString == "Oem3")
                {
                    keyString = "Tilde";
                }
                else if (keyString == "Oem6")
                {
                    keyString = "ClosedBracket";
                }
                else
                {
                    keyString = keyString.Replace("Oem", "");
                }
            }
            else if (keyString.StartsWith("D"))
            {
                if (keyString.Length > 1)
                {
                    if (Char.IsDigit(keyString[1]))
                        keyString = keyString.Replace("D", "");
                }
            }
            else if (keyString == "Next")
            {
                keyString = "PageDown";
            }
            if (ModifierKeys == ModifierKeys.None)
            {
                return keyString;
            }
            else
            {
                return $"{ModifierKeys} + {keyString}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
