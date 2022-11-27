// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

/*using System.Collections.Generic;
using System.Linq;
using HoloCure.Launcher.Base.Core.Localization;
using HoloCure.Launcher.Base.Rendering.Graphics;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace HoloCure.Launcher.Game.Tests.Visual;

public class TestLanguageDisplayList : LauncherTestScene
{
    [Resolved]
    private Languages languages { get; set; } = null!;

    private ScrollContainer<Drawable> scrollContainer = null!;

    [SetUp]
    public void Setup() => Schedule(Clear);

    [TestCase("en")]
    [TestCase("ja")]
    [TestCase("this locale does not exist")]
    public void DisplayLanguageListTest(string selectedLocale)
    {
        AddStep(
            "Create scroll container",
            () =>
            {
                Add(scrollContainer = new BasicScrollContainer
                {
                });
            }
        );
    }

    [BackgroundDependencyLoader]
    private void load(LauncherColor colors)
    {
        IEnumerable<Drawable> makeContentFromLanguages(Languages langs) =>
            langs.Stores.Select(x =>
            {
                return new TextFlowContainer(t => t.Font = LauncherFont.GetFont(size: 12, weight: FontWeight.SemiBold))
                {
                    AutoSizeAxes = Axes.Y,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                }.With(text =>
                {
                    text.AddText(x.Get("localized-name"));
                    text.AddText(" ");
                    text.AddText(
                        $"({x.LangCode.Name}/{x.LangCode.Code})",
                        t =>
                        {
                            t.Colour = colors.GrayC;
                            t.Font = LauncherFont.GetFont(size: 12, weight: FontWeight.Regular);
                        }
                    );

                    if (x.LangCode.Code == langs.DefaultLanguage.Code)
                    {
                        text.AddText(" ");
                        text.AddText(
                            "[default]",
                            t =>
                            {
                                t.Colour = colors.Gray8;
                                t.Font = LauncherFont.GetFont(size: 12, weight: FontWeight.Light);
                            }
                        );
                    }
                });
            });

        var fill = new FillFlowContainer();
        Child = scrollContainer = new BasicScrollContainer
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Size = new Vector2(500, 200),
            Child = fill
        };

        fill.Children = makeContentFromLanguages(languages).ToArray();
    }
}*/
