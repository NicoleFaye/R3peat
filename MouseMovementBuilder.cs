using System.Collections.Generic;
using WindowsInput;

namespace R3peat
{
    class MouseMovementBuilder
    {
        private List<MouseMovementStep> MouseMovementSteps;
        private InputSimulator Input;
        public void BuildMouseMovement() {
            this.MouseMovementSteps.Clear();
        }
        public void AddStep(MouseMovementStep newStep) { 
            this.MouseMovementSteps.Add(newStep);
        }
        public MouseMovement GetMouseMovement() {
            return new MouseMovement(this.Input,this.MouseMovementSteps);
        }

        protected MouseMovementBuilder(InputSimulator newInput) {
            this.Input = newInput;
            this.MouseMovementSteps = new List<MouseMovementStep>();
        }

    }
}
