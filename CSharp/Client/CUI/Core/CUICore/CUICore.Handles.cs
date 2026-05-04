using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using ComponentGenerator;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public partial class CUICore
  {
    public class CUIRunnerHandle_Part : Part
    {
      public void Update(double totalTime, MouseState mouse) => Self.LifeCycle.Update(totalTime, mouse);
      public void DrawBeforeGUI(ICUISpriteBatch spriteBatch) => Self.LifeCycle.DrawBeforeGUI(spriteBatch);
      public void DrawAfterGUI(ICUISpriteBatch spriteBatch) => Self.LifeCycle.DrawAfterGUI(spriteBatch);
    }

    public CUIRunnerHandle_Part CUIRunnerHandle { get; } = new();
  }
}