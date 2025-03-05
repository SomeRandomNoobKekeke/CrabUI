using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System.Xml;
using System.Xml.Linq;
using HarmonyLib;

namespace CrabUI
{
  public partial class CUIComponent : IDisposable
  {
    private void SetupStyles()
    {
      Style = new CUIStyle();
    }

    /// <summary>
    /// Use it to e.g. update component color
    /// </summary>
    public event Action OnStyleApplied;
    internal void InvokeOnStyleApplied() => OnStyleApplied?.Invoke();

    private void HandleStylePropChange(string key, string value)
    {
      CUIGlobalStyleResolver.OnComponentStylePropChanged(this, key);
    }
    private void HandleStyleChange(CUIStyle s)
    {
      CUIGlobalStyleResolver.OnComponentStyleChanged(this);
    }

    private CUIStyle style;
    /// <summary>
    /// Allows you to assing parsable string or link to CUIPalette to any prop  
    /// It's indexable, so you can access it like this: component.Style["BackgroundColor"] = "cyan"  
    /// if value starts with "CUIPalette." it will extract the value from palette  
    /// e.g. component.Style["BackgroundColor"] = "CUIPalette.DarkBlue.Secondary.On"  
    /// </summary>
    [CUISerializable]
    public CUIStyle Style
    {
      get => style;
      set
      {
        if (style == value) return;

        if (style != null)
        {
          style.OnUse -= HandleStyleChange;
          style.OnPropChanged -= HandleStylePropChange;
        }

        style = value;

        if (style != null)
        {
          style.OnUse += HandleStyleChange;
          style.OnPropChanged += HandleStylePropChange;
        }

        HandleStyleChange(style);
      }
    }

    public CUIStyle ResolvedStyle { get; set; }
  }
}