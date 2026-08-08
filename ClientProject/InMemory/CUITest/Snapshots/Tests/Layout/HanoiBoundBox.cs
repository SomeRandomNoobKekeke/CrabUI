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

namespace CrabUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Layout
    {
      public static CUIComponent HanoiBoundBox()
      {
        CUIFrame frame = new()
        {
          Background = { Color = new Color(0, 0, 64) },
          Relative = new CUINullRect(0.1f, 0.1f, 0.8f, 0.8f),
          Anchor = CUIAnchor.LeftTop,
        };

        CUIComponent parent = frame;
        for (int i = 0; i < 10; i++)
        {
          CUIComponent next = new CUIComponent()
          {
            Background = { Color = new Color(255, 255, 255, 32) },
            Relative = new CUINullRect(0.1f, 0.1f, 0.8f, 0.8f),
            Draggable = true,
          };

          next.ChildrenBounds = (rect) => new CUIBoundaries(
            rect.Left + rect.Width * 0.05f,
            rect.Right - rect.Width * 0.05f,
            rect.Top + rect.Height * 0.05f,
            rect.Bottom - rect.Height * 0.05f
          );

          parent.Children.Add(next);
          parent = next;
        }

        return frame;
      }
    }
  }
}