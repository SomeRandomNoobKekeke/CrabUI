using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;

namespace CrabUI
{
  public partial class CUIMainComponent
  {
    public class FocusHandle_Part : Part
    {

      public event Action<IFocusable> FocusRequested;
      public event Action<IFocusable> BlurRequested;

      public void RequestFocus(IFocusable focusable) => FocusRequested?.Invoke(focusable);
      public void RequestBlur(IFocusable focusable) => BlurRequested?.Invoke(focusable);
    }

    public FocusHandle_Part FocusHandle { get; } = new();
  }
}