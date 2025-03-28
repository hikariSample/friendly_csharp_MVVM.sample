using System.ComponentModel;
using System.Reflection;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace MVVM.Sample.WPF.ViewModels;

public static class ViewModelLocator
{
    public static readonly DependencyProperty AutoWireViewModelProperty =
        DependencyProperty.RegisterAttached(
            "AutoWireViewModel",
            typeof(bool),
            typeof(ViewModelLocator),
            new PropertyMetadata(false, OnAutoWireViewModelChanged));

    private static void OnAutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (DesignerProperties.GetIsInDesignMode(d))
            return; // 设计模式不处理

        var viewType = d.GetType();
        var viewModelTypeName = $"MVVM.Sample.WPF.ViewModels.{viewType.Name}ViewModel";
        var viewModelType = Type.GetType(viewModelTypeName);

        if (viewModelType != null)
        {
            // 从 DI 容器解析 ViewModel
            var viewModel = Ioc.Default.GetService(viewModelType);
            ((FrameworkElement)d).DataContext = viewModel;
        }
    }

    public static bool GetAutoWireViewModel(DependencyObject obj) => (bool)obj.GetValue(AutoWireViewModelProperty);
    public static void SetAutoWireViewModel(DependencyObject obj, bool value) => obj.SetValue(AutoWireViewModelProperty, value);
}