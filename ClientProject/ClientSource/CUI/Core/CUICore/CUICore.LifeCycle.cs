using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Barotrauma;

namespace CrabUI
{
  public partial class CUICore
  {
    public class LifeCycle_Part : Part
    {
      public ClearableEvent<double> OnBeforeUpdate = new();
      public ClearableEvent<double> OnUpdate = new();
      public ClearableEvent<CUISpriteBatch> OnDrawAfterGUI = new();
      public ClearableEvent<CUISpriteBatch> OnDrawBeforeGUI = new();

      public int MaxErrorCount = 10;
      public int ErrorCount = 0;

      private void HandleError()
      {
        if (ErrorCount++ < MaxErrorCount) return;
        CUI.Logger.Warning($"More than [{MaxErrorCount}] errors happened in CUICore.LifeCycle");
        CUI.Logger.Warning($"Stopping CUI");
        CUI.Stop();
      }

      public void Update(double totalTime, MouseState mouse, KeyboardState keyboard, TextInputEventPack textInput)
      {
        try
        {
          Stopwatch sw = Stopwatch.StartNew();

          OnBeforeUpdate.Raise(totalTime);

          Self._Input.Update(totalTime, mouse, keyboard, textInput);
          Self.EventConstructor.Construct(Self._Input);

          Self.TopMain.Update(totalTime, Self._Input);
          Self.VanillaGUILayerImage.Update(Self._Input);
          Self.Main.Update(totalTime, Self._Input);
          Self.VanillaGUILayerImage.CommunicateCUIMouseOnToRunner();

          Self._AnimationPlayer.Update();

          OnUpdate.Raise(totalTime);

          sw.Stop();
          GameMain.PerformanceCounter.AddElapsedTicks("Update:CUI", sw.ElapsedTicks);
          // CUI.Logger.Log($"Update took {sw.ElapsedTicks}");
        }
        catch (Exception e)
        {
          CUI.Logger.Error(e);
          HandleError();
        }
      }

      public void DrawAfterGUI(CUISpriteBatch spriteBatch)
      {
        try
        {
          Stopwatch sw = Stopwatch.StartNew();

          OnDrawAfterGUI.Raise(spriteBatch);
          Self.TopMain.DrawChildren(spriteBatch);

          sw.Stop();
          GameMain.PerformanceCounter.AddElapsedTicks("Draw:CUI", sw.ElapsedTicks);
        }
        catch (Exception e)
        {
          CUI.Logger.Error($"Error in CUICore.DrawAfterGUI: {e}\n");
          HandleError();
        }
      }

      public void DrawBeforeGUI(CUISpriteBatch spriteBatch)
      {
        try
        {
          Stopwatch sw = Stopwatch.StartNew();

          Self.Main.DrawChildren(spriteBatch);
          OnDrawBeforeGUI.Raise(spriteBatch);

          sw.Stop();
          GameMain.PerformanceCounter.AddElapsedTicks("Draw:CUI", sw.ElapsedTicks);
        }
        catch (Exception e)
        {
          CUI.Logger.Error($"Error in CUICore.DrawBeforeGUI: {e}\n");
          HandleError();
        }
      }
    }



    public LifeCycle_Part LifeCycle { get; } = new();
  }
}