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
        public ObservableCollection<Action> Actions { get; set; }= new ObservableCollection<Action>();
        private HotkeyObject Hotkey { get; set; }
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
        private string _id;
        public string ID
        {
            get
         {
                return _id;
            }
            set
            {
                _id = value;
                base.onPropertyChanged("ID");
            }
        }
        public Macro(string name, string id)
        {
            this.ID = id;
            this.Name = name;
            this.Hotkey = null;
        }
        public Macro(string name, string id,HotkeyObject newHotkeyObject)
        {
            this.ID = id;
            this.Name = name;
            this.Hotkey= newHotkeyObject;
        }
    }
}
