using Jellyfin.Maui.Pages;
using Jellyfin.Maui.Pages.Facades;
using Jellyfin.Maui.Pages.Login;
using Jellyfin.Maui.ViewModels;
using Jellyfin.Maui.ViewModels.Facades;

namespace Jellyfin.Maui.Services;

/// <inheritdoc />
public class NavigationService : INavigationService
{
    // Application is initialized on startup.
    private Application _application = null!;
    private NavigationPage? _navigationPage;
    private NavigationPage? _loginNavigationPage;

    /// <inheritdoc />
    public void Initialize(Application application)
    {
        _application = application;
    }

    /// <inheritdoc />
    public void NavigateToUserSelectPage()
    {
        if (_loginNavigationPage is null)
        {
            NavigateToServerSelectPage();
            return;
        }

        Application.Current?.Dispatcher.Dispatch(() =>
        {
            var userSelectPage = InternalServiceProvider.GetService<SelectUserPage>();
            _loginNavigationPage.PushAsync(userSelectPage).SafeFireAndForget();
        });
    }

    /// <inheritdoc />
    public void NavigateToLoginPage()
    {
        if (_loginNavigationPage is null)
        {
            NavigateToServerSelectPage();
            return;
        }

        Application.Current?.Dispatcher.Dispatch(() =>
        {
            var loginPage = InternalServiceProvider.GetService<LoginPage>();
            _loginNavigationPage.PushAsync(loginPage).SafeFireAndForget();
        });
    }

    /// <inheritdoc />
    public void NavigateToServerSelectPage()
    {
        if (_loginNavigationPage is null)
        {
            Application.Current?.Dispatcher.Dispatch(() =>
            {
                var serverSelectPage = InternalServiceProvider.GetService<ServerSelectPage>();
                _loginNavigationPage = new NavigationPage(serverSelectPage);
                _application.MainPage = _loginNavigationPage;
            });
        }
        else
        {
            Application.Current?.Dispatcher.Dispatch(() => _loginNavigationPage.PopToRootAsync(true).SafeFireAndForget());
        }
    }

    /// <inheritdoc />
    public void NavigateToAddServerPage()
    {
        if (_loginNavigationPage is null)
        {
            NavigateToServerSelectPage();
            return;
        }

        Application.Current?.Dispatcher.Dispatch(() =>
        {
            var addServerPage = InternalServiceProvider.GetService<AddServerPage>();
            _loginNavigationPage.PushAsync(addServerPage).SafeFireAndForget();
        });
    }

    /// <inheritdoc />
    public void NavigateHome()
    {
        _loginNavigationPage = null;
        if (_navigationPage is null)
        {
            Application.Current?.Dispatcher.Dispatch(() =>
            {
                var homePage = InternalServiceProvider.GetService<HomePage>();
                _application.MainPage = _navigationPage = new NavigationPage(homePage);
            });
        }
        else
        {
            Application.Current?.Dispatcher.Dispatch(() => _navigationPage.PopToRootAsync(true).SafeFireAndForget());
        }
    }

    /// <inheritdoc />
    public void NavigateToItemView(BaseItemDto item)
    {
        switch (item.Type)
        {
            case BaseItemKind.CollectionFolder:
                Navigate<LibraryPage, LibraryViewModel>(item);
                break;
            default:
                Navigate<ItemPage, ItemViewModel>(item);
                break;
        }
    }

    private void Navigate<TPage, TViewModel>(BaseItemDto item)
        where TViewModel : BaseItemViewModel
        where TPage : BaseContentIdPage<TViewModel>
    {
        if (_navigationPage is null)
        {
            NavigateHome();
            return;
        }

        Application.Current?.Dispatcher.Dispatch(() =>
        {
            var resolvedView = InternalServiceProvider.GetService<TPage>();
            resolvedView.Initialize(item);
            _navigationPage.PushAsync(resolvedView, true).SafeFireAndForget();
        });
    }
}
