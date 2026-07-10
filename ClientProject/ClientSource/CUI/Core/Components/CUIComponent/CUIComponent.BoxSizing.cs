using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUIComponent
  {
    private CUISizes _Margin; public CUISizes Margin
    {
      get => _Margin;
      set
      {
        _Margin = value;
        UpdateSizeDiffs();
      }
    }

    private CUISizes _Padding; public CUISizes Padding
    {
      get => _Padding;
      set
      {
        _Padding = value;
        UpdateSizeDiffs();
      }
    }

    public CUISizes Border
    {
      get => Borders.Sizes;
      set
      {
        Borders.Sizes = value;
        UpdateSizeDiffs();
      }
    }

    private void UpdateSizeDiffs()
    {
      OutToChildDiff = Margin + Border + Padding;
    }

    public CUISizes OutToChildDiff { get; private set; }



    private CUIRect _OuterRect; public CUIRect OuterRect
    {
      get => _OuterRect;
      set
      {
        _OuterRect = value;
        _Rect = _OuterRect - Margin;
        _InnerRect = _Rect - Border;
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
        _InnerRect = _Rect - Border;
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
        _Rect = _InnerRect + Border;
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
        _Rect = _InnerRect + Border;
        _OuterRect = _Rect + Margin;

        UpdateRects();
      }
    }
  }
}