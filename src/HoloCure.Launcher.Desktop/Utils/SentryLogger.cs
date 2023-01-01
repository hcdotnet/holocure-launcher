// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using System.IO;
using HoloCure.Launcher.Base;
using osu.Framework;
using osu.Framework.Logging;
using Sentry;
using Sentry.Protocol;

namespace HoloCure.Launcher.Desktop.Utils;

public class SentryLogger : IDisposable
{
    private LauncherBase game;
    private readonly IDisposable? sentrySession;

    public SentryLogger(LauncherBase game)
    {
        this.game = game;
        sentrySession = SentrySdk.Init(options =>
        {
            if (game.BuildInfo.IsDeployedBuild) options.Dsn = "https://d17c15f7a4e04e4d82ab6b941d3069cd@sentry.tomat.dev/2";
            options.AutoSessionTracking = true;
            options.IsEnvironmentUser = false; // ensure user isn't tracked; try to scrub away more information if any exists?
            options.Release = $"{LauncherBase.GAME_NAME}@{game.BuildInfo.AssemblyVersion.ToString()}-{game.BuildInfo.ReleaseChannel}";
        });

        Logger.NewEntry += processLogEntry;
    }

    private void processLogEntry(LogEntry entry)
    {
        if (!shouldSubmitEntry(ref entry)) return;

        if (entry.Exception is { } ex)
        {
            if (!shouldSubmitException(ex)) return;

            // framework does some weird exception redirection which means sentry does not see unhandled exceptions using its automatic methods.
            // but all unhandled exceptions still arrive via this pathway. we just need to mark them as unhandled for tagging purposes.
            // easiest solution is to check the message matches what the framework logs this as.
            // see https://github.com/ppy/osu-framework/blob/f932f8df053f0011d755c95ad9a2ed61b94d136b/osu.Framework/Platform/GameHost.cs#L336
            bool wasUnhandled = entry.Message == @"An unhandled error has occurred.";
            bool wasUnobserved = entry.Message == @"An unobserved error has occurred.";

            // see https://github.com/getsentry/sentry-dotnet/blob/c6a660b1affc894441c63df2695a995701671744/src/Sentry/Integrations/TaskUnobservedTaskExceptionIntegration.cs#L39
            if (wasUnobserved) ex.Data[Mechanism.MechanismKey] = @"UnobservedTaskException";

            // see https://github.com/getsentry/sentry-dotnet/blob/main/src/Sentry/Integrations/AppDomainUnhandledExceptionIntegration.cs#L38-L39
            if (wasUnhandled) ex.Data[Mechanism.MechanismKey] = @"AppDomain.UnhandledException";

            ex.Data[Mechanism.HandledKey] = !wasUnhandled;

            SentrySdk.CaptureEvent(
                new SentryEvent(ex)
                {
                    Message = entry.Message,
                    Level = getSentryLevel(entry.Level)
                },
                scope =>
                {
                    // add scope contexts eventually too (running game (if any), etc.)
                    scope.SetTag(@"os", $"{RuntimeInfo.OS} ({Environment.OSVersion})");
                    scope.SetTag(@"processor count", Environment.ProcessorCount.ToString());
                }
            );
        }
        else
            SentrySdk.AddBreadcrumb(entry.Message, entry.Target.ToString(), "navigation", level: getBreadcrumbLevel(entry.Level));
    }

    private BreadcrumbLevel getBreadcrumbLevel(LogLevel entryLevel) =>
        entryLevel switch
        {
            LogLevel.Debug => BreadcrumbLevel.Debug,
            LogLevel.Verbose => BreadcrumbLevel.Info,
            LogLevel.Important => BreadcrumbLevel.Warning,
            LogLevel.Error => BreadcrumbLevel.Error,
            _ => throw new ArgumentOutOfRangeException(nameof(entryLevel), entryLevel, null)
        };

    private SentryLevel getSentryLevel(LogLevel entryLevel) =>
        entryLevel switch
        {
            LogLevel.Debug => SentryLevel.Debug,
            LogLevel.Verbose => SentryLevel.Info,
            LogLevel.Important => SentryLevel.Warning,
            LogLevel.Error => SentryLevel.Error,
            _ => throw new ArgumentOutOfRangeException(nameof(entryLevel), entryLevel, null)
        };

    private bool shouldSubmitEntry(ref LogEntry entry)
    {
        // Modify "[context] Log for [user]" message to omit identifiable name.
        // Maybe redact/remove GL logging as well? Could be considered a tracking vector, but it's important info (as is OS type, etc.), so not sure.
        if (entry.Level == LogLevel.Verbose && entry.Message.Contains(" Log for ")) entry.Message = entry.Message.Replace(Environment.UserName, "[name removed]");

        return true;
    }

    private bool shouldSubmitException(Exception exception)
    {
        switch (exception)
        {
            case IOException ioe:
                // disk full exceptions, see https://stackoverflow.com/a/9294382
                const int hr_error_handle_disk_full = unchecked((int)0x80070027);
                const int hr_error_disk_full = unchecked((int)0x80070070);

                if (ioe.HResult is hr_error_handle_disk_full or hr_error_disk_full) return false;

                break;
        }

        return true;
    }

    #region IDisposable Impl

    ~SentryLogger() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool isDisposing)
    {
        Logger.NewEntry -= processLogEntry;
        sentrySession?.Dispose();
    }

    #endregion
}
