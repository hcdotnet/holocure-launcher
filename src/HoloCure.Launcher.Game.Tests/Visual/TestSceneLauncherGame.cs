using osu.Framework.Allocation;
using osu.Framework.Platform;
using NUnit.Framework;

namespace HoloCure.Launcher.Game.Tests.Visual
{
    [TestFixture]
    public class TestSceneLauncherGame : LauncherTestScene
    {
        // Add visual tests to ensure correct behaviour of your game: https://github.com/ppy/osu-framework/wiki/Development-and-Testing
        // You can make changes to classes associated with the tests and they will recompile and update immediately.

        private LauncherGame game = null!;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            game = new LauncherGameTest();
            game.SetHost(host);

            AddGame(game);
        }
    }
}
