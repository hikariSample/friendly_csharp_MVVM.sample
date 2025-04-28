namespace MVVM.Sample.WPF.DialogServices;

public interface IDialogService
{
    /// <summary>
    /// 显示非模态对话框
    /// </summary>
    /// <param name="viewModel"></param>
    /// <exception cref="ArgumentNullException">找不到对应的窗口</exception>
    void Show(object viewModel);
    /// <summary>
    /// 显示模态对话框
    /// </summary>
    /// <param name="ownerViewModel">可以是对象，也可以是类型</param>
    /// <param name="viewModel"></param>
    /// <exception cref="ArgumentNullException">找不到对应的窗口</exception>
    /// <returns></returns>
    bool? ShowDialog(object? ownerViewModel, object viewModel);
    /// <summary>
    /// 关闭指定ViewModel对应的窗口
    /// </summary>
    /// <param name="viewModel">可以是对象，也可以是类型</param>
    void Close(object viewModel);
    /// <summary>
    /// 关闭指定ViewModel对应的窗口
    /// </summary>
    /// <param name="viewModel">可以是对象，也可以是类型</param>
    /// <exception cref="ArgumentNullException"></exception>
    void Hide(object viewModel);
}