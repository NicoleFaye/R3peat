using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace R3peat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MacroEditorWindow : Window
    {
        public ObservableCollection<Action> actions = new ObservableCollection<Action>();
        public MacroEditorWindow()
        {
            InitializeComponent();
            actions.Add(new Pause(2, "jim"));
            actions.Add(new Pause(2, "j3www34im"));
            ActionList.ItemsSource = actions;
            NewActionTypeComboBox.ItemsSource = Enum.GetValues(typeof(ActionType));
            actions.Add(new Pause(2, "j3www28937498234im"));
            actions.Add(new Pause(2, "j3www3982734982374im"));
            actions.Add(new Pause(2, "234723984723984j3www34im"));
            actions.Add(new Pause(2, "j3www34872938479im"));

        }
        private void ChangeActionOrderSooner(object sender, RoutedEventArgs e)
        {
            int currentIndex = ActionList.SelectedIndex;
            if (currentIndex == 0)
            {
                return;
            }
            Action swap = actions[currentIndex - 1];
            actions[currentIndex - 1] = actions[currentIndex];
            actions[currentIndex] = swap;
        }
        private void ChangeActionOrderLater(object sender, RoutedEventArgs e)
        {
            int currentIndex = ActionList.SelectedIndex;
            if (currentIndex + 1 >= ActionList.Items.Count)
            {
                return;
            }
            Action swap = actions[currentIndex + 1];
            actions[currentIndex + 1] = actions[currentIndex];
            actions[currentIndex] = swap;
        }
        private void AddNewAction(object sender, RoutedEventArgs e)
        {
            switch (ActionList.SelectedItem)
            {
                case ActionType.Pause:

                    break;
                case ActionType.MouseMovement:

                    break;
                default:
                    break;
            }
        }
    }
}
