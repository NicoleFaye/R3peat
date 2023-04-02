using R3peat;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
namespace R3peat
{
    public class Hotkey : INotifyPropertyChanged, IDisposable
    {
        private readonly Window _window;
        private readonly IntPtr _hWnd;
        private readonly int _id;
        private string _hotkeyString;
        private HotkeyMode _hotkeyMode;

        public Key Key { get; }
        public ModifierKeys ModifierKeys { get; }
        public bool IsRegistered { get; private set; }
        public Action Action { get; set; }

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

        public Hotkey(Window window, Key key, ModifierKeys modifierKeys, HotkeyMode hotkeyMode)
        {
            _window = window;
            Key = key;
            ModifierKeys = modifierKeys;
            _id = GetHashCode();
            _hWnd = new WindowInteropHelper(window).Handle;
            HotkeyMode = hotkeyMode;
            HotkeyString = ToString();

            var source = HwndSource.FromHwnd(_hWnd);
            source.AddHook(WndProc);
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

            if (msg == WM_HOTKEY && wParam.ToInt32() == _id)
            {
                OnHotkeyPressed();
                handled = true;
            }

            return IntPtr.Zero;
        }

        protected virtual void OnHotkeyPressed()
        {
            Action.Run();
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public static bool IsHotkeyRegistered(Key key, ModifierKeys modifierKeys)
        {
            int virtualKeyCode = KeyInterop.VirtualKeyFromKey(key);
            uint fsModifiers = (uint)modifierKeys;

            int tempId = -1; // Temporary ID for testing registration
            IntPtr hWnd = new WindowInteropHelper(Application.Current.MainWindow).Handle;

            bool isRegistered = RegisterHotKey(hWnd, tempId, fsModifiers, (uint)virtualKeyCode);



            if (isRegistered)
            {
                // Unregister the hotkey since it was only for testing purposes
                UnregisterHotKey(hWnd, tempId);
            }

            return isRegistered;
        }

        public override string ToString()
        {
            return $"{ModifierKeys} + {Key}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
