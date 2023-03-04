using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace R3peat
{
    public class HotkeyIncrementer
    {
        private Key _key;
        private ModifierKeys _modifierKeys;

        private static readonly ModifierKeys[] ModifierKeyValues = (ModifierKeys[])Enum.GetValues(typeof(ModifierKeys));

        public HotkeyIncrementer(ModifierKeys modifierKeys, Key key)
        {
            _modifierKeys = modifierKeys;
            _key = key;
        }

        public Key Key
        {
            get { return _key; }
        }

        public ModifierKeys ModifierKeys
        {
            get { return _modifierKeys; }
        }

        public void Increment()
        {
            int currentBitmap = (int)_modifierKeys;
            int maxBitmap = (int)ModifierKeys.Control | (int)ModifierKeys.Alt | (int)ModifierKeys.Shift | (int)ModifierKeys.Windows;

            if (_key == Key.System)
            {
                if (currentBitmap == maxBitmap)
                {
                    throw new Exception("All hotkeys have been used.");
                }

                int nextBitmap = currentBitmap + 1;
                while (!IsValidModifierBitmap(nextBitmap))
                {
                    nextBitmap++;
                }

                _modifierKeys = (ModifierKeys)nextBitmap;
                _key = Key.A;
            }
            else if (_key == Key.Z)
            {
                _key = Key.System;
                _modifierKeys = ModifierKeys.None;
            }
            else
            {
                _key++;
            }
        }

        private bool IsValidModifierBitmap(int bitmap)
        {
            ModifierKeys modifiers = (ModifierKeys)bitmap;
            return modifiers == ModifierKeys.None || ModifierKeyValues.Any(m => modifiers.HasFlag(m));
        }

        public override string ToString()
        {
            string modifiers = string.Join(" + ", ModifierKeyValues.Where(m => _modifierKeys.HasFlag(m)).Select(m => m.ToString()));
            return modifiers + (_modifierKeys == ModifierKeys.None ? "" : " + ") + _key.ToString();
        }
    }

}
