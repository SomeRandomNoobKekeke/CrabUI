using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{

  public partial class SoloCUIRunner
  {
    private void AfterGUIDrawHook(SpriteBatch spritebatch)
    {
      try
      {
        SpriteBatch.XNASpriteBatch = spritebatch;
        Core.CUIRunnerHandle.DrawAfterGUI(SpriteBatch);
      }
      catch (Exception e)
      {
        CUI.Logger.Error($"CUI AfterGUIDraw hook: [{e.Message}] -> Stopping CUI");
        Disconnect();
      }
    }

    private void BeforeGUIDrawHook(SpriteBatch spritebatch)
    {
      try
      {
        SpriteBatch.XNASpriteBatch = spritebatch;
        Core.CUIRunnerHandle.DrawBeforeGUI(SpriteBatch);
      }
      catch (Exception e)
      {
        CUI.Logger.Error($"CUI BeforeGUIDraw hook: [{e.Message}] -> Stopping CUI");
        Disconnect();
      }
    }

    private void UpdateHook(GameTime gameTime)
    {
      try
      {
        //TODO extract real totalTime from gameTime
        //TODO mb i should pass RunnerMouseOnTracker as arg
        Core.CUIRunnerHandle.Update(
          gameTime.TotalGameTime.TotalSeconds,
          DataSources.ScanMouse(),
          DataSources.ScanKeyboard(),
          DataSources.ScanTextInput()
        );

        if (Core.CUIRunnerHandle.MouseIsOnSomeCUIElement)
        {
          GUI.MouseOn = DummyComponent;
        }
      }
      catch (Exception e)
      {
        CUI.Logger.Error($"CUI Update hook: [{e.Message}] -> Stopping CUI");
        Disconnect();
      }
    }


    private void SyncMouseOn()
    {
      try
      {
        CUICoreHandles.IsMouseOnVanillaGUIComponent = GUI.MouseOn != null && GUI.MouseOn != DummyComponent;

        if (Core.CUIRunnerHandle.MouseIsOnSomeCUIElement)
        {
          GUI.MouseOn = DummyComponent;
        }
      }
      catch (Exception e)
      {
        CUI.Logger.Error($"CUI SyncMouseOn hook: [{e.Message}] -> Stopping CUI");
        Disconnect();
      }
    }

  }
}