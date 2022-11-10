using Jellyfin.Maui.Pages;
using Jellyfin.Maui.Services;

namespace Jellyfin.Maui;

/// <summary>
/// The main application.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        InitializeComponent();
        MainPage = InternalServiceProvider.GetService<MainPage>();
        InternalServiceProvider.GetService<ISdkService>().InitializeAsync().GetAwaiter().GetResult();
        InternalServiceProvider.GetService<INavigationService>().Initialize(this);
    }
}
