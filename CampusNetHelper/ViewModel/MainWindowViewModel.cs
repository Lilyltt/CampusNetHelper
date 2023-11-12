using CommunityToolkit.Mvvm.ComponentModel;

namespace CampusNetHelper.ViewModel;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private string account = "";
    [ObservableProperty] private string password = "";
    [ObservableProperty] private bool autoLogin;
    [ObservableProperty] private bool autoExit;
}