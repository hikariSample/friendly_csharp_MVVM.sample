using MVVM.Sample.WPF.Views;

namespace MVVM.Sample.WPF.Models
{
    public class MainWindowModel : NotificationObject
    {
        private string _txt;
        public string Txt
        {
            get => _txt;
            set
            {
                _txt = value;
                NotifyPropertyChanged("Txt");
            }
        }

        public void CopyCommand(object obj)
        {
            this.Txt = Txt + Txt;
        }
        public void OpenCommand(object obj)
        {
            Window1 window1 = new Window1();
            window1.ShowDialog();
        }
    }
}