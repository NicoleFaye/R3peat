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

namespace R3peat
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Macro> macroList = new ObservableCollection<Macro>();
        public MainWindow()
        {
            InitializeComponent();
            MainItemsRepeater.ItemsSource = macroList;
        }
        private void NewMacro(object sender, RoutedEventArgs e) {
            Macro test = new Macro();
            test.Name = "test macro name";
            macroList.Add(test);
        }
    }
}
