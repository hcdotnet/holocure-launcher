// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System.Globalization;
using HoloCure.Launcher.Base.Core.Localization;
using HoloCure.Launcher.Base.Core.Localization.Fluent;
using Linguini.Bundle.Builder;
using NUnit.Framework;

namespace HoloCure.Launcher.Game.Tests.Localization;

[TestFixture]
public class MultiSourcedFluentBundleFallbackTest
{
    [Test]
    public void TestResourceFallback()
    {
        var enBundle = LinguiniBuilder.Builder()
                                      .CultureInfo(new CultureInfo(LanguageCode.en.Code))
                                      .AddResources("test = English", "test2 = English")
                                      .SetUseIsolating(false)
                                      .UncheckedBuild();

        var jpBundle = LinguiniBuilder.Builder()
                                      .CultureInfo(new CultureInfo(LanguageCode.ja.Code))
                                      .AddResource("test = 日本語")
                                      .SetUseIsolating(false)
                                      .UncheckedBuild();

        var english = new FluentMultiBundle(enBundle);
        var japanese = new FluentMultiBundle(jpBundle, enBundle);

        // Preliminary - ensure that these bundles actually contain the expected messages.
        // This should always be true, but hey, these are tests - might as well.
        Assert.AreEqual(true, english.HasMessage("test"));
        Assert.AreEqual(true, english.HasMessage("test2"));
        Assert.AreEqual(true, japanese.HasMessage("test"));
        Assert.AreEqual(true, japanese.HasMessage("test2"));

        // English bundle tests.
        //  test = English
        //  test2 = English
        //  test != 日本語
        //  test2 != 日本語
        Assert.AreEqual("English", english.GetMsg("test", null));
        Assert.AreEqual("English", english.GetMsg("test2", null));
        Assert.AreNotEqual("日本語", english.GetMsg("test", null));
        Assert.AreNotEqual("日本語", english.GetMsg("test2", null));

        // Japanese bundle tests.
        //  test = 日本語
        //  test2 = English
        //  test != English
        //  test2 != 日本語
        Assert.AreEqual("日本語", japanese.GetMsg("test", null));
        Assert.AreEqual("English", japanese.GetMsg("test2", null));
        Assert.AreNotEqual("English", japanese.GetMsg("test", null));
        Assert.AreNotEqual("日本語", japanese.GetMsg("test2", null));
    }
}
