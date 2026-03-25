using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using ComponentInjector;

namespace CrabUI
{
  [GeneratedComponent]
  public partial class CUICore : IComponent
  {
    public class Part : IPart { public CUICore Self { get; set; } }

    public CUIMainComponent Main { get; } = new();
    public CUIInput Input { get; } = new();

    public CUICore()
    {
      this.Inject();
    }
  }
}