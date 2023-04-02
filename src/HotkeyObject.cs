/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using NHotkey.Wpf;
using System.Windows.Input;
using System.Data;
using Windows.UI.Xaml.Automation.Peers;
using System.Collections;

namespace R3peat
{
    public class HotkeyObject : NameProperty
    {
        private HotkeyManager _manager;
        private HotkeyMode _hotkeyMode;
        private String _string;
        public String HotkeyString
        {
            get { return _string; }
            set
            {
                _string = value;
                base.onPropertyChanged("HotkeyString");

            }
        }
        public HotkeyMode HotkeyMode
        {
            get
            {
                return _hotkeyMode;
            }
            set
            {
                _hotkeyMode = value;
                base.onPropertyChanged("HotkeyMode");
            }
        }
        private KeyGesture _keyCombo;
        public KeyGesture KeyCombo
        {
            get
            {
                return _keyCombo;
            }
            set
            {
                _keyCombo = value;
                base.onPropertyChanged("KeyCombo");
                this.HotkeyString = KeyComboToString();
            }
        }
        public EventHandler<NHotkey.HotkeyEventArgs> Action { get; set; }
        public void Update()
        {
            if (Action != null)
            {
                _manager.AddOrReplace(this.Name, KeyCombo, Action);
            }
        }
        public String KeyComboToString()
        {
            
            String output = "";
            if (_keyCombo != null)
            {
                output += _keyCombo.Modifiers.ToString().Replace(", ", " + ");
                output += " + ";
                output += _keyCombo.Key.ToString();

            }
            return output;
        }
        public HotkeyObject(HotkeyManager hotkeyManager, String name)
        {
            _manager = hotkeyManager;
            this.Name = name;
            Action = null;
            KeyCombo = new KeyGesture(Key.F, ModifierKeys.Control | ModifierKeys.Shift);
        }
        public HotkeyObject(HotkeyManager hotkeyManager, String name, EventHandler<NHotkey.HotkeyEventArgs> handler)
        {
            _manager = hotkeyManager;
            this.Name = name;
            Action = handler;
        }

    }
}
*/
