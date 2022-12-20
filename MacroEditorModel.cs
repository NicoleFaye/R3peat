using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WindowsInput;

namespace R3peat
{
    public class MacroEditorModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Action> actions = new ObservableCollection<Action>();
        private InputSimulator input = new InputSimulator();
        private MouseMovementBuilder MouseMovementBuilder;
        private PauseBuilder PauseBuilder;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public MacroEditorModel() {
            MouseMovementBuilder = new MouseMovementBuilder(input);
            PauseBuilder = new PauseBuilder();
        }

        public void AddNewAction(ActionType type) {
            switch (type)
            {
                case ActionType.Pause:
                    PauseBuilder.BuildPause();
                    actions.Add(PauseBuilder.GetPause());
                    break;
                case ActionType.MouseMovement:
                    MouseMovementBuilder.BuildMouseMovement();
                    actions.Add(MouseMovementBuilder.GetMouseMovement());
                    break;
                default:
                    break;
            }
        }
        public void ChangeActionOrderLater(int currentIndex)
        {
            Action swap = actions[currentIndex + 1];
            actions[currentIndex + 1] = actions[currentIndex];
            actions[currentIndex] = swap;
        }
        public void ChangeActionOrderSooner(int currentIndex)
        {
            Action swap = actions[currentIndex - 1];
            actions[currentIndex - 1] = actions[currentIndex];
            actions[currentIndex] = swap;
        }

    }
}
