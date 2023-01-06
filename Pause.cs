using System.Threading;

namespace R3peat
{
    public class Pause: Action
    {
        public int Duration {get;set;}
        override public void Run() {
            Thread.Sleep(this.Duration);
        }
        public Pause(int milliseconds)
        {
            this.Duration= milliseconds;
            this.Name = "";
        }
        public Pause(int milliseconds,string Name)
        {
            this.Duration= milliseconds;
            this.Name = Name;
        }
    }
}
