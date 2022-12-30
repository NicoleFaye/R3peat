using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    public abstract class Action :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _name;
        public string Name { 
            get {
                return _name; 
            }
            set {
                _name = value;
                onPropertyChanged("Name");
            }
        }
        public abstract void Run();
        private void onPropertyChanged(string propertyName){
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
