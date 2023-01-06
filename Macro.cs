using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    public class Macro : NameAndDescriptionProperty
    {
        ObservableCollection<Action> Actions = new ObservableCollection<Action>();
        private bool _active;
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
                base.onPropertyChanged("Active");
            }
        }
        public Macro(string name)
        {
            this.Name = name;
        }
    }
}
