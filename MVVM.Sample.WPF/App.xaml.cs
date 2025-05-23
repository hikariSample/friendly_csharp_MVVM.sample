﻿using System.Diagnostics;
using CommunityToolkit.Mvvm.DependencyInjection;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Extensions.DependencyInjection;
using MVVM.Sample.WPF.DialogServices;
using System.Windows;
using System.Windows.Threading;

namespace MVVM.Sample.WPF
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private static Mutex? _mutex = null;

        public App()
        {
            Current.DispatcherUnhandledException += App_OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            DispatcherHelper.Initialize();
        }
        /// <summary>
        /// UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandyControl.Controls.MessageBox.Show(e.Exception.Message, "UI线程全局异常", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
            DialogHelper.CloseLoading();
            //throw e.Exception;
        }

        /// <summary>
        /// 非UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                HandyControl.Controls.MessageBox.Show(exception.Message, "非UI线程全局异常", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogHelper.CloseLoading();
                //throw exception;
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            string? appName = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name;
            _mutex = new Mutex(true, appName, out bool createdNew);
            if (!createdNew)
            {
                // 程序已运行，退出当前实例
                Application.Current.Shutdown();
                return;
            }


            var services = new ServiceCollection();
            
            // 注册对话框服务（单例模式，全局共用）
            services.AddSingleton<IDialogService, DialogService>();


            // 注册主窗口和ViewModel
            services.Scan(selector => selector.FromAssembliesOf(typeof(App)).AddClasses(classes => classes.Where(c => c.BaseType==typeof(Window))).AsSelf().WithTransientLifetime());
            services.Scan(selector => selector.FromAssembliesOf(typeof(App)).AddClasses(classes => classes.Where(c => c.Name.EndsWith("ViewModel"))).AsSelf().WithTransientLifetime());

            services.AddTransient(typeof(Lazy<>));
            var serviceProvider = services.BuildServiceProvider();
            // 初始化 DI 容器
            Ioc.Default.ConfigureServices(serviceProvider);
            DialogLocator.SetServiceCollection(services);

            var service = Ioc.Default.GetService<MainWindow>();
            
            service?.Show();
            base.OnStartup(e);
        }

        /// <summary>
        /// 重启程序
        /// </summary>
        public static void Restart()
        {
            _mutex?.ReleaseMutex(); // 释放锁
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            location = location.Replace(".dll", ".exe");
            Process.Start(location); // 启动新实例
            Application.Current.Shutdown(); // 关闭当前实例
        }
    }
}
