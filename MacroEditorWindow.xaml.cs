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

namespace R3peat
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MacroEditorWindow : Window
    {
        public MacroEditorModel MacroEditorModel = new MacroEditorModel();
        public MacroEditorWindow()
        {
            InitializeComponent();
            ActionList.ItemsSource = MacroEditorModel.actions;
            NewActionTypeComboBox.ItemsSource = Enum.GetValues(typeof(ActionType));


            //sets up binding for PauseEditorGrid visibility 
            Binding PauseEditorVisibilityBinding = new Binding("PauseEditorGridVisibility");
            PauseEditorVisibilityBinding.Source = MacroEditorModel;
            PauseEditorGrid.SetBinding(Grid.VisibilityProperty, PauseEditorVisibilityBinding);

            //sets up binding for MouseMovementEditorGrid visibility 
            Binding MouseMovementVisibilityBinding = new Binding("MouseMovementEditorGridVisibility");
            MouseMovementVisibilityBinding.Source = MacroEditorModel;
            MouseMovementEditorGrid.SetBinding(Grid.VisibilityProperty, MouseMovementVisibilityBinding);
        }
        private void ChangeActionOrderSooner(object sender, RoutedEventArgs e)
        {
            int currentIndex = ActionList.SelectedIndex;
            if (currentIndex <=0) {
                return;
            }
            MacroEditorModel.ChangeActionOrderSooner(currentIndex);
            ActionList.SelectedIndex = currentIndex - 1;
        }
        private void ChangeActionOrderLater(object sender, RoutedEventArgs e)
        {
            int currentIndex = ActionList.SelectedIndex;
            if (currentIndex >= MacroEditorModel.actions.Count)
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


        public void OnActionListSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            MacroEditorModel.SelectedActionChanged((Action)ActionList.SelectedItem);
        }
    }
}
