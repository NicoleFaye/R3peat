﻿using NHotkey.Wpf;
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
        public HotkeyEditorWindow(Macro macro,HotkeyManager hotkeyManager)
        {
            InitializeComponent();
            this.HotkeyEditorModel = new HotkeyEditorModel(macro,hotkeyManager);
        }
    }
}
