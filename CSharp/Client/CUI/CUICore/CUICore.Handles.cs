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
    public class UpdateConnectionHandle : Part
    {
      public void Update(double totalTime, MouseState mouse) => Self.LifeCycle.Update(totalTime, mouse);
    }

    public class DrawBeforeGUIConnectionHandle : Part
    {
      public void Draw(ICUISpriteBatch spriteBatch) => Self.LifeCycle.DrawBeforeGUI(spriteBatch);
    }

    public class DrawAfterGUIConnectionHandle : Part
    {
      public void Draw(ICUISpriteBatch spriteBatch) => Self.LifeCycle.DrawAfterGUI(spriteBatch);
    }

    public UpdateConnectionHandle UpdateHandle { get; } = new();
    public DrawBeforeGUIConnectionHandle DrawBeforeGUIHandle { get; } = new();
    public DrawAfterGUIConnectionHandle DrawAfterGUIHandle { get; } = new();
  }
}