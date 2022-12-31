using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    public class NameIncrementer 
    {
        private string _type;
        public int count { get; set; }
        public NameIncrementer(string type) {
            _type = type;
            count = 1;
        }
        public string Next() {
            return _type + " " + count++.ToString();
        }

    }
}
