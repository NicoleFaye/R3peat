using System.Collections.Generic;
using WindowsInput;

namespace R3peat
{
    public class PauseBuilder 
    {
        private int PauseDuration;
        private Pause pause;
        private string Name;
        private NameIncrementer NameIncrementer;
        public void BuildPause() {
            this.PauseDuration = 100;
            this.Name = NameIncrementer.Next();
            this.pause = new Pause(this.PauseDuration,this.Name);
        }
        public void BuildPause(string Name) {
            this.Name = Name; 
        }
        public void SetName(string Name) {
            this.Name = Name;
        }
        public void SetPauseDuration(int PauseDuration) {
            this.PauseDuration = PauseDuration;
        }
        public Pause GetPause() {
            return this.pause;
        }

        public PauseBuilder() {
            this.NameIncrementer = new NameIncrementer("Pause");
        }

    }
}
