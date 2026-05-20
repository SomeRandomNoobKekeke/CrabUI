using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  public class ResizeHandle : CUIVisualComponent, IModule
  {
    private IResizable host;
    [In]
    public IResizable Host
    {
      get => host;
      set
      {
        if (host is not null) DisconnectFromHost(host);
        host = value;
        if (host is not null) ConnectToHost(host);
      }
    }

    public SimpleTexture Background { get; } = new();

    public Vector2 Anchor { get; set; } = new Vector2(1, 1);
    // public Vector2 ParentAnchor { get; set; }
    public Vector2 Size { get; set; } = new Vector2(15, 10);


    public override CUIRect Rect
    {
      get => Background.Rect;
      set => Background.Rect = value;
    }

    private void ConnectToHost(IResizable host)
    {
      UpdateRect();
    }

    private void DisconnectFromHost(IResizable host)
    {

    }

    public void UpdateRect()
    {
      if (Host is null)
      {
        Rect = new CUIRect(Vector2.Zero, Size);
      }
      else
      {
        Rect = new CUIRect(
          CUIAnchor.ChildPosIn(Host.Rect, Anchor, Size),
          Size
        );
      }
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      yield return new VisualUnit.PrimitiveVisualElement(Background);
    }

    public ResizeHandle()
    {
      Background.Color = Color.Yellow;
    }
  }
}