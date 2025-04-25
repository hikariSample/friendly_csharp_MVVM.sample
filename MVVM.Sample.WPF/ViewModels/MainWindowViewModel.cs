using CommunityToolkit.Mvvm.Input;
using GalaSoft.MvvmLight.Threading;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.DependencyInjection;
using MVVM.Sample.WPF.DialogServices;
using MVVM.Sample.WPF.Models;

namespace MVVM.Sample.WPF.ViewModels
{
    public partial class MainWindowViewModel
    {
        private readonly Lazy<IDialogService> _dialogService;
        public Models.MainWindowModel Model { get; set; }
        public MainWindowViewModel(Lazy<IDialogService> dialogService)
        {

            this.Model = new MainWindowModel();
            _dialogService = dialogService;
            this.Model.UserNameList = new List<string>();
            this.Model.UserNameList.Add("admin");
            this.Model.UserNameList.Add("operator");
        }
        public ICommand CopyCommand
        {
            get
            {
                return new RelayCommand<object?>(delegate (object? obj)
                {
                    this.Model.Txt = this.Model.Txt + this.Model.Txt;
                });
            }
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        public ICommand OpenCommand
        {
            get
            {
                return new AsyncRelayCommand<object?>(async delegate (object? obj)
                {
                    int loopIndex = 0;
                    ThreadPool.QueueUserWorkItem(
                        o =>
                        {
                            // This is a background operation!

                            while (loopIndex < 100)
                            {
                                // Do something

                                DispatcherHelper.CheckBeginInvokeOnUI(
                                    () =>
                                    {
                                        // Dispatch back to the main thread
                                        this.Model.Txt = string.Format("Loop # {0}", loopIndex++);
                                    });

                                // Sleep for a while
                                Thread.Sleep(500);
                            }
                        });

                    //Window1 window1 = new Window1();
                    //window1.ShowDialog();
                });
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public ICommand ClosingCommand => new RelayCommand<object>((obj) =>
        {
            CancelEventArgs? e = obj as CancelEventArgs;
            if (HandyControl.Controls.MessageBox.Show("确定要关闭软件？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                Environment.Exit(0);
            }
        });
        /// <summary>
        /// 退出
        /// </summary>
        [RelayCommand]
        public void Exit()
        {
            _dialogService.Value.Close(this);
        }
        public ICommand PasswordChangedCommand => new RelayCommand<object>((obj) =>
        {
            if (obj is System.Windows.Controls.PasswordBox passwordBox)
            {
                Model.Password = passwordBox.Password;
            }
        });
        /// <summary>
        /// 登录
        /// </summary>
        public ICommand LoginCommand => new RelayCommand(() =>
        {

            if (string.IsNullOrWhiteSpace(Model.UserName) || string.IsNullOrWhiteSpace(Model.Password))
            {
                HandyControl.Controls.MessageBox.Show("请输入用户名和密码");
                return;
            }

            // TODO: 实现实际的登录验证逻辑
            if (Model.Password == "123")
            {
                // 如果是新用户名，添加到下拉列表中
                if (!Model.UserNameList.Contains(Model.UserName))
                {
                    Model.UserNameList.Add(Model.UserName);
                }

                HandyControl.Controls.MessageBox.Show("登录成功");
                // TODO: 跳转到主界面
                var vm = Ioc.Default.GetService<Window1ViewModel>();
                _dialogService.Value.Show(this, vm);
                _dialogService.Value.Hide(this);
            }
            else
            {
                HandyControl.Controls.MessageBox.Show("用户名或密码错误");
                Model.Password = "";
            }
        });
    }
}