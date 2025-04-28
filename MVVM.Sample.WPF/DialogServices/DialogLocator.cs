using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace MVVM.Sample.WPF.DialogServices;

public static class DialogLocator
{
    private static ServiceCollection _services;

    public static readonly DependencyProperty IsRegisteredProperty =
        DependencyProperty.RegisterAttached(
            "IsRegistered",
            typeof(bool),
            typeof(DialogLocator),
            new PropertyMetadata(false, OnIsRegisteredChanged));

    private static void OnIsRegisteredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (DesignerProperties.GetIsInDesignMode(d))
            return; // 设计模式不处理
        var viewType = d.GetType();
        var viewModelTypeName = $"{viewType.Name}ViewModel";

        foreach (var descriptor in _services)
        {

            if (descriptor.ServiceType.Name == viewModelTypeName)
            {
                var viewModel = Ioc.Default.GetService(descriptor.ServiceType);
                ((FrameworkElement)d).DataContext = viewModel;
                break;
            }
        }
    }

    public static bool GetIsRegistered(DependencyObject obj) => (bool)obj.GetValue(IsRegisteredProperty);
    public static void SetIsRegistered(DependencyObject obj, bool value) => obj.SetValue(IsRegisteredProperty, value);
    public static void SetServiceCollection(ServiceCollection service)
    {
        _services = service;
    }
    /// <summary>
    /// 根据窗口的datacontext获得窗口
    /// </summary>
    /// <param name="viewModel">可以是对象，也可以是类型</param>
    /// <returns></returns>
    public static FrameworkElement? Locate(object viewModel)
    {
        Type viewType;
        if (viewModel is Type type)
        {
            viewType = type;
        }
        else
        {
            viewType = viewModel.GetType();
        }
        // 根据viewModel的类型，从dI容器中解析出窗口
        foreach (var descriptor in _services)
        {
            var service = Ioc.Default.GetService(descriptor.ServiceType);
            if (service is FrameworkElement frameworkElement && service.GetType().FullName == descriptor.ServiceType.FullName && frameworkElement.DataContext.GetType() == viewType)
            {
                return frameworkElement;
            }
        }
        return null;
    }
}