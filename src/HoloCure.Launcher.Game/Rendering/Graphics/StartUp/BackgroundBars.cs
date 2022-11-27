// // Copyright (c) Tomat. Licensed under the GPL v3 License.
// // See the LICENSE-GPL file in the repository root for full license text.
//
// using System;
// using System.Collections.Generic;
// using osu.Framework.Allocation;
// using osu.Framework.Graphics;
// using osu.Framework.Graphics.Colour;
// using osu.Framework.Graphics.Containers;
// using osu.Framework.Graphics.Rendering;
// using osu.Framework.Graphics.Shapes;
// using osu.Framework.Graphics.Textures;
// using osuTK;
//
// namespace HoloCure.Launcher.Graphics.StartUp
// {
//     public class BackgroundBars : CompositeDrawable
//     {
//         private static readonly Colour4 bar_top_color = new(255, 255, 255, 255);
//         private static readonly Colour4 bar_bottom_color = new(255, 255, 255, 0);
//         private const int destroy_width = 300;
//         private const int separation_width = 15;
//         private const int pixels_per_second = 60;
//
//         private const double move_length = (1 / 30.0) * 1000;
//
//         private readonly List<BoxBar> bars = new();
//         private Texture bgBarTexture = null!;
//
//         private int barCount => (int)Math.Ceiling(DrawWidth / separation_width);
//
//         [BackgroundDependencyLoader]
//         private void load(IRenderer renderer)
//         {
//             //bgBarTexture = textures.Get("StartUp/BackgroundBar");
//             bgBarTexture = renderer.WhitePixel;
//         }
//
//         protected override void Update()
//         {
//             base.Update();
//
//             Invalidate(Invalidation.DrawNode);
//
//             addBars(false);
//
//             float elapsedSeconds = (float)Time.Elapsed / 1000f;
//             float movedDistance = -elapsedSeconds * pixels_per_second / (DrawWidth);
//
//             for (int i = 0; i < bars.Count; i++)
//             {
//                 BoxBar bar = bars[i];
//                 bar.OriginPosition = new Vector2(bar.OriginPosition.X - movedDistance, bar.OriginPosition.Y);
//
//                 float leftPos = bars[i].X / DrawWidth;
//
//                 if (leftPos < 0)
//                 {
//                     bars.RemoveAt(i);
//                     RemoveInternal(bar, true);
//                 }
//             }
//         }
//
//         protected override void LoadComplete()
//         {
//             base.LoadComplete();
//
//             addBars(true);
//         }
//
//         private void addBars(bool firstPopulation)
//         {
//             if (firstPopulation) bars.Clear();
//
//             int count = barCount;
//
//             for (int i = 0; i < count - bars.Count; i++)
//             {
//                 BoxBar bar = createBar(firstPopulation ? i * 15 : DrawWidth);
//                 bars.Add(bar);
//                 AddInternal(bar);
//             }
//         }
//
//         private BoxBar createBar(float x) =>
//             new()
//             {
//                 Width = 4,
//                 Height = 100,
//                 Rotation = 22.5f,
//                 Colour = ColourInfo.GradientVertical(bar_top_color, bar_bottom_color),
//                 OriginPosition = new Vector2(x, 0),
//                 Anchor = Anchor.BottomRight
//             };
//
//         private class BoxBar : Box
//         {
//             protected override void Update()
//             {
//                 base.Update();
//             }
//         }
//     }
// }


