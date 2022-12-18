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
using WindowsInput;

namespace R3peat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MacroEditorWindow : Window
    {
        public ObservableCollection<Action> actions = new ObservableCollection<Action>();
        private InputSimulator input = new InputSimulator();
        private MouseMovementBuilder MouseMovementBuilder;
        private PauseBuilder PauseBuilder;
        public MacroEditorWindow()
        {
            InitializeComponent();
            ActionList.ItemsSource = actions;
            NewActionTypeComboBox.ItemsSource = Enum.GetValues(typeof(ActionType));
            MouseMovementBuilder= new MouseMovementBuilder(input);
            PauseBuilder = new PauseBuilder();

        }
        private void ChangeActionOrderSooner(object sender, RoutedEventArgs e)
        {
            int currentIndex = ActionList.SelectedIndex;
            if (currentIndex <= 0)
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
            switch (NewActionTypeComboBox.SelectedItem)
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
    }
}
