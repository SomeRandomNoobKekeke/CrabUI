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
  [GeneratedComponent]
  public partial class CUIComponent : IComponent
  {
    public class Part : IPart { public CUIComponent Self { get; set; } }

    public static int MaxID { get; private set; }
    public int ID { get; set; }


    public CUIComponent()
    {
      ID = MaxID++;
      this.Inject();

      Layout = new PlainLayout();
    }

    public override string ToString() => $"{this.GetType().Name} [{this.ID}]";
  }
}