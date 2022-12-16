using System.Threading;

namespace R3peat
{
    class Pause: IAction
    {
        private int Delay;
        private string Name;
        public void Run() {
            Thread.Sleep(this.Delay);
        }
        public string GetName() {
            return this.Name;
        }
        public Pause(int milliseconds)
        {
            this.Delay= milliseconds;
        }
    }
}
