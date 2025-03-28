
using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVM.Sample.WPF.Models
{
    public partial class MainWindowModel : ObservableObject
    {
        [ObservableProperty]
        private string _txt;

        [ObservableProperty]
        private List<string> _userNameList;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

    }
}