using NHotkey.Wpf;
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
    /// Interaction logic for HotkeyEditorWindow.xaml
    /// </summary>
    public partial class HotkeyEditorWindow : Window
    {
        public HotkeyEditorModel HotkeyEditorModel;
        public HotkeyEditorWindow(Macro macro, HotkeyManager hotkeyManager)
        {
            InitializeComponent();
            this.HotkeyEditorModel = new HotkeyEditorModel(macro, hotkeyManager);

            Binding HotkeyDisplayBinding = new Binding("HotkeyString");
            HotkeyDisplayBinding.Source = macro.Hotkey;
            HotkeyDisplayLabel.SetBinding(Label.ContentProperty, HotkeyDisplayBinding);

            ChangeHotkeyButton.Content = "Change";
        }
        private void ToggleHotkeyUpdate(object sender, RoutedEventArgs e)
        {
            ChangeHotkeyButton.Content = HotkeyEditorModel.ToggleHotkeyUpdate();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (HotkeyEditorModel.Updating)
            {
                if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                {
                    HotkeyEditorModel.ModifierKeys = HotkeyEditorModel.ModifierKeys | ModifierKeys.Control;
                }
                else if (e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
                {
                    HotkeyEditorModel.ModifierKeys = HotkeyEditorModel.ModifierKeys | ModifierKeys.Alt;

                }
                else if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                {
                    HotkeyEditorModel.ModifierKeys = HotkeyEditorModel.ModifierKeys | ModifierKeys.Shift;
                }
                else
                {
                    HotkeyEditorModel.Key = e.Key;
                }
            }
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (HotkeyEditorModel.Updating)
            {
                if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                {
                    if (HotkeyEditorModel.ModifierKeys.HasFlag(ModifierKeys.Shift)
                        || HotkeyEditorModel.ModifierKeys.HasFlag(ModifierKeys.Alt))
                    {
                        HotkeyEditorModel.ModifierKeys &= ~ModifierKeys.Control;
                    }
                }
                else if (e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
                {

                    if (HotkeyEditorModel.ModifierKeys.HasFlag(ModifierKeys.Shift)
                        || HotkeyEditorModel.ModifierKeys.HasFlag(ModifierKeys.Control))
                    {
                        HotkeyEditorModel.ModifierKeys &= ~ModifierKeys.Alt;
                    }
                }
                else if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                {
                    if (HotkeyEditorModel.ModifierKeys.HasFlag(ModifierKeys.Alt)
                        || HotkeyEditorModel.ModifierKeys.HasFlag(ModifierKeys.Control))
                    {
                        HotkeyEditorModel.ModifierKeys &= ~ModifierKeys.Shift;
                    }
                }

            }

        }
    }
}
