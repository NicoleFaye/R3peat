using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace R3peat
{
    public class HotkeyObject: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public HotkeyMode HotkeyMode { get; set; }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
