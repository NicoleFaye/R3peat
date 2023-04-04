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

        public MouseMovement Build(string name = null)
        {
            MouseMovementSteps = new ObservableCollection<MouseMovementStep>();
            Name = name ?? NameIncrementer.Next();
            return new MouseMovement(this.Input, this.Name, this.MouseMovementSteps);
        }

        public MouseMovementBuilder AddStep(MouseMovementStep newStep)
        {
            MouseMovementSteps.Add(newStep);
            return this;
        }

        public MouseMovementBuilder SetName(string name)
        {
            Name = name;
            return this;
        }

        public MouseMovement GetMouseMovement()
        {
            return new MouseMovement(Input, Name, MouseMovementSteps);
        }

        public MouseMovementBuilder(InputSimulator newInput)
        {
            Input = newInput;
            MouseMovementSteps = new ObservableCollection<MouseMovementStep>();
            NameIncrementer = new NameIncrementer("Mouse Movement");
        }
    }

}
