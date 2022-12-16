using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    public interface IAction
    {
        void Run();
        String GetName();
    }
}
