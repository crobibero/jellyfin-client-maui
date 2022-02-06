using System.Net;
using System.Net.Http;
using System.Text;
using Jellyfin.Maui.Helpers;
using Jellyfin.Maui.Services;
using Jellyfin.Sdk;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using Polly;
using Polly.Extensions.Http;

namespace Jellyfin.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseCometApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("Quicksand-Bold.ttf", "QuicksandBold");
                fonts.AddFont("Quicksand-Light.ttf", "QuicksandLight");
                fonts.AddFont("Quicksand-Medium.ttf", "QuicksandMedium");
                fonts.AddFont("Quicksand-Regular.ttf", "QuicksandRegular");
                fonts.AddFont("Quicksand-SemiBold.ttf", "QuicksandSemiBold");
            });

/*#if DEBUG
        builder.EnableHotReload();
#endif*/

        // builder.Services.AddPages();
        // builder.Services.AddSdkClients();
        // builder.Services.AddServices();
        return builder.Build();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IStateService, StateService>();
        // services.AddSingleton<INavigationService, NavigationService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<ILibraryService, LibraryService>();
        services.AddScoped<IStateStorageService, StateStorageService>();
    }

    private static void AddSdkClients(this IServiceCollection services)
    {
        static HttpMessageHandler DefaultHttpClientHandlerDelegate(IServiceProvider serviceProvider)
        {
            return new SocketsHttpHandler
            {
                AutomaticDecompression = DecompressionMethods.All,
                RequestHeaderEncodingSelector = (_, _) => Encoding.UTF8
            };
        }

        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

        // Register sdk services
        services.AddSingleton<SdkClientSettings>();

        services.AddHttpClient<IApiKeyClient, ApiKeyClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IArtistsClient, ArtistsClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IAudioClient, AudioClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IBrandingClient, BrandingClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IChannelsClient, ChannelsClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ICollectionClient, CollectionClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IConfigurationClient, ConfigurationClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IDashboardClient, DashboardClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IDevicesClient, DevicesClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IDisplayPreferencesClient, DisplayPreferencesClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IDlnaClient, DlnaClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IDlnaServerClient, DlnaServerClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IDynamicHlsClient, DynamicHlsClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IEnvironmentClient, EnvironmentClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IFilterClient, FilterClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IGenresClient, GenresClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IHlsSegmentClient, HlsSegmentClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IImageClient, ImageClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IImageByNameClient, ImageByNameClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IInstantMixClient, InstantMixClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IItemLookupClient, ItemLookupClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IItemRefreshClient, ItemRefreshClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IItemsClient, ItemsClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ILibraryClient, LibraryClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IItemUpdateClient, ItemUpdateClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ILibraryStructureClient, LibraryStructureClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ILiveTvClient, LiveTvClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ILocalizationClient, LocalizationClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IMediaInfoClient, MediaInfoClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IMoviesClient, MoviesClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IMusicGenresClient, MusicGenresClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<INotificationsClient, NotificationsClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IPackageClient, PackageClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IPersonsClient, PersonsClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IPlaylistsClient, PlaylistsClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IPlaystateClient, PlaystateClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IPluginsClient, PluginsClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IQuickConnectClient, QuickConnectClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IRemoteImageClient, RemoteImageClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IScheduledTasksClient, ScheduledTasksClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ISearchClient, SearchClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ISessionClient, SessionClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IStartupClient, StartupClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IStudiosClient, StudiosClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ISubtitleClient, SubtitleClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ISuggestionsClient, SuggestionsClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ISyncPlayClient, SyncPlayClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ISystemClient, SystemClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ITimeSyncClient, TimeSyncClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ITrailersClient, TrailersClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<ITvShowsClient, TvShowsClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IUniversalAudioClient, UniversalAudioClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IUserClient, UserClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IUserLibraryClient, UserLibraryClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IUserViewsClient, UserViewsClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IVideoAttachmentsClient, VideoAttachmentsClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IVideosClient, VideosClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);

        services.AddHttpClient<IYearsClient, YearsClient>()
            .ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate)
            .AddPolicyHandler(retryPolicy);
    }
}
