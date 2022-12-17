using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    public abstract class Action
    {
        protected string Name;
        public abstract void Run();
        public String GetName() {
            return this.Name;
        }
        public void SetName(String newName) {
            this.Name=newName;
        }

    }
}
