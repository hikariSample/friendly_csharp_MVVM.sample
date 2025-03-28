using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using GalaSoft.MvvmLight.Threading;
using MVVM.Sample.WPF.Models;
using System.Windows.Input;
using Fs.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MVVM.Sample.WPF.ViewModels
{
    public partial class MainWindowViewModel
    {
        private readonly Lazy<IWindow1Service> _mainWindowService;
        public Models.MainWindowModel Model { get; set; }
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

        // 关闭窗口
        public ICommand ClosingCommand => new RelayCommand<object>((obj) =>
        {
            CancelEventArgs? e = obj as CancelEventArgs;
            if (MessageBox.Show("确定要关闭软件？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                Environment.Exit(0);
            }
        });

        [RelayCommand]
        public void Exit()
        {
            if (MessageBox.Show("确定要关闭软件？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                _mainWindowService.Value.Show();
            }
            else
            {
                _mainWindowService.Value.Close();
                Environment.Exit(0);
            }
        }
        public ICommand PasswordChangedCommand => new RelayCommand<object>((obj) =>
        {
            if (obj is System.Windows.Controls.PasswordBox passwordBox)
            {
                Model.Password = passwordBox.Password;
            }
        });
        // 登录
        public ICommand LoginCommand => new RelayCommand(() =>
        {
            if (string.IsNullOrWhiteSpace(Model.UserName) || string.IsNullOrWhiteSpace(Model.Password))
            {
                MessageBox.Show("请输入用户名和密码");
                return;
            }

            // TODO: 实现实际的登录验证逻辑
            if (Model.Password == "admin123")
            {
                // 如果是新用户名，添加到下拉列表中
                if (!Model.UserNameList.Contains(Model.UserName))
                {
                    Model.UserNameList.Add(Model.UserName);
                }

                MessageBox.Show("登录成功");
                // TODO: 跳转到主界面
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
                Model.Password = "";
            }
        });
        public MainWindowViewModel(Lazy<IWindow1Service> mainWindowService)
        {
            //_mainWindowService = mainWindowService;
            _mainWindowService = mainWindowService;
            this.Model = new MainWindowModel();
            this.Model.UserNameList = new List<string>();
            this.Model.UserNameList.Add("admin");
            this.Model.UserNameList.Add("operator");
        }
    }
}