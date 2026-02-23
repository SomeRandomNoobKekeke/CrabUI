using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;

namespace CrabUI
{
  public partial class CUICore
  {
    public class LifeCyclePart
    {
      public void DrawBeforeGUI(CUISpriteBatch spriteBatch)
      {
        Core.Main.DrawChildren(spriteBatch);
      }

      public void DrawAfterGUI(CUISpriteBatch spriteBatch)
      {

      }

      public void Update(double totalTime)
      {
        Core.Input.Update(totalTime);
        Core.Main.Update(totalTime);
      }

      private CUICore Core;
      public LifeCyclePart(CUICore core) => Core = core;
    }
  }
}