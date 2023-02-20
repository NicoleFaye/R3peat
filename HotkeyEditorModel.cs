using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

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

        public HotkeyEditorModel(Macro macro) {
            this.CurrentMacro = macro;
        
        }
    }
}
