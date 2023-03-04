using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    public class NameProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _name = "";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                onPropertyChanged("Name");
            }
        }
        protected void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
