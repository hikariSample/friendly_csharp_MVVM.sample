
using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVM.Sample.WPF.Models
{
    public class MainWindowModel : ObservableObject
    {
        private string _txt;
        public string Txt
        {
            get => _txt;
            set { _txt = value; OnPropertyChanged(); }
        }
    }
}