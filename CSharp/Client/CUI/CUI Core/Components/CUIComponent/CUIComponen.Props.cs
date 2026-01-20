using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIComponent
  {
    public List<CUIProp> AllCUIProps = new();

    public CUIPropsWrapper CUIProps = new();

    private void WireUpProps()
    {
      CUIProps.Rect.OnValueSet = (rect) =>
      {
        Background.Rect = rect.Box;
      };
    }

    public class CUIPropsWrapper
    {
      public CUILayoutProp<CUINullRect> Absolute { get; set; } = new()
      {
        Pattern = LayoutMarkPattern.ParentChanged,
      };
      public CUILayoutProp<CUINullRect> Relative { get; set; } = new()
      {
        Pattern = LayoutMarkPattern.ParentChanged,
      };
      public CUILayoutProp<CUIRect> Rect { get; set; } = new()
      {
        Pattern = LayoutMarkPattern.ParentChanged,
      };
    }
  }
}