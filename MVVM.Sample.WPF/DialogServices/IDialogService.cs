namespace MVVM.Sample.WPF.DialogServices;

public interface IDialogService
{
    void Show(object ownerViewModel, object viewModel);
    bool? ShowDialog(object ownerViewModel, object viewModel);
    void Close(object viewModel);

    void Hide(object viewModel);
}