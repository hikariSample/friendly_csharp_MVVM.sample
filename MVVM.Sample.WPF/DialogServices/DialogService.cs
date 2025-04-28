using System.Windows;

namespace MVVM.Sample.WPF.DialogServices;

public class DialogService : IDialogService
{
    /// <summary>
    /// 显示非模态对话框
    /// </summary>
    /// <param name="viewModel"></param>
    /// <exception cref="ArgumentNullException">找不到对应的窗口</exception>
    public void Show(object viewModel)
    {
        var dialog = FindWindow(viewModel) ?? (Window?)DialogLocator.Locate(viewModel);

        if (dialog == null)
        {
            throw new ArgumentNullException(nameof(Window));
        }
        dialog.DataContext = viewModel;
        dialog.Show();
    }

    /// <summary>
    /// 显示模态对话框
    /// </summary>
    /// <param name="ownerViewModel">可以是对象，也可以是类型</param>
    /// <param name="viewModel">可以是对象，也可以是类型</param>
    /// <exception cref="ArgumentNullException">找不到对应的窗口</exception>
    /// <returns></returns>
    public bool? ShowDialog(object? ownerViewModel, object viewModel)
    {
        var dialog = FindWindow(viewModel) ?? (Window?)DialogLocator.Locate(viewModel);

        if (dialog == null)
        {
            throw new ArgumentNullException(nameof(Window));
        }
        dialog.Owner = FindWindow(ownerViewModel);

        dialog.DataContext = viewModel;
        return dialog.ShowDialog();
    }
    /// <summary>
    /// 关闭指定ViewModel对应的窗口
    /// </summary>
    /// <param name="viewModel">可以是对象，也可以是类型</param>
    public void Close(object viewModel)
    {
        var window = FindWindow(viewModel);
        window?.Close();
    }

    /// <summary>
    /// 关闭指定ViewModel对应的窗口
    /// </summary>
    /// <param name="viewModel">可以是对象，也可以是类型</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Hide(object viewModel)
    {
        var window = FindWindow(viewModel);
        window?.Hide();
    }

    /// <summary>
    /// 根据ViewModel查找对应的窗口
    /// </summary>
    /// <param name="viewModel">可以是对象，也可以是类型</param>
    /// <returns></returns>
    private Window? FindWindow(object? viewModel)
    {
        if (viewModel == null) return null;
        // 遍历所有打开的窗口
        foreach (Window window in Application.Current.Windows)
        {
            if (viewModel is Type type)
            {
                // 检查窗口的DataContext是否为指定的ViewModel
                if (window.DataContext.GetType() == type)
                {
                    return window;
                }
            }
            else
            {
                // 检查窗口的DataContext是否为指定的ViewModel
                if (window.DataContext == viewModel)
                {
                    return window;
                }
            }

        }
        return null;

    }
}