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
    public static int MaxID { get; private set; }
    public int ID { get; set; }

    protected virtual void InitModules() { }

    public CUIComponent()
    {
      ID = MaxID++;
      Layout = new PlainLayout(this, new ListProxy<IBasicLayoutElement>(Children));

      InitModules();
    }

    public override string ToString() => $"{this.GetType().Name} [{this.ID}]";
  }
}