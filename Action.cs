﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3peat
{
    public abstract class Action
    {
        public string Name { get; set; }
        public abstract void Run();

    }
}
