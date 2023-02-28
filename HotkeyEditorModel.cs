using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using NHotkey.Wpf;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections;

namespace R3peat
{
    public class HotkeyEditorModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Key Key { get; set; }
        public ModifierKeys ModifierKeys { get; set; }
        public Macro CurrentMacro;
        private DispatcherTimer _timer;
        private Boolean _updating;

        public Boolean Updating {
            get { 
                return _updating;
            }
            set
            {
                _updating = value;
                NotifyPropertyChanged("Updating");
            }
        }
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        public HotkeyEditorModel(Macro macro, HotkeyManager hotkeyManager) {
            this.CurrentMacro = macro;
        }
        public String ToggleHotkeyUpdate() {
            string output;
            if (Updating)
            {
                output= "Change";
            }
            else { 
                output= "Finish";
            }
            Updating = !Updating;
            return output;
        }
        KeyGesture GetKeyCombo()
        {
            KeyGesture keyCombo = null;
            Array keys = Enum.GetValues(typeof(Key));

            List<Key> shiftKeys = new List<Key>();
            shiftKeys.Add(Key.LeftShift);
            shiftKeys.Add(Key.RightShift);

            List<Key> ctrlKeys = new List<Key>();
            ctrlKeys.Add(Key.LeftCtrl);
            ctrlKeys.Add(Key.RightCtrl);

            List<Key> altKeys = new List<Key>();
            altKeys.Add(Key.LeftAlt);
            altKeys.Add(Key.RightAlt);

            List<Key> modifierKeys = new List<Key>();
            modifierKeys.Concat(shiftKeys);
            modifierKeys.Concat(ctrlKeys);
            modifierKeys.Concat(altKeys);

            Key key = Key.None;
            ModifierKeys modifiers = ModifierKeys.None;

            foreach (Key k in keys)
            {
                if (k == Key.None) continue;
                if (Keyboard.IsKeyDown(k) )
                {
                    if (modifierKeys.Contains(k))
                    {
                        if (altKeys.Contains(k))
                        {
                            modifiers = modifiers|ModifierKeys.Alt;
                        }
                        else if (shiftKeys.Contains(k))
                        {
                            modifiers = modifiers|ModifierKeys.Shift;
                        }
                        else if (ctrlKeys.Contains(k))
                        {
                            modifiers = modifiers|ModifierKeys.Control;
                        }
                    }
                    else
                    {
                        key= k;
                    }
                }
            }
            if(key!=Key.None && modifiers!=ModifierKeys.None)
                keyCombo = new KeyGesture(key, modifiers);
            else
                keyCombo = null;
            return keyCombo;
        }
    }
}
