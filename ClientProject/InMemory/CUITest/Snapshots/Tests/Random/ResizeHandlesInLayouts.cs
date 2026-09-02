using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;

namespace CursedUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Random
    {
      public static CUIComponent ResizeHandlesInLayouts()
      {
        CUIComponent CreateResizeAbomination()
        {
          CUIComponent component = new CUIDefault.Panel()
          {
            // Anchor = CUIAnchor.Center,
            Absolute = new CUINullRect(w: 200, h: 200),
            ResizableBoth = true,
            Draggable = false,
            AbsoluteMin = new CUINullRect(ResizeHandle.DefaultSize),
            Palette = CUICore.Palettes.Quaternary,
          };

          component.Children.Add(ResizeHandle.CreateAngle(0.0f, 0.0f));
          component.Children.Add(ResizeHandle.CreateAngle(1.0f, 0.0f));


          component.Children.Add(ResizeHandle.CreateAngle(0.3f, 0.3f));
          component.Children.Add(ResizeHandle.CreateAngle(0.7f, 0.3f));
          component.Children.Add(ResizeHandle.CreateAngle(0.7f, 0.7f));
          component.Children.Add(ResizeHandle.CreateAngle(0.3f, 0.7f));


          component.Children.Add(ResizeHandle.CreateAngle(-0.5f, -0.5f));
          component.Children.Add(ResizeHandle.CreateAngle(1.5f, -0.5f));
          component.Children.Add(ResizeHandle.CreateAngle(1.5f, 1.5f));
          component.Children.Add(ResizeHandle.CreateAngle(-0.5f, 1.5f));

          using (new CUIContextStyle<ResizeHandle>(c =>
          {
            c.Background.Sprite = CUISprite.Vignette;
            c.Absolute = new CUINullRect(w: 20, h: 60);
            c.OnlyHorizontal = true;
            c.Anchor = CUIAnchor.Center;
          }))
          {
            component.Children.Add(new ResizeHandle() { ParentAnchor = new Vector2(0.0f, 0.5f) });
            component.Children.Add(new ResizeHandle() { ParentAnchor = new Vector2(1.0f, 0.5f) });

            component.Children.Add(new ResizeHandle() { ParentAnchor = new Vector2(0.3f, 0.5f) });
            component.Children.Add(new ResizeHandle() { ParentAnchor = new Vector2(0.7f, 0.5f) });
          }

          using (new CUIContextStyle<ResizeHandle>(c =>
          {
            c.Background.Sprite = CUISprite.Vignette;
            c.Absolute = new CUINullRect(w: 60, h: 20);
            c.OnlyVertical = true;
          }))
          {
            component.Children.Add(new ResizeHandle(0.5f, 0.0f));
            component.Children.Add(new ResizeHandle(0.5f, 1.0f));

            component.Children.Add(new ResizeHandle(0.5f, 0.3f));
            component.Children.Add(new ResizeHandle(0.5f, 0.7f));
          }

          return component;
        }


        CUIFrame frame = new CUIDefault.Frame("ResizeHandlesInLayouts", 400, 600);

        frame["layout"]["button1"] = new CUIButton("button");
        frame["layout"]["abomination"] = CreateResizeAbomination();
        frame["layout"]["button2"] = new CUIButton("button");

        return frame;
      }
    }
  }
}