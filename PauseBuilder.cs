using System.Collections.Generic;
using WindowsInput;

namespace R3peat
{
    class PauseBuilder 
    {
        private int PauseDuration;
        private Pause pause;
        private string Name;
        private int totalBuiltWithNoName { get; set; }
        public void BuildPause() {
            this.PauseDuration = 100;
            this.totalBuiltWithNoName+=1;
            this.Name = "Pause "+totalBuiltWithNoName.ToString();
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
        }

    }
}
