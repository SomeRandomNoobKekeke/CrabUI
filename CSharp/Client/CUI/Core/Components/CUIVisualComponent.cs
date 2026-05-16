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
  [GeneratedComponent]
  public abstract partial class CUIVisualComponent : IVisualComponent, IComponent
  {
    public class Part : IPart { public CUIVisualComponent Self { get; set; } }

    public static int MaxID { get; private set; }
    public int ID { get; set; }

    public string TypeName => this.GetType().Name;

    public abstract void UpdateRect(CUIRect rect);
    public abstract IEnumerable<VisualUnit> VisualSplit();


    public CUIVisualComponent()
    {
      ID = MaxID++;
      this.Inject();
    }

    public override string ToString() => $"{this.GetType().Name} [{this.ID}]";
  }
}