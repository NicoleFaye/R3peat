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
        public MainModel mainModel= new MainModel();
        
        public MainWindow()
        {
            InitializeComponent();
            MainItemsRepeater.ItemsSource = mainModel.MacroList;
        }
        private void NewMacro(object sender, RoutedEventArgs e) {
            mainModel.NewMacro();
        }
        private void EditMacro(object sender, RoutedEventArgs e) {

            int index = -1;
            for (int i = 0; i < mainModel.MacroList.Count; i++) {
                if (mainModel.MacroList[i].ID == (string)(((Button)sender).Tag))
                {
                    index = i;
                }
            }
            if (index < 0) return;
            
            mainModel.EditMacro(index);
        }
        private void EditHotkey(object sender, RoutedEventArgs e) {
            int index = -1;
            for (int i = 0; i < mainModel.MacroList.Count; i++) {
                if (mainModel.MacroList[i].ID == (string)(((Button)sender).Tag))
                {
                    index = i;
                }
            }
            if (index < 0) return;
            mainModel.EditHotkey(index);
        }
            

    }
}
