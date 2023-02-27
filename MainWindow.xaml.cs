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
using System.Windows.Shapes;
using Windows.Devices.Bluetooth.Background;
using NHotkey.Wpf;

namespace R3peat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Macro> MacroList = new ObservableCollection<Macro>();
        NameIncrementer MacroNameIncrementer = new NameIncrementer("Macro");
        HotkeyManager HotkeyManager;
        NameIncrementer HotkeyNameIncrementer = new NameIncrementer("Hotkey");
        public ObservableCollection<HotkeyObject> HotkeyObjects=new ObservableCollection<HotkeyObject>();

        int CurrentMacroID = 0;

        public MainWindow()
        {
            InitializeComponent();
            MainItemsRepeater.ItemsSource = MacroList;
        }
        private void NewMacro(object sender, RoutedEventArgs e) {
            HotkeyObject hotkeyObject= new HotkeyObject(HotkeyManager,HotkeyNameIncrementer.Next());
            HotkeyObjects.Add(hotkeyObject);
            Macro newMacro = new Macro(MacroNameIncrementer.Next(),CurrentMacroID++.ToString(),hotkeyObject);
            MacroList.Add(newMacro);
        }
        private void EditMacro(object sender, RoutedEventArgs e) {

            int index = -1;
            for (int i = 0; i < MacroList.Count; i++) {
                if (MacroList[i].ID == (string)(((Button)sender).Tag))
                {
                    index = i;
                }
            }
            if (index < 0) return;
            
            MacroEditorWindow EditorWindow = new MacroEditorWindow(MacroList[index]);
            EditorWindow.Show();

        }
        private void EditHotkey(object sender, RoutedEventArgs e) {
            int index = -1;
            for (int i = 0; i < MacroList.Count; i++) {
                if (MacroList[i].ID == (string)(((Button)sender).Tag))
                {
                    index = i;
                }
            }
            if (index < 0) return;
            HotkeyEditorWindow EditorWindow = new HotkeyEditorWindow(MacroList[index],HotkeyManager);
            EditorWindow.Show();
        }
            

    }
}
