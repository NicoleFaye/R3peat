using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WindowsInput;
using System.Windows;

namespace R3peat
{
    public class MacroEditorModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Action> actions = new ObservableCollection<Action>();
        private InputSimulator input = new InputSimulator();
        private MouseMovementBuilder MouseMovementBuilder;
        private PauseBuilder PauseBuilder;
        private Visibility mouseMovementEditorGridVisibility;

        public Visibility MouseMovementEditorGridVisibility
        {
            get
            {
                return mouseMovementEditorGridVisibility;
            }
            set
            {
                mouseMovementEditorGridVisibility = value;
                NotifyPropertyChanged("MouseMovementEditorGridVisibility");
            }
        }
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public MacroEditorModel()
        {
            MouseMovementBuilder = new MouseMovementBuilder(input);
            PauseBuilder = new PauseBuilder();
            MouseMovementEditorGridVisibility = Visibility.Hidden;
        }

        public void SelectedActionChanged(Action CurrentAction)
        {
            if (CurrentAction.GetType() == typeof(MouseMovement))
            {
                MouseMovementEditorGridVisibility = Visibility.Visible;
            }
            else
            {
                MouseMovementEditorGridVisibility = Visibility.Hidden;
            }
        }

        public void AddNewAction(ActionType type)
        {
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
            if (currentIndex + 1 >= actions.Count)
            {
                return;
            }
            Action swap = actions[currentIndex + 1];
            actions[currentIndex + 1] = actions[currentIndex];
            actions[currentIndex] = swap;
        }
        public void ChangeActionOrderSooner(int currentIndex)
        {
            if (currentIndex <= 0)
            {
                return;
            }
            Action swap = actions[currentIndex - 1];
            actions[currentIndex - 1] = actions[currentIndex];
            actions[currentIndex] = swap;
        }

    }
}
