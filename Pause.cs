﻿using System.Threading;

namespace R3peat
{
    class Pause: IAction
    {
        private int Delay;
        public void Run() {
            Thread.Sleep(this.Delay);
        }
        public Pause(int milliseconds)
        {
            this.Delay= milliseconds;
        }
    }
}
