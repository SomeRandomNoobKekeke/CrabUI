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
  public partial class CUIComponent
  {
    public CUISizes Margin { get; set; }
    public CUISizes Padding { get; set; }
    public CUISizes BorderSizes
    {
      get => Borders.Sizes;
      set => Borders.Sizes = value;
    }

    private CUIRect _OuterRect; public CUIRect OuterRect
    {
      get => _OuterRect;
      set
      {
        _OuterRect = value;
        _Rect = _OuterRect - Margin;
        _InnerRect = _Rect - BorderSizes;
        _ChildrenRect = _InnerRect - Padding;

        UpdateRects();
      }
    }

    private CUIRect _Rect; public override CUIRect Rect
    {
      get => _Rect;
      set
      {
        _Rect = value;
        _InnerRect = _Rect - BorderSizes;
        _ChildrenRect = _InnerRect - Padding;
        _OuterRect = _Rect + Margin;

        UpdateRects();
      }
    }

    private CUIRect _InnerRect; public CUIRect InnerRect
    {
      get => _InnerRect;
      set
      {
        _InnerRect = value;
        _ChildrenRect = _InnerRect - Padding;
        _Rect = _InnerRect + BorderSizes;
        _OuterRect = _Rect + Margin;

        UpdateRects();
      }
    }

    private CUIRect _ChildrenRect; public CUIRect ChildrenRect
    {
      get => _ChildrenRect;
      set
      {
        _ChildrenRect = value;
        _InnerRect = _ChildrenRect + Padding;
        _Rect = _InnerRect + BorderSizes;
        _OuterRect = _Rect + Margin;

        UpdateRects();
      }
    }
  }
}