using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUICore
  {
    public class LifeCycle_Part : Part
    {
      public void Update()
      {

      }

      public void DrawAfterGUI()
      {

      }

      public void DrawBeforeGUI()
      {

      }
    }

    private LifeCycle_Part LifeCycle { get; } = new();
  }
}