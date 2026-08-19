using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;
using Microsoft.Xna.Framework;
namespace CrabUI
{

  public partial class CUICore
  {
    public class FocusHandle_Part : Part
    {
      public List<IFocusable> RequestedFocus { get; } = new();
      public List<IFocusable> RequestedBlur { get; } = new();

      public void RequestFocus(IFocusable focusable)
      {
        RequestedFocus.Add(focusable);
      }
      public void RequestBlur(IFocusable focusable)
      {
        RequestedBlur.Add(focusable);
      }

      public void Reset()
      {
        RequestedFocus.Clear();
        RequestedBlur.Clear();
      }

      public void ResolveFocus()
      {

      }
    }

    private FocusHandle_Part FocusHandle { get; } = new();
  }
}