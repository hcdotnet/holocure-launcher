// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HoloCure.Launcher.Base.Games;
using HoloCure.Launcher.Base.Graphics.Containers;
using HoloCure.Launcher.Base.Graphics.Screens;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using osuTK;

namespace HoloCure.Launcher.Base.Graphics.UI.Screens.Paneled;

public class GameLauncherScreen : LauncherScreen
{
    private readonly Game game;
    private ActionableStaticButton playButton = null!;
    private ActionableStaticButton updateButton = null!;

    public GameLauncherScreen(Game game)
    {
        this.game = game;
    }

    [BackgroundDependencyLoader]
    private void load(TextureStore textures, Storage storage, GameProvider gameProvider)
    {
        InternalChildren = new Drawable[]
        {
            new Sprite
            {
                Origin = Anchor.TopCentre,
                Anchor = Anchor.TopCentre,

                Texture = textures.Get(game.GameTitlePath),

                Position = new Vector2(0f, 24f)
            },
            playButton = new ActionableStaticButton
            {
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,

                Position = new Vector2(0f, -30f),

                Action = () =>
                {
                    void handleAlert(GameAlert alert)
                    {
                        switch (alert)
                        {
                            case GameAlert.CheckingInstallation:
                                playButton.UpdateText("Checking installation...");
                                Schedule(() =>
                                {
                                    playButton.Enabled.Value = false;
                                    updateButton.Enabled.Value = false;
                                });
                                break;

                            case GameAlert.InstallationNotFoundInstallingGame:
                                playButton.UpdateText("Installing game...");
                                break;

                            case GameAlert.InstallationFoundStartingGame:
                                playButton.UpdateText("Starting game...");
                                break;

                            case GameAlert.GameStarted:
                                playButton.UpdateText("Game started!");
                                gameProvider.PlayingGame.Value = game; /* TEMPORARY  */
                                break;

                            case GameAlert.GameExited:
                                playButton.UpdateText("Play Game");
                                playButton.Enabled.Value = true;
                                updateButton.Enabled.Value = true;
                                gameProvider.PlayingGame.Value = null; /* TEMPORARY  */
                                break;

                            case GameAlert.CheckingForUpdates:
                            case GameAlert.NoUpdatesFound:
                            case GameAlert.UpdatingGame:
                            case GameAlert.GameUpdated:
                                break;

                            default:
                                throw new ArgumentOutOfRangeException(nameof(alert), alert, null);
                        }
                    }

                    Task.Run(async () => await game.InstallOrPlayGameAsync(handleAlert, storage));
                }
            },
            updateButton = new ActionableStaticButton
            {
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,

                Position = new Vector2(0f, 30f),

                Action = () =>
                {
                    void handleAlert(GameAlert alert)
                    {
                        switch (alert)
                        {
                            case GameAlert.CheckingForUpdates:
                                updateButton.UpdateText("Checking for updates...");
                                Schedule(() =>
                                {
                                    playButton.Enabled.Value = false;
                                    updateButton.Enabled.Value = false;
                                });
                                break;

                            case GameAlert.NoUpdatesFound:
                                updateButton.UpdateText("No updates found.");
                                Schedule(() => { playButton.Enabled.Value = true; });
                                Scheduler.AddDelayed(
                                    () =>
                                    {
                                        updateButton.Enabled.Value = true;
                                        updateButton.UpdateText("Update");
                                    },
                                    5 * 1000
                                );
                                break;

                            case GameAlert.UpdatingGame:
                                updateButton.UpdateText("Installing update...");
                                break;

                            case GameAlert.GameUpdated:
                                updateButton.UpdateText("Update installed!");
                                Schedule(() => { playButton.Enabled.Value = true; });
                                Scheduler.AddDelayed(
                                    () =>
                                    {
                                        updateButton.Enabled.Value = true;
                                        updateButton.UpdateText("Update");
                                    },
                                    5 * 1000
                                );
                                break;

                            case GameAlert.CheckingInstallation:
                            case GameAlert.InstallationNotFoundInstallingGame:
                            case GameAlert.InstallationFoundStartingGame:
                            case GameAlert.GameExited:
                                break;

                            default:
                                throw new ArgumentOutOfRangeException(nameof(alert), alert, null);
                        }
                    }

                    Task.Run(async () => await game.UpdateGameAsync(handleAlert, storage));
                }
            }
        };

        playButton.UpdateText("Play Game");
        updateButton.UpdateText("Update Game");
    }

    private class ActionableStaticButton : LauncherHoverContainer
    {
        [Resolved]
        private LauncherTheme theme { get; set; } = null!;

        protected override IEnumerable<Drawable> EffectTargets => box.Yield();

        private LauncherTextFlowContainer textContainer = null!;
        private Box box = null!;

        [BackgroundDependencyLoader]
        private void load()
        {
            Enabled.Value = true;

            Height = 50;
            Width = 150;

            HoverColor = theme.ReturnButtonHoverColour;
            IdleColor = theme.ReturnButtonIdleColour;

            Masking = true;
            CornerRadius = 10f;

            textContainer = new LauncherTextFlowContainer
            {
                AutoSizeAxes = Axes.Both,

                TextAnchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,

                Margin = new MarginPadding(10f)
            };

            InternalChildren = new Drawable[]
            {
                box = new Box
                {
                    Colour = IdleColor,
                    RelativeSizeAxes = Axes.Both
                },
                textContainer
            };

            Enabled.Value = true;
        }

        protected override bool OnHover(HoverEvent e)
        {
            this.ScaleTo(1.1f, 200D, Easing.Out);

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            this.ScaleTo(1f, 200D, Easing.In);

            base.OnHoverLost(e);
        }

        public void UpdateText(LocalisableString text)
        {
            Schedule(() => textContainer.Text = text);
            return;

            lock (textContainer)
            {
                textContainer.Clear(true);
                textContainer.Text = text;
            }
        }
    }
}
