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
namespace CursedUI
{
  public partial class CUIComponent
  {
    [CUISerializableProp]
    public bool Resizable
    {
      get => RightResizeHandle.Displayed;
      set => RightResizeHandle.Displayed = value;
    }

    public ResizeHandle RightResizeHandle { get; } = new();
  }
}