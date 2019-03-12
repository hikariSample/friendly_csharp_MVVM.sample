using System.Windows.Input;
using MVVM.Sample.WPF.Models;

namespace MVVM.Sample.WPF.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowModel Model { get; set; }
        public ICommand CopyCommand { get; }
        /// <summary>
        /// 打开窗口
        /// </summary>
        public ICommand OpenCommand { get; }
        public MainWindowViewModel()
        {
            this.Model = new MainWindowModel();
            CopyCommand = new DelegateCommand<object>(obj => this.Model.CopyCommand(obj));
            OpenCommand = new DelegateCommand<object>(obj => this.Model.OpenCommand(obj));
        }
    }
}