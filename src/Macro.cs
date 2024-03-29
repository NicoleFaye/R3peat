﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace R3peat
{
    public class Macro : NameAndDescriptionProperty
    {
        private Hotkey _Hotkey;
        public Hotkey Hotkey { get {
                return _Hotkey;
            } set {
                _Hotkey = value;
                base.onPropertyChanged("Hotkey");
            }
        }
        private string _id;
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                base.onPropertyChanged("ID");
            }
        }
        public Macro(string name, string id, Hotkey newHotkey)
        {
            this.ID = id;
            this.Name = name;
            this.Hotkey = newHotkey;
        }
        public Macro(string name, string id) : this(name, id, null) { }
    }
}
