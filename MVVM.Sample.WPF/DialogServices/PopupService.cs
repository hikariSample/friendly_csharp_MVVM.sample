using System.Reflection;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Fs.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MVVM.Sample.WPF.DialogServices;

public class PopupService<T> : IPopupService
{
    private readonly Window _window;
    public PopupService()
    {
        _window = (Window)Ioc.Default.GetService(typeof(T));
    }
    public object GetInstance()
    {
        return _window;
    }

    public void SetDataContext(object viewModel)
    {
        throw new NotImplementedException();
    }

    public void Show()
    {
        _window.Show();
    }

    public void Close()
    {
        _window.Close();
    }

    public void Hide()
    {
        _window.Hide();
    }
}