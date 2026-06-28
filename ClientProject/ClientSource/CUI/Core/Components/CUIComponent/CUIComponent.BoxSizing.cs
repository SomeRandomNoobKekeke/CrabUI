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
    public CUISizes Border { get; set; }


    private CUIRect _OuterRect; public CUIRect OuterRect
    {
      get => _OuterRect;
      set
      {
        _OuterRect = value;

        _Rect = new CUIRect(
          _OuterRect.Left + Margin.Left,
          _OuterRect.Top + Margin.Top,
          Math.Max(0, _OuterRect.Width - Margin.Left - Margin.Right),
          Math.Max(0, _OuterRect.Height - Margin.Top - Margin.Bottom)
        );

        _InnerRect = new CUIRect(
          _Rect.Left + Border.Left + Padding.Left,
          _Rect.Top + Border.Top + Padding.Top,
          Math.Max(0, _Rect.Width - Padding.Left - Border.Left - Border.Right - Padding.Right),
          Math.Max(0, _Rect.Height - Padding.Top - Border.Top - Border.Bottom - Padding.Bottom)
        );

        UpdateRects();
      }
    }

    private CUIRect _Rect; public override CUIRect Rect
    {
      get => _Rect;
      set
      {
        _Rect = value;

        _InnerRect = new CUIRect(
         _Rect.Left + Border.Left + Padding.Left,
         _Rect.Top + Border.Top + Padding.Top,
         Math.Max(0, _Rect.Width - Padding.Left - Border.Left - Border.Right - Padding.Right),
         Math.Max(0, _Rect.Height - Padding.Top - Border.Top - Border.Bottom - Padding.Bottom)
       );

        _OuterRect = new CUIRect(
          _Rect.Left - Margin.Left,
          _Rect.Top - Margin.Top,
          _Rect.Width + Margin.Left + Margin.Right,
          _Rect.Height + Margin.Top + Margin.Bottom
        );

        UpdateRects();
      }
    }

    private CUIRect _InnerRect; public CUIRect InnerRect
    {
      get => _InnerRect;
      //set;//TODO
    }
  }
}