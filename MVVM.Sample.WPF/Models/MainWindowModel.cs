﻿using Hikari.Mvvm;

namespace MVVM.Sample.WPF.Models
{
    public class MainWindowModel : NotificationObject
    {
        private string _txt;
        public string Txt
        {
            get => _txt;
            set { _txt = value; NotifyPropertyChanged(); }
        }
    }
}