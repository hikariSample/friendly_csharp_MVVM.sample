using System.Windows.Input;
using MVVM.Sample.WPF.Models;

namespace MVVM.Sample.WPF.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowModel Model { get; set; }
        public ICommand CopyCommand { get; }
        public MainWindowViewModel()
        {
            this.Model = new MainWindowModel();
            CopyCommand = new DelegateCommand<object>(obj => this.Model.CopyCommand(obj));
        }
    }
}