using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsInput;

namespace R3peat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MacroEditorWindow : Window
    {
        public MacroEditorModel MacroEditorModel=new MacroEditorModel();



        public MacroEditorWindow()
        {
            InitializeComponent();
            ActionList.ItemsSource = MacroEditorModel.actions;
            NewActionTypeComboBox.ItemsSource = Enum.GetValues(typeof(ActionType));

            //Comment out to disable grid
            MacroEditorModel.MouseMovementEditorGridVisible=Visibility.Visible;
            
            //sets up binding for MouseMovementEditorGrid visibility 
            Binding binding = new Binding("MouseMovementEditorGridVisible");
            binding.Source = MacroEditorModel;
            MouseMovementEditorGrid.SetBinding(Grid.VisibilityProperty,binding);
        }
        private void ChangeActionOrderSooner(object sender, RoutedEventArgs e)
        {
            int currentIndex = ActionList.SelectedIndex;
            MacroEditorModel.ChangeActionOrderSooner(currentIndex);
            ActionList.SelectedIndex = currentIndex - 1;
        }
        private void ChangeActionOrderLater(object sender, RoutedEventArgs e)
        {
            int currentIndex = ActionList.SelectedIndex;
            MacroEditorModel.ChangeActionOrderLater(currentIndex);
            ActionList.SelectedIndex = currentIndex + 1;
        }
        private void AddNewAction(object sender, RoutedEventArgs e)
        {
            MacroEditorModel.AddNewAction((ActionType)NewActionTypeComboBox.SelectedItem);
        }

        //not sure if needed yet
        private bool SelectedActionIsMouseMovement(object sender, RoutedEventArgs e)
        {
            if (ActionList.SelectedItem.GetType() == typeof(MouseMovement)) return true;
            return false;
        }
    }
}
