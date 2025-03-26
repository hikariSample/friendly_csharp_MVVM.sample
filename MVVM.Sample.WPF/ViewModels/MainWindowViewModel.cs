using CommunityToolkit.Mvvm.Input;
using GalaSoft.MvvmLight.Threading;
using MVVM.Sample.WPF.Models;
using System.Windows.Input;

namespace MVVM.Sample.WPF.ViewModels
{
    public class MainWindowViewModel
    {
        
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
        public MainWindowViewModel()
        {
            this.Model = new MainWindowModel();
        }
    }
}