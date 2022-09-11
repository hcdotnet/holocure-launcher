using osu.Framework.Graphics;
using NUnit.Framework;

namespace HoloCure.Launcher.Game.Tests.Visual
{
    [TestFixture]
    public class TestSceneSpinningBox : LauncherTestScene
    {
        // Add visual tests to ensure correct behaviour of your game: https://github.com/ppy/osu-framework/wiki/Development-and-Testing
        // You can make changes to classes associated with the tests and they will recompile and update immediately.

        public TestSceneSpinningBox()
        {
            Add(new SpinningBox
            {
                Anchor = Anchor.Centre,
            });
        }
    }
}
