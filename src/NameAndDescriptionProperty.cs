using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    public class NameAndDescriptionProperty : NameProperty
    {
        private string _description = "";
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                base.onPropertyChanged("Description");
            }
        }
    }
}
