using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using Barotrauma.Extensions;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;

namespace CursedUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Dynamic
    {
      public static CUIComponent TextProps()
      {
        DynamicContainer container = new DynamicContainer() { FPS = 5 };

        CUIComponent frame = container["frame"] = new CUIDefault.Frame("TextProps", 800, 800);

        CUIComponent CreateBlock<T>(float x, float y) where T : CUIComponent, ITextComponent, new()
        {
          CUIComponent wrapper = new CUIComponent()
          {
            Absolute = new CUINullRect(x: x, y: y),
            Anchor = CUIAnchor.Center,
          };

          wrapper["color"] = new T()
          {
            Text = "changing color",
            Anchor = CUIAnchor.Center,
            Absolute = new CUINullRect(y: 0),
          };

          wrapper["text"] = new T()
          {
            Absolute = new CUINullRect(y: 50),
            Anchor = CUIAnchor.Center,
            Background = { Color = Color.Blue }
          };

          wrapper["size"] = new T()
          {
            Text = "changing text size",
            Absolute = new CUINullRect(y: 100),
            Anchor = CUIAnchor.Center,
            Background = { Color = Color.Blue }
          };

          wrapper["anchor"] = new T()
          {
            Text = "anchor",
            Absolute = new CUINullRect(y: 150, w: 200, h: 50),
            Anchor = CUIAnchor.Center,
            Background = { Color = Color.Blue }
          };

          wrapper.Actions["update"] = () =>
          {
            wrapper.Get<CUIComponent>("color").Background.Color = CUIColor.Random;
            wrapper.Get<ITextComponent>("color").TextColor = CUIColor.Random;

            wrapper.Get<ITextComponent>("text").Text = CUI.Random.RandomString(CUI.Random.Range(1, 16));

            wrapper.Get<ITextComponent>("size").Scale = CUI.Random.Range(0.5f, 2.0f);

            wrapper.Get<ITextComponent>("anchor").TextAnchor = CUI.Random.Vector2();
          };

          return wrapper;

        }


        frame["text blocks"] = CreateBlock<CUITextBlock>(0, 0);
        frame["buttons"] = CreateBlock<CUIButton>(-250, 0);
        frame["toggle buttons"] = CreateBlock<CUIButton>(250, 0);

        container.Updated += () =>
        {
          frame["text blocks"].Do("update");
          frame["buttons"].Do("update");
          frame["toggle buttons"].Do("update");
        };

        return container;
      }
    }
  }
}