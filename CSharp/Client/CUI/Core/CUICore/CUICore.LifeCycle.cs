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
    public class LifeCycle_Part : Part
    {
      public ClearableEvent<double> OnUpdate = new();
      public ClearableEvent<ICUISpriteBatch> OnDrawAfterGUI = new();
      public ClearableEvent<ICUISpriteBatch> OnDrawBeforeGUI = new();

      public void Update(double totalTime, MouseState mouse)
      {
        try
        {
          Self.Input.Update(totalTime, mouse);
          Self.Main.Update(totalTime, Self.Input);
          OnUpdate.Raise(totalTime);
        }
        catch (Exception e)
        {
          CUI.Logger.Error(e);
        }
      }

      public void DrawAfterGUI(ICUISpriteBatch spriteBatch)
      {
        OnDrawAfterGUI.Raise(spriteBatch);
      }

      public void DrawBeforeGUI(ICUISpriteBatch spriteBatch)
      {
        Self.Main.DrawChildren(spriteBatch);
        OnDrawBeforeGUI.Raise(spriteBatch);
      }
    }

    public LifeCycle_Part LifeCycle { get; } = new();
  }
}