using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUITextLine : CUIComponent, IComponent
  {
    public TextLine TextLine { get; } = new();
    public string Text
    {
      get => TextLine.Text;
      set => TextLine.Text = value;
    }



    protected override void UpdateRects()
    {
      base.UpdateRects();
      TextLine.Position = Rect.LeftTop;
    }

    [CUISerializable]
    public override bool Visible
    {
      get => Background.Visible;
      set
      {
        Background.Visible = value;
        TextLine.Visible = value;
      }
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return Background.VisualWrapper;
      yield return TextLine.VisualWrapper;
      foreach (CUIComponent child in Tree.Children)
      {
        yield return child.VisualWrapper;
      }
    }

  }
}