using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;
using Barotrauma.Extensions;

namespace CursedUI
{
  [GeneratedComponent]
  public abstract partial class CUIButtonBase : CUIComponent, IComponent
  {
    public class Part : IPart { public CUIButtonBase Self { get; set; } }

    public abstract Color MasterColor { set; }
    public abstract void DetermineColor();


    public bool PlaySound { get; set; } = true;
    public GUISoundType ClickSound { get; set; } = GUISoundType.Select;//TODO don't reference it directly? there should be some cui sound manager
    public string Emit { get; set; }
  }
}