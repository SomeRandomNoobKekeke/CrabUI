using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;


namespace CrabUI
{
  public partial class CUIComponent
  {
    public SimpleTexture Background { get; } = new();

    protected CUIRect _Rect;
    public override CUIRect Rect
    {
      get => _Rect;
      set
      {
        _Rect = value;
        UpdateRect(value);
      }
    }

    [CUISerializable]
    public bool Visible { get; set; } = true;
    protected bool CulledOut { get; set; }


    protected virtual void UpdateRect(CUIRect rect)
    {
      Debug_PropSet.Send(typeof(CUIRect), rect, this, "Rect");
      Background.Rect = rect;
      RightResizeHandle.UpdateRect();

      if (CullChildren)
      {
        ScissorRect = rect.Box;
      }
    }

    protected Rectangle? ScissorRect
    {
      get => VisualBounds.ScissorRect;
      set => VisualBounds.ScissorRect = value;
    }
    protected VisualBounds VisualBounds { get; } = new();


    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Visible || CulledOut) yield break;

      yield return VisualWrappers.BackgroundWrapper;
      yield return VisualBounds.LeftBound; //TODO bounds should be yielded only if there's something non standart
      foreach (CUIComponent child in Tree.Children)
      {
        yield return child.VisualWrappers.SelfWrapper;
      }
      yield return VisualBounds.RightBound;
      yield return RightResizeHandle.SelfWrapper;
    }




    //Just optimization to not create new Wrappers every Frame
    protected VisualWrappers_Part VisualWrappers { get; } = new();
    public class VisualWrappers_Part : Part
    {
      public void Init()
      {
        BackgroundWrapper = new VisualUnit.PrimitiveVisualElement(Self.Background);
        SelfWrapper = new VisualUnit.NestedVisualComponent(Self);
      }
      public VisualUnit.NestedVisualComponent SelfWrapper { get; private set; }
      public VisualUnit.PrimitiveVisualElement BackgroundWrapper { get; private set; }
    }






  }
}