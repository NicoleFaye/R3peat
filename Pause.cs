using System.Threading;

namespace R3peat
{
    class Pause: Action
    {
        private int Delay;
        override public void Run() {
            Thread.Sleep(this.Delay);
        }
        public Pause(int milliseconds)
        {
            this.Delay= milliseconds;
            this.Name = "";
        }
    }
}
