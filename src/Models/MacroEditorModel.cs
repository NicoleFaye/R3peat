using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WindowsInput;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace R3peat
{
    public class MacroEditorModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Macro CurrentMacro;
        private InputSimulator input = new InputSimulator();
        protected MouseMovementBuilder MouseMovementBuilder;
        protected PauseBuilder PauseBuilder;
        private Visibility mouseMovementEditorGridVisibility;
        public ButtonPressBuilder ButtonPressBuilder { get; private set; }
        public MediaControlBuilder MediaControlBuilder { get; private set; }
        public Key Key { get; set; }
        public ModifierKeys ModifierKeys { get; set; }
        private DispatcherTimer _timer;
        private Boolean _updating;
        public Key[] PossibleModifiers { get; set; }

        public Boolean Updating
        {
            get
            {
                return _updating;
            }
            set
            {
                _updating = value;
                NotifyPropertyChanged("Updating");
            }
        }
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
        private Visibility pauseEditorGridVisibility;
        public Visibility PauseEditorGridVisibility
        {
            get
            {
                return pauseEditorGridVisibility;
            }
            set
            {
                pauseEditorGridVisibility = value;
                NotifyPropertyChanged("PauseEditorGridVisibility");
            }
        }
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public MacroEditorModel(Macro macro)
        {
            this.CurrentMacro = macro;
            MouseMovementBuilder = new MouseMovementBuilder(input);
            PauseBuilder = new PauseBuilder();
            ButtonPressBuilder = new ButtonPressBuilder(input);
            MediaControlBuilder = new MediaControlBuilder(input);
            MouseMovementEditorGridVisibility = Visibility.Hidden;
            PauseEditorGridVisibility = Visibility.Hidden;
            PossibleModifiers = new Key[]
            {
                Key.LeftAlt, Key.RightAlt,
                Key.LeftShift, Key.RightShift,
                Key.LeftCtrl, Key.RightCtrl,
            };
        }
        public void UpdateKeyCombo()
        {
            if (this.Key != Key.None && this.Key != Key.System)
            {
                if (CurrentMacro.Hotkey.Key != this.Key || CurrentMacro.Hotkey.ModifierKeys != this.ModifierKeys)
                {
                    if (CurrentMacro.Hotkey.IsRegistered) {
                        CurrentMacro.Hotkey.Unregister();
                    }
                    CurrentMacro.Hotkey.Key = this.Key;
                    CurrentMacro.Hotkey.ModifierKeys= this.ModifierKeys;
                    if (CurrentMacro.Hotkey.Register())
                    {
                        Console.WriteLine("Valid Hotkey: "+CurrentMacro.Hotkey.ToString());
                        if (!CurrentMacro.Hotkey.Active) { CurrentMacro.Hotkey.Unregister(); }
                    }
                    else {
                        Console.WriteLine("Invalid Hotkey");
                    }
                }
            }
        }

        public String ToggleHotkeyUpdate()
        {
            string output;
            if (Updating)
            {
                output = "Change";
            }
            else
            {
                output = "Finish";
            }
            Updating = !Updating;
            return output;
        }

        public void AddNewAction(ActionType type)
        {
            switch (type)
            {
                case ActionType.Pause:
                    CurrentMacro.Hotkey.Actions.Add(PauseBuilder.Build());
                    break;
                case ActionType.MouseMovement:
                    CurrentMacro.Hotkey.Actions.Add(MouseMovementBuilder.Build());
                    break;
                case ActionType.ButtonPress:
                    CurrentMacro.Hotkey.Actions.Add(ButtonPressBuilder.SetKey(Key.A).Build());
                    break;
                case ActionType.MediaControl:
                    CurrentMacro.Hotkey.Actions.Add(MediaControlBuilder.SetControlType(MediaControlType.PlayPause).Build());
                    break;
                default:
                    break;
            }
        }
        public void DeleteAction(int index)
        {
            CurrentMacro.Hotkey.Actions.RemoveAt(index);
        }
        public void DeleteMouseMovementStep(int index, ObservableCollection<MouseMovementStep> steps)
        {
            steps.RemoveAt(index);
        }
        public void ChangeActionOrderLater(int currentIndex)
        {
            if (currentIndex + 1 >= CurrentMacro.Hotkey.Actions.Count)
            {
                return;
            }
            Action swap = CurrentMacro.Hotkey.Actions[currentIndex + 1];
            CurrentMacro.Hotkey.Actions[currentIndex + 1] = CurrentMacro.Hotkey.Actions[currentIndex];
            CurrentMacro.Hotkey.Actions[currentIndex] = swap;
        }
        public void ChangeActionOrderSooner(int currentIndex)
        {
            if (currentIndex <= 0)
            {
                return;
            }
            Action swap = CurrentMacro.Hotkey.Actions[currentIndex - 1];
            CurrentMacro.Hotkey.Actions[currentIndex - 1] = CurrentMacro.Hotkey.Actions[currentIndex];
            CurrentMacro.Hotkey.Actions[currentIndex] = swap;
        }
        public void ChangeStepOrderSooner(int index, ObservableCollection<MouseMovementStep> steps)
        {
            if (index <= 0) return;
            MouseMovementStep swap = steps[index - 1];
            steps[index - 1] = steps[index];
            steps[index] = swap;
        }
        public void ChangeStepOrderLater(int index, ObservableCollection<MouseMovementStep> steps)
        {
            if (index + 1 >= steps.Count) return;
            MouseMovementStep swap = steps[index + 1];
            steps[index + 1] = steps[index];
            steps[index] = swap;
        }
    }
}
