using osu.Framework.Testing;

namespace HoloCure.Launcher.Game.Tests.Visual
{
    public class LauncherTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new LauncherTestSceneTestRunner();

        private class LauncherTestSceneTestRunner : LauncherGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner = null!;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
