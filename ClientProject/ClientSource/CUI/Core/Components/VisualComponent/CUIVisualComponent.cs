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
  public abstract partial class CUIVisualComponent : IVisualComponent
  {
    public class Part : IPart { public CUIVisualComponent Self { get; set; } }

    public static int MaxID { get; private set; }
    public int ID { get; set; }

    public string TypeName => this.GetType().Name;



    public abstract CUIRect Rect { get; set; }
    public VisualUnit.NestedVisualComponent VisualWrapper { get; }

    public abstract IEnumerable<VisualUnit> VisualSplit();


    public CUIVisualComponent()
    {
      ID = MaxID++;
      VisualWrapper = new(this);
    }

    public override string ToString() => $"{this.GetType().Name} [{this.ID}]";
  }
}