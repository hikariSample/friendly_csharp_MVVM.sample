using System;
using System.Windows;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.DependencyInjection;
using Fs.Service.Interfaces;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Extensions.DependencyInjection;
using MVVM.Sample.WPF.DialogServices;

namespace MVVM.Sample.WPF
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
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
            //LogHelper.WriteError(e.Exception, "UI线程全局异常");
            e.Handled = true;
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
                //LogHelper.WriteError(exception, "非UI线程全局异常");
                //throw exception;
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();
            
            // 注册对话框服务（单例模式，全局共用）
            services.Scan(selector => selector.FromAssembliesOf(typeof(PopupService<>)).AddClasses().AsImplementedInterfaces().WithSingletonLifetime());


            // 注册主窗口和ViewModel
            services.Scan(selector => selector.FromAssembliesOf(typeof(App)).AddClasses(classes => classes.Where(c => c.BaseType==typeof(Window))).AsSelf().WithTransientLifetime());
            services.Scan(selector => selector.FromAssembliesOf(typeof(App)).AddClasses(classes => classes.Where(c => c.Name.EndsWith("ViewModel"))).AsSelf().WithTransientLifetime());

            services.AddTransient(typeof(Lazy<>));
            var serviceProvider = services.BuildServiceProvider();
            // 初始化 DI 容器
            Ioc.Default.ConfigureServices(serviceProvider);

            var service = Ioc.Default.GetService<IMainWindowService>();
            service.Show();
            base.OnStartup(e);
        }
    }
}
