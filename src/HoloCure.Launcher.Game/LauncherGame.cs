using HoloCure.Launcher.Game.Updater;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace HoloCure.Launcher.Game
{
    public abstract class LauncherGame : LauncherGameBase
    {
        private ScreenStack screenStack = null!;

        #region Dependencies

        /// <summary>
        ///     Set by <see cref="CreateChildDependencies"/>, exposes access to the <see cref="DependencyContainer"/> instance used by this type in the hierarchy.
        /// </summary>
        private DependencyContainer dependencies = null!;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        #endregion

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.CacheAs(this);

            // Add your top-level game components here.
            // A screen stack and sample screen has been provided for convenience, but you can replace it if you don't want to use screens.
            Child = screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            // Schedule to be ran after LoadComplete finishes.
            Schedule(() =>
            {
                if (CreateUpdateManager()?.AsDrawable() is { } updateManager)
                {
                    dependencies.CacheAs(updateManager);
                    Add(updateManager);
                }
            });

            screenStack.Push(new MainScreen());
        }

        /// <summary>
        ///     Creates an <see cref="IUpdateManager"/> instance that will handle notifying the user of *launcher* updates, and applying them if possible.
        /// </summary>
        /// <remarks>
        ///     This has nothing to do with updating installations handled by this launcher.
        /// </remarks>
        protected abstract IUpdateManager? CreateUpdateManager();
    }
}
