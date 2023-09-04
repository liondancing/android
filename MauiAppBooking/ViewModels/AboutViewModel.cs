using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace MauiAppBooking.ViewModels
{
    internal class AboutViewModel
    {
        public string Title => AppInfo.Name;
        public string Version => AppInfo.VersionString;
        public string MoreInfoUrl => "https://aka.ms/maui";
        public string Message => "This app is written in XAML and C# with .Net MAUI";
        public ICommand ShowMoreInfoCommand { get; }

        public AboutViewModel()
        {
            ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
        }

        async Task ShowMoreInfo() => await Launcher.Default.OpenAsync(MoreInfoUrl);
    }
}
