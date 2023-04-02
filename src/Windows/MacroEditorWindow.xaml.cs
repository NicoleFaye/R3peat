using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Windows.Globalization.NumberFormatting;

namespace R3peat
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MacroEditorWindow : Window
    {
        public MacroEditorModel MacroEditorModel;
        public ICoordinateConversion Converter = new WPFCoordinateConversion();

        //stops alt from doing anything
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
            {
                e.Handled = true;
            }
            else
            {
                base.OnKeyDown(e);
            }
        }
        public MacroEditorWindow(Macro macro)
        {
            InitializeComponent();
            
            
            this.MacroEditorModel = new MacroEditorModel(macro);
            this.MacroNameTextBox.Text = macro.Name;

            ActionList.ItemsSource = MacroEditorModel.CurrentMacro.Hotkey.Actions;
            NewActionTypeComboBox.ItemsSource = Enum.GetValues(typeof(ActionType));

            NumberBoxIntegerFormatter numberBoxIntegerFormatter = new NumberBoxIntegerFormatter();
            PauseDurationNumberBox.NumberFormatter = numberBoxIntegerFormatter;

            //sets up binding for PauseEditorGrid visibility 
            Binding PauseEditorVisibilityBinding = new Binding("PauseEditorGridVisibility");
            PauseEditorVisibilityBinding.Source = MacroEditorModel;
            PauseEditorGrid.SetBinding(Grid.VisibilityProperty, PauseEditorVisibilityBinding);

            //sets up binding for MouseMovementEditorGrid visibility 
            Binding MouseMovementVisibilityBinding = new Binding("MouseMovementEditorGridVisibility");
            MouseMovementVisibilityBinding.Source = MacroEditorModel;
            MouseMovementEditorGrid.SetBinding(Grid.VisibilityProperty, MouseMovementVisibilityBinding);

            Binding HotkeyDisplayBinding = new Binding("HotkeyString");
            HotkeyDisplayBinding.Source = this.MacroEditorModel.CurrentMacro.Hotkey;
            HotkeyDisplayLabel.SetBinding(Label.ContentProperty, HotkeyDisplayBinding);
            
            
            ChangeHotkeyButton.Content = "Change";
        }
        private void PauseNameChanged(object sender, TextChangedEventArgs e)
        {
            if (PauseNameTextBox.Text == "")
            {
                ((Action)ActionList.SelectedItem).Name = "Pause";
                return;
            }
            if (ActionList.SelectedIndex >= 0)
            {
                ((Action)ActionList.SelectedItem).Name = PauseNameTextBox.Text;
            }

        }
        private void ToggleHotkeyUpdate(object sender, RoutedEventArgs e)
        {
            ChangeHotkeyButton.Content = MacroEditorModel.ToggleHotkeyUpdate();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (MacroEditorModel.Updating)
            {
                if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                {
                    MacroEditorModel.ModifierKeys |= ModifierKeys.Control;
                }
                else if (e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
                {
                    MacroEditorModel.ModifierKeys |= ModifierKeys.Alt;
                }
                else if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                {
                    MacroEditorModel.ModifierKeys |= ModifierKeys.Shift;
                }
                else
                {
                    MacroEditorModel.Key = e.Key;
                }
                MacroEditorModel.UpdateKeyCombo();
            }
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (MacroEditorModel.Updating && MacroEditorModel.PossibleModifiers.Contains(e.Key))
            {
                if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                {
                    MacroEditorModel.ModifierKeys &= ~ModifierKeys.Control;
                }
                else if (e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
                {
                    MacroEditorModel.ModifierKeys &= ~ModifierKeys.Alt;
                }
                else if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                {
                    MacroEditorModel.ModifierKeys &= ~ModifierKeys.Shift;
                }
                MacroEditorModel.UpdateKeyCombo();
            }
        }


        private void PauseDurationChanged(NumberBox sender, NumberBoxValueChangedEventArgs e)
        {
            NumberBox _sender = sender;
            if (_sender.Value == double.NaN)
            {
                ((Pause)ActionList.SelectedItem).Duration = 0;
                return;
            }
            if (ActionList.SelectedIndex >= 0)
            {
                ((Pause)ActionList.SelectedItem).Duration = (int)_sender.Value;
            }
        }
        private void MacroNameChanged(object sender, TextChangedEventArgs e) { 
            TextBox _sender = (TextBox)sender;
            if (_sender.Text == "")
            {
                MacroEditorModel.CurrentMacro.Name = "Macro";
                return;
            }
            MacroEditorModel.CurrentMacro.Name= _sender.Text;
        }
        private void ActionNameChanged(object sender, TextChangedEventArgs e)
        {
            TextBox _sender = (TextBox)sender;
            if (_sender.Text == "")
            {
                ((Action)ActionList.SelectedItem).Name = "Mouse Movement";
                return;
            }
            if (ActionList.SelectedIndex >= 0)
            {
                ((Action)(ActionList.SelectedItem)).Name = _sender.Text;
            }
        }
        private void MouseMovementStepNameChanged(object sender, TextChangedEventArgs e)
        {
            TextBox _sender = (TextBox)sender;
            if (MouseMovementStepList.SelectedIndex < 0) return;
            if (_sender.Text == "")
            {
                ((MouseMovementStep)MouseMovementStepList.SelectedItem).Name = "Mouse Movement Step";
                return;
            }
            ((MouseMovementStep)MouseMovementStepList.SelectedItem).Name = _sender.Text;
            return;
        }
        private void MouseMovementStepAbsoluteXChanged(NumberBox sender, NumberBoxValueChangedEventArgs e) { 
            if(MouseMovementStepList.SelectedIndex < 0) return;
            if (sender.Value == double.NaN)
            {
                sender.Value = 0;
            }
            else if (sender.Value < 0)
            {
                sender.Value = 0;
            }
            else if (sender.Value > ushort.MaxValue) {
                sender.Value = ushort.MaxValue;
            }
            MouseMovementStepPixelXNumberBox.Value= Converter.AbsoluteXToPixelX((ushort)sender.Value);
        }
        private void MouseMovementStepAbsoluteYChanged(NumberBox sender, NumberBoxValueChangedEventArgs e) { 
            if(MouseMovementStepList.SelectedIndex < 0) return;
            if (sender.Value == double.NaN)
            {
                sender.Value = 0;
            }
            else if (sender.Value < 0)
            {
                sender.Value = 0;
            }
            else if (sender.Value > ushort.MaxValue) {
                sender.Value = ushort.MaxValue;
            }
            MouseMovementStepPixelYNumberBox.Value= Converter.AbsoluteYToPixelY((ushort)sender.Value);
        }

        private void ChangeActionOrderSooner(object sender, RoutedEventArgs e)
        {
            int currentIndex = ActionList.SelectedIndex;
            if (currentIndex <= 0)
            {
                return;
            }
            MacroEditorModel.ChangeActionOrderSooner(currentIndex);
            ActionList.SelectedIndex = currentIndex - 1;
        }
        private void DeleteAction(object sender, RoutedEventArgs e) {
            
            int currentIndex = ActionList.SelectedIndex;
            if (currentIndex <= 0)
            {
                return;
            }
            MacroEditorModel.DeleteAction(currentIndex);
        }

        private void DeleteMouseMovementStep(object sender, RoutedEventArgs e) {
            int currentIndex = MouseMovementStepList.SelectedIndex;
            MouseMovement currentMovement = (MouseMovement)ActionList.SelectedItem;
            if (currentIndex >= currentMovement.MouseMovementSteps.Count)
            {
                return;
            }
            MacroEditorModel.DeleteMouseMovementStep(currentIndex, currentMovement.MouseMovementSteps);
        }
        private void ChangeActionOrderLater(object sender, RoutedEventArgs e)
        {
            int currentIndex = ActionList.SelectedIndex;
            if (currentIndex >= MacroEditorModel.CurrentMacro.Hotkey.Actions.Count)
            {
                return;
            }
            MacroEditorModel.ChangeActionOrderLater(currentIndex);
            ActionList.SelectedIndex = currentIndex + 1;
        }
        private void AddNewAction(object sender, RoutedEventArgs e)
        {
            if (NewActionTypeComboBox.SelectedIndex < 0) return;
            MacroEditorModel.AddNewAction((ActionType)NewActionTypeComboBox.SelectedItem);
        }


        public void OnMouseMovementStepSelectionChanged(object sender, SelectionChangedEventArgs e) {
            MouseMovementStep CurrentStep = (MouseMovementStep)((ListBox)sender).SelectedItem;
            if (CurrentStep == null)
            {
                MouseMovementStepNameTextBox.Text = "";
                MouseMovementStepAbsoluteXNumberBox.Value = 0;
                MouseMovementStepAbsoluteYNumberBox.Value = 0;
                MouseMovementStepPauseDurationNumberBox.Value = 0;
                MouseMovementStepPixelVarianceNumberBox.Value = 0;
                MouseMovementStepPixelXNumberBox.Value = 0;
                MouseMovementStepPixelYNumberBox.Value = 0;
            }
            else
            {
                MouseMovementStepNameTextBox.Text = CurrentStep.Name;
                MouseMovementStepAbsoluteXNumberBox.Value = CurrentStep.GetDestinationAbsoluteX();
                MouseMovementStepAbsoluteYNumberBox.Value = CurrentStep.GetDestinationAbsoluteY();
                MouseMovementStepPauseDurationNumberBox.Value = CurrentStep.GetPauseMillisecondDuration();
                MouseMovementStepPixelVarianceNumberBox.Value = CurrentStep.GetVariance();
                MouseMovementStepPixelXNumberBox.Value = Converter.AbsoluteXToPixelX(CurrentStep.GetDestinationAbsoluteX());
                MouseMovementStepPixelYNumberBox.Value = Converter.AbsoluteYToPixelY(CurrentStep.GetDestinationAbsoluteY());
            }
        }
        public void OnActionListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Action CurrentAction = (Action)ActionList.SelectedItem;
            if (CurrentAction == null) return;

            if (CurrentAction.GetType() == typeof(Pause))
            {
                MacroEditorModel.PauseEditorGridVisibility = Visibility.Visible;
                PauseNameTextBox.Text = CurrentAction.Name;
                PauseDurationNumberBox.Value = ((Pause)CurrentAction).Duration;

            }
            else
            {
                MacroEditorModel.PauseEditorGridVisibility = Visibility.Hidden;
            }
            if (CurrentAction.GetType() == typeof(MouseMovement))
            {
                MacroEditorModel.MouseMovementEditorGridVisibility = Visibility.Visible;
                MouseMovementNameTextBox.Text = CurrentAction.Name;
                MouseMovementStepList.ItemsSource = ((MouseMovement)CurrentAction).MouseMovementSteps;
            }
            else
            {
                MacroEditorModel.MouseMovementEditorGridVisibility = Visibility.Hidden;
            }
        }
        public void AddNewMouseMovementStep(object sender, RoutedEventArgs e)
        {
            MouseMovement CurrentMovement= ((MouseMovement)ActionList.SelectedItem);
            CurrentMovement.MouseMovementSteps.Add(new MouseMovementStep(CurrentMovement.MouseMovementStepNameIncrementer.Next()));
        }
        public void ChangeMouseMovementStepOrderSooner(object sender, RoutedEventArgs e) {
            int currentIndex = MouseMovementStepList.SelectedIndex;
            MouseMovement currentMovement = (MouseMovement)ActionList.SelectedItem;
            if (currentIndex <= 0)
            {
                return;
            }
            MacroEditorModel.ChangeStepOrderSooner(currentIndex, currentMovement.MouseMovementSteps);
            MouseMovementStepList.SelectedIndex = currentIndex - 1;
        }
        public void ChangeMouseMovementStepOrderLater(object sender, RoutedEventArgs e)
        {
            int currentIndex = MouseMovementStepList.SelectedIndex;
            MouseMovement currentMovement = (MouseMovement)ActionList.SelectedItem;
            if (currentIndex >= currentMovement.MouseMovementSteps.Count)
            {
                return;
            }
            MacroEditorModel.ChangeStepOrderLater(currentIndex, currentMovement.MouseMovementSteps);
            MouseMovementStepList.SelectedIndex = currentIndex + 1;
        }

        private void LoseFocusOnEscReturn(object sender, KeyEventArgs e)
        {
            if(e.Key== Key.Escape ||e.Key==Key.Return)
            {
                Keyboard.ClearFocus();
            }
        }
    }
}
