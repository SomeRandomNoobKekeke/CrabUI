using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CrabUI;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUIUser
{
  public partial class E2ETestPack
  {
    public partial class Console : CUIDefault.Frame, IE2ETest
    {
      public CUIVerticalList LogList { get; private set; }
      public CUITextInput Input { get; private set; }

      public int? SearchIndex = null;

      public void Initialize()
      {
        Absolute = new CUINullRect(w: 600, h: 400);
        Caption.Text = "Console";

        this["layout"]["logwrapper"] = new CUIVerticalList()
        {
          Flex = 1,
          Background = {
            Sprite = CUISprite.VerticalGradient with {
              Effects = SpriteEffects.FlipVertically,
            },
          },
          Scrollable = true,
          Direction = CUIDirection.Reverse,
          Style = (c) => c.Background.Color = c.Palette["panel"],
          Palette = CUICore.Palettes.Secondary,
        };

        this["layout"]["logwrapper"]["log"] = LogList = new CUIVerticalList()
        {
          FitContent = new CUIBool2(true, true),
        };

        this["layout"]["input"] = Input = new CUITextInput()
        {
          AbsoluteMin = new CUINullRect(h: 24),
        };

        Input.KeyPressed += HandleKeyPressed;

        OnClose += Dispose;

        Open();
      }

      public void HandleKeyPressed(CUIKeyPressedEvent e)
      {
        if (e.Key == Keys.Up || e.Key == Keys.Down)
        {
          if (LogList.Children.Count == 0) return;

          if (SearchIndex == null)
          {
            SearchIndex = e.Key == Keys.Up ? LogList.Children.Count - 1 : 0;
          }
          else
          {
            SearchIndex = e.Key == Keys.Up ? SearchIndex - 1 : SearchIndex + 1;
            SearchIndex = Math.Clamp(SearchIndex.Value, 0, LogList.Children.Count - 1);
          }

          Input.Text = (LogList.Children[SearchIndex.Value] as CUITextBlock).Text;
        }

        if (e.Key == Keys.Enter)
        {
          SearchIndex = null;

          string command = Input.Text.Trim();
          if (command == "") return;

          bool consoleWasClosed = DebugConsole.IsOpen == false;

          DebugConsole.ExecuteCommand(command);
          LogList.Add(new CUITextBlock(command) { TextAnchor = CUIAnchor.LeftCenter });
          Input.Text = "";

          if (consoleWasClosed) DebugConsole.IsOpen = false; //HACK shut 🐦👌
        }
      }


      public void Dispose()
      {

      }
    }
  }
}