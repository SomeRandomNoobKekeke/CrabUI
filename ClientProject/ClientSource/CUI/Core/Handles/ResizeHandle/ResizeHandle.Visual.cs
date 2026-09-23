using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using Microsoft.Xna.Framework.Graphics;

namespace CursedUI
{
  public partial class ResizeHandle : CUIVisualComponent
  {
    public SimpleTexture Background { get; } = new();

    public override bool Visible
    {
      get => Background.Visible;
      set => Background.Visible = value;
    }

    public override bool MouseOver => Background.MouseOver;
    public override bool MousePressed => Background.MousePressed;

    public override CUIRect OuterRect { get => Rect; set => Rect = value; }
    public override CUIRect ChildrenRect { get => Rect; set => Rect = value; }
    public CUIRect Rect
    {
      get => Background.Rect;
      set => Background.Rect = value;
    }


    //BRUH Why is this inverted, why not just set Rect from Host.UpdateRect?
    public void UpdateRect()
    {
      if (Host is null)
      {
        Rect = new CUIRect(Vector2.Zero, Absolute.Size);
      }
      else
      {
        Rect = new CUIRect(
          CUIAnchor.ChildPosIn(Host.Rect, Anchor, Absolute.Size),
          Absolute.Size
        );
      }
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (Displayed) yield return Background.VisualWrapper;
    }
  }
}