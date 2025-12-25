using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;

namespace CrabUI
{
  public partial class CUIComponentCore
  {
    public static int MaxID { get; private set; }
    public int ID { get; set; }

    protected virtual void InitModules() { }

    public CUIComponentCore()
    {
      ID = MaxID++;

      InitModules();
    }

    public override string ToString() => $"{this.GetType().Name} [{this.ID}]";
  }
}