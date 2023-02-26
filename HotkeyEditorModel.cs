using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using NHotkey.Wpf;
using System.Windows.Input;

namespace R3peat
{
    public class HotkeyEditorModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Macro CurrentMacro;
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

            foreach (Key k in keys)
            {
                if (Keyboard.IsKeyDown(k) && keyCombo == null)
                {
                    keyCombo = new KeyGesture(k);
                }
                else if (Keyboard.IsKeyDown(k) && keyCombo != null)
                {
                    if (modifierKeys.Contains(k) && keyCombo.Key != k)
                    {
                        if (altKeys.Contains(k))
                        {
                            keyCombo = new KeyGesture(keyCombo.Key, keyCombo.Modifiers | ModifierKeys.Alt);
                        }
                        else if (shiftKeys.Contains(k))
                        {
                            keyCombo = new KeyGesture(keyCombo.Key, keyCombo.Modifiers | ModifierKeys.Shift);
                        }
                        else if (ctrlKeys.Contains(k))
                        {
                            keyCombo = new KeyGesture(keyCombo.Key, keyCombo.Modifiers | ModifierKeys.Control);
                        }
                    }
                }
            }
            return keyCombo;
        }
    }
}
