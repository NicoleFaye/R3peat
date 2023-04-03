using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace R3peat
{
    public class MainModel : INotifyPropertyChanged
    {
        private NameIncrementer MacroNameIncrementer = new NameIncrementer("Macro");
        private NameIncrementer HotkeyNameIncrementer = new NameIncrementer("Hotkey");
        public ObservableCollection<Macro> MacroList = new ObservableCollection<Macro>();
        public ObservableCollection<Hotkey> Hotkeys = new ObservableCollection<Hotkey>();

        private int CurrentMacroID = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        public void NewMacro()
        {
            Hotkey hotkeyObject = new Hotkey();
            Hotkeys.Add(hotkeyObject);
            Macro newMacro = new Macro(MacroNameIncrementer.Next(), CurrentMacroID++.ToString(), hotkeyObject);
            MacroList.Add(newMacro);
        }
        public void EditMacro(int index)
        {
            MacroEditorWindow EditorWindow = new MacroEditorWindow(MacroList[index]);
            EditorWindow.Show();
        }
        public void DeleteMacro(int index) { 
            MacroList.RemoveAt(index);
        }
    }
}
