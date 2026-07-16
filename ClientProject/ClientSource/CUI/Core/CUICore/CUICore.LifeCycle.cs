using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public partial class CUICore
  {
    public class LifeCycle_Part : Part
    {
      public ClearableEvent<double> OnUpdate = new();
      public ClearableEvent<CUISpriteBatch> OnDrawAfterGUI = new();
      public ClearableEvent<CUISpriteBatch> OnDrawBeforeGUI = new();

      public void Update(double totalTime, MouseState mouse, KeyboardState keyboard, TextInputEventPack textInput)
      {
        try
        {
          Stopwatch sw = Stopwatch.StartNew();

          Self._Input.Update(totalTime, mouse, keyboard, textInput);
          Self.EventConstructor.Construct(Self._Input);

          Self.TopMain.Update(totalTime, Self._Input);
          Self.Main.Update(totalTime, Self._Input);

          Self.GlobalFocusTracker.DispatchKeyboadEvents();
          Self.GlobalFocusTracker.ResolveFocus(Self._Input.SomethingFocusedElsewhere);

          Self._AnimationPlayer.Update();

          OnUpdate.Raise(totalTime);

          sw.Stop();
          // CUI.Logger.Log($"Update took {sw.ElapsedTicks}");
        }
        catch (Exception e)
        {
          CUI.Logger.Error(e);
        }
      }

      public void DrawAfterGUI(CUISpriteBatch spriteBatch)
      {
        try
        {
          OnDrawAfterGUI.Raise(spriteBatch);
          Self.TopMain.DrawChildren(spriteBatch);
        }
        catch (Exception e)
        {
          CUI.Logger.Error($"Error in CUICore.DrawAfterGUI: {e}\n");
        }

      }

      public void DrawBeforeGUI(CUISpriteBatch spriteBatch)
      {
        Self.Main.DrawChildren(spriteBatch);
        OnDrawBeforeGUI.Raise(spriteBatch);

        try
        {

        }
        catch (Exception e)
        {
          CUI.Logger.Error($"Error in CUICore.DrawBeforeGUI: {e}\n");
        }
      }

      public bool IsMouseOnSomeCUIComponent()
      {
        return Self.Main.MouseOverSomeElement;
      }
    }

    public LifeCycle_Part LifeCycle { get; } = new();
  }
}