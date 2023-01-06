using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using WindowsInput;

namespace R3peat
{
    public class MouseMovementBuilder
    {
        private ObservableCollection<MouseMovementStep> MouseMovementSteps;
        private InputSimulator Input;
        private string Name;
        private NameIncrementer NameIncrementer;
        public void BuildMouseMovement() {
            this.MouseMovementSteps = new ObservableCollection<MouseMovementStep>();
            this.Name = NameIncrementer.Next();
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

        public MouseMovementBuilder(InputSimulator newInput) {
            this.Input = newInput;
            this.MouseMovementSteps = new ObservableCollection<MouseMovementStep>();
            this.NameIncrementer = new NameIncrementer("Mouse Movement");
        }

    }
}
