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
        CUI.Logger.Log("Update");
      }

      public void DrawAfterGUI(ICUISpriteBatch spriteBatch)
      {
        CUI.Logger.Log("DrawAfterGUI");
      }

      public void DrawBeforeGUI(ICUISpriteBatch spriteBatch)
      {
        CUI.Logger.Log("DrawBeforeGUI");
      }
    }

    private LifeCycle_Part LifeCycle { get; } = new();
  }
}