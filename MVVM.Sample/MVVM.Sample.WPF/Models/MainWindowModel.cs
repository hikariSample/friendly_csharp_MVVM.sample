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
    }
}