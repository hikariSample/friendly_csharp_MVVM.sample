using System.Windows;
using Fs.Service.Interfaces;

namespace MVVM.Sample.WPF.DialogServices;

public class MainWindowService : PopupService<MainWindow>, IMainWindowService;