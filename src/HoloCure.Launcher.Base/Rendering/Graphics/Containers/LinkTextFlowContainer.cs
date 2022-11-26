/*// Copyright (c) Tomat. Licensed under the GPL v3 License.
// See the LICENSE-GPL file in the repository root for full license text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Framework.Platform;

namespace HoloCure.Launcher.Base.Rendering.Graphics.Containers;

public class LinkFlowContainer : LauncherTextFlowContainer
{
    [Resolved]
    private GameHost host { get; set; } = null!;

    public virtual DrawableLinkCompiler CreateLinkCompiler(ITextPart textPart) => new(textPart);

    public override IEnumerable<Drawable> FlowingChildren => base.FlowingChildren.Where(c => c is not DrawableLinkCompiler);

    public virtual void AddLink(LocalisableString text, string url, Action<SpriteText>? creationParameters = null) => createLink();

    protected virtual void CreateLink(ITextPart textPart, string url, LocalisableString tooltipText, Action? action = null)
    {
        var onClickAction = () =>
        {
            if (action is not null)
                action();
            else
                host.OpenUrlExternally(url);
        };

        AddPart(new TextLink(textPart, tooltipText, onClickAction));
    }

    private class TextLink : TextPart
    {
        private readonly ITextPart innerPart;
        private readonly LocalisableString tooltipText;
        private readonly Action action;

        public TextLink(ITextPart innerPart, LocalisableString tooltipText, Action action)
        {
            this.innerPart = innerPart;
            this.tooltipText = tooltipText;
            this.action = action;
        }

        protected override IEnumerable<Drawable> CreateDrawablesFor(TextFlowContainer textFlowContainer)
        {
            var linkFlowContainer = (LinkFlowContainer)textFlowContainer;

            innerPart.RecreateDrawablesFor(linkFlowContainer);
            var drawables = innerPart.Drawables.ToList();

            drawables.Add(linkFlowContainer.);
        }

        protected virtual DrawableLink
    }
}
*/
