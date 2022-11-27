using DiscordRPC;
using DiscordRPC.Message;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace HoloCure.Launcher.Desktop.Components;

internal class DRPComponent : Component
{
    private const string client_id = "1018319345073533088";
    private const string large_image_key = "logo_big";

    private DiscordRpcClient client = null!;

    private readonly RichPresence presence = new RichPresence
    {
        Assets = new Assets { LargeImageKey = large_image_key }
    };

    // see: https://github.com/ppy/osu/blob/master/osu.Desktop/DiscordRichPresence.cs#L48
    [BackgroundDependencyLoader]
    private void load()
    {
        client = new DiscordRpcClient(client_id)
        {
            SkipIdenticalPresence = false // https://github.com/ppy/osu/blob/master/osu.Desktop/DiscordRichPresence.cs#L52
        };

        client.OnReady += onReady;

        // https://github.com/ppy/osu/blob/master/osu.Desktop/DiscordRichPresence.cs#L57
        client.OnConnectionFailed += (_, _) => client.Deinitialize();

        client.OnError += (_, e) => Logger.Log($"An error occurred with Discord RPC Client: {e.Code} {e.Message}", LoggingTarget.Network);

        client.Initialize();
    }

    private void onReady(object _, ReadyMessage __)
    {
        Logger.Log("Discord RPC Client ready.", LoggingTarget.Network, LogLevel.Debug);
        updateStatus();
    }

    private void updateStatus()
    {
        if (!client.IsInitialized) return;

        client.SetPresence(presence);
    }

    protected override void Dispose(bool isDisposing)
    {
        client.Dispose();
        base.Dispose(isDisposing);
    }
}
