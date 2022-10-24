using System;
using HoloCure.Launcher.Core;
using osu.Framework.Graphics;
using osu.Framework.Testing;

namespace HoloCure.Launcher.Game.Tests.Visual;

public class LauncherTestScene : TestScene
{
    protected override ITestSceneTestRunner CreateRunner() => new LauncherTestSceneTestRunner();

    private class LauncherTestSceneTestRunner : LauncherGameBase, ITestSceneTestRunner
    {
        protected override IStoreProvider StoreProvider { get; }

        private TestSceneTestRunner.TestRunner runner = null!;

        public LauncherTestSceneTestRunner()
        {
            StoreProvider = new LauncherTestSceneStoreProvider(LoadComponent);
        }

        protected override void LoadAsyncComplete()
        {
            base.LoadAsyncComplete();
            Add(runner = new TestSceneTestRunner.TestRunner());
        }

        public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);

        private class LauncherTestSceneStoreProvider : LauncherStoreProvider
        {
            public LauncherTestSceneStoreProvider(Action<Drawable> componentLoader)
                : base(componentLoader)
            {
            }

            protected override void InitializeFonts(CoreGame game)
            {
            }
        }
    }
}
