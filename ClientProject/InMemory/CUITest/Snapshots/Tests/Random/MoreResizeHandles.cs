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
      public static CUIComponent MoreResizeHandles()
      {
        CUIComponent component = new CUIDefault.Panel()
        {
          Anchor = CUIAnchor.Center,
          // Absolute = new CUINullRect(w: 400, h: 400),
          Relative = new CUINullRect(w: 0.3f, h: 0.3f),
          ResizableBoth = true,
          Draggable = true,
          AbsoluteMin = new CUINullRect(ResizeHandle.DefaultSize),
          ResizeRelative = true,
        };

        component.Children.Add(ResizeHandle.CreateAngle(0.0f, 0.0f));


        ResizeHandle handle = ResizeHandle.CreateAngle(1.0f, 0.0f);
        handle.Background.SensorExpansion = new CUISizes(30, 30, 30, 30);
        component.Children.Add(handle);


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
        }))
        {
          component.Children.Add(new ResizeHandle(0.0f, 0.5f));
          component.Children.Add(new ResizeHandle(1.0f, 0.5f));

          component.Children.Add(new ResizeHandle(0.3f, 0.5f));
          component.Children.Add(new ResizeHandle(0.7f, 0.5f));
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

        component["mode"] = new CUIToggleButton("mode")
        {
          Anchor = CUIAnchor.Center,
          OnToggle = (state) =>
          {
            component.ResizeRelative = state;
            if (state) component.Absolute = new CUINullRect();
          },
          State = true,
        };


        component.Resized += (rect) =>
        {
          CUI.Logger.LogVars(component.Absolute, component.Relative);
        };

        return component;
      }
    }
  }
}