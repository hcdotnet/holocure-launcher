using System;
using DiscordRPC;
using DiscordRPC.Message;
using HoloCure.Launcher.Base.Games;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace HoloCure.Launcher.Desktop.Components;

internal class DRPComponent : Component
{
    private const string client_id = "1018319345073533088";
    private const string large_image_key = "logo_big";

    private DiscordRpcClient client = null!;

    private readonly RichPresence presence = new()
    {
        Assets = new Assets { LargeImageKey = large_image_key }
    };

    // see: https://github.com/ppy/osu/blob/master/osu.Desktop/DiscordRichPresence.cs#L48
    [BackgroundDependencyLoader]
    private void load(GameProvider gameProvider)
    {
        client = new DiscordRpcClient(client_id)
        {
            SkipIdenticalPresence = false // https://github.com/ppy/osu/blob/master/osu.Desktop/DiscordRichPresence.cs#L52
        };

        client.OnReady += onReady;
        client.OnConnectionFailed += (_, _) => client.Deinitialize(); // https://github.com/ppy/osu/blob/master/osu.Desktop/DiscordRichPresence.cs#L57
        client.OnError += (_, e) => Logger.Log($"An error occurred with Discord RPC Client: {e.Code} {e.Message}", LoggingTarget.Network);
        client.Initialize();

        gameProvider.SelectedGame.ValueChanged += e => { updatePresence(e.NewValue, gameProvider.PlayingGame.Value); };
        gameProvider.PlayingGame.ValueChanged += e => { updatePresence(gameProvider.SelectedGame.Value, e.NewValue); };
        updatePresence(gameProvider.SelectedGame.Value, gameProvider.PlayingGame.Value); // ensure current selected game when this component is loaded is displayed
    }

    private void onReady(object _, ReadyMessage __)
    {
        Logger.Log("Discord RPC Client ready.", LoggingTarget.Network, LogLevel.Debug);
        updateStatus();
    }

    private void updatePresence(Base.Games.Game? browsingGame, Base.Games.Game? playingGame)
    {
        presence.Details = playingGame is null ? "Browsing games..." : "Playing game!";
        presence.State = playingGame is null ? browsingGame is null ? "" : $"Looking at {browsingGame.GameTitle}" : $"Playing {playingGame.GameTitle}";

        presence.Timestamps = new Timestamps
        {
            Start = playingGame is not null ? DateTime.UtcNow : null
        };

        updateStatus();
    }

    private void updateStatus()
    {
        if (!client.IsInitialized) return;

        client.SetPresence(presence);
    }

    protected override void Dispose(bool isDisposing)
    {
        base.Dispose(isDisposing);
        client.Dispose();
    }
}
