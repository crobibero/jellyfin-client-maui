#if DEBUG
using System.Reflection;
using System.Threading.Tasks;
using Esp.Resources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.HotReload;

[assembly: AssemblyMetadata("IsTrimmable", "True")]

namespace Jellyfin.Maui.Helpers;

public static class Reload
{
    public static MauiAppBuilder EnableHotReload(this MauiAppBuilder builder, string? ideIp = null, int idePort = Constants.DEFAULT_PORT)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IMauiInitializeService, HotReloadBuilder>(_ => new HotReloadBuilder { IdeIp = ideIp, IdePort = idePort, }));
        return builder;
    }

    private class HotReloadBuilder : IMauiInitializeService
    {
        public string? IdeIp { get; set; }

        public int IdePort { get; set; } = 9988;

        public async void Initialize(IServiceProvider services)
        {
            var handlers = services.GetRequiredService<IMauiHandlersFactory>();

            MauiHotReloadHelper.Init(handlers.GetCollection());

            Reloadify.Reload.Instance.ReplaceType = (d) =>
            {
                MauiHotReloadHelper.RegisterReplacedView(d.ClassName, d.Type);
            };

            Reloadify.Reload.Instance.FinishedReload = () =>
            {
                MauiHotReloadHelper.TriggerReload();
            };

            await Task.Run(async () =>
            {
                try
                {
                    var success = await Reloadify.Reload.Init(IdeIp, IdePort).ConfigureAwait(false);

                    Console.WriteLine($"HotReload Initialize: {success}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }).ConfigureAwait(false);
        }
    }
}
#endif
