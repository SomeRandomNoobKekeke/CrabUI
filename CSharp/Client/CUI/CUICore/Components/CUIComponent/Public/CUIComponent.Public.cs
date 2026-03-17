using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUIComponent : IComponent
  {
    public CUIMainComponent MainComponent => MainComponentTracker.MainComponent;
    public CUIComponent Parent => Tree.Parent;


    public IReadOnlyList<CUIComponent> Children { get; }
    public void AddChild(CUIComponent child) => Tree.AddChild(child);
    public void RemoveChild(CUIComponent child) => Tree.RemoveChild(child);
  }
}