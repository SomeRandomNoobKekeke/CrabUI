using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;
using System.IO;

namespace CrabUIUser
{
  public class TestManagerGUI : CUIFrame
  {
    public CUIButton OpenButton { get; } = new CUIButton()
    {
      Absolute = new CUINullRect(w: 50, h: 30),
      MasterColorOpaque = new Color(0, 0, 128),
      Anchor = CUIAnchor.RightCenter,
      Text = "Test",
      TextColor = Color.White,
    };

    public CUIFrame Frame { get; } = new CUIFrame()
    {
      Absolute = new CUINullRect(w: 300, h: 400),
      BackgroundColor = Color.Blue,
      Anchor = CUIAnchor.RightCenter,
      // Draggable = false,

      NamedChildren = new()
      {
        ["layout"] = new CUIVerticalList()
        {
          Relative = new CUINullRect(0, 0, 1, 1),
          NamedChildren = new()
          {
            ["header"] = new CUIHorizontalList()
            {
              Direction = CUIDirection.Reverse,
              Absolute = new CUINullRect(h: 30),
              BackgroundColor = new Color(32, 32, 32),
              NamedChildren = new()
              {
                ["close"] = new CUIButton()
                {
                  Text = "X",
                  MasterColorOpaque = new Color(255, 0, 0),
                  Absolute = new CUINullRect(w: 30, h: 30),
                }
              }
            }
          }
        },
      },
    };

    public bool IsOpen
    {
      get => Frame.Parent != null;
      set
      {
        if (value)
        {
          OpenButton.RemoveSelf();
          Frame.Open();
        }
        else
        {
          CUI.Main.Append(OpenButton);
          Frame.Close();
        }
      }
    }

    public TestManagerGUI()
    {
      OpenButton.MouseDown += (c, e) => IsOpen = true;


    }

    public void Init()
    {
      CUI.Main.Append(OpenButton);
    }

    public void Dispose()
    {

    }

  }
}