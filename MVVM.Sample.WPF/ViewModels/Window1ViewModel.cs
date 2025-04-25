using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MVVM.Sample.WPF.DialogServices;

namespace MVVM.Sample.WPF.ViewModels
{
    public class Window1ViewModel
    {
        private Lazy<IDialogService> _dialogService;
        public Window1ViewModel(Lazy<IDialogService> dialogService)
        {
            _dialogService = dialogService;
        }/// <summary>
        /// 关闭窗口
        /// </summary>
        public ICommand ClosingCommand => new RelayCommand<object>((obj) =>
        {
            _dialogService.Value.Close(typeof(MainWindowViewModel));
        });
    }
}
