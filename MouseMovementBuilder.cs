using System.Collections.Generic;
using WindowsInput;

namespace R3peat
{
    class MouseMovementBuilder
    {
        private List<MouseMovementStep> MouseMovementSteps;
        private InputSimulator Input;
        private string Name;
        private int totalBuilt { get; set; }
        public void BuildMouseMovement() {
            this.MouseMovementSteps.Clear();
            this.totalBuilt+=1;
            this.Name = "Mouse Movement "+totalBuilt.ToString();
        }
        public void BuildMouseMovement(string Name) {
            this.MouseMovementSteps.Clear();
            this.Name = Name; 
        }
        public void AddStep(MouseMovementStep newStep) { 
            this.MouseMovementSteps.Add(newStep);
        }
        public void SetName(string Name) {
            this.Name = Name;
        }
        public MouseMovement GetMouseMovement() {
            return new MouseMovement(this.Input,this.Name,this.MouseMovementSteps);
        }

        protected MouseMovementBuilder(InputSimulator newInput) {
            this.Input = newInput;
            this.MouseMovementSteps = new List<MouseMovementStep>();
        }

    }
}
