using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using Barotrauma;
namespace CrabUI
{
  public partial class SoloCUIRunner : ICUIRunner
  {
    public CUICore Core { get; set; }
    public ICUIRunnerDataSources DataSources { get; set; }
    public CUICore.CUICoreHandles CUICoreHandles { get; private set; }

    public string ModDir { get; private set; } = ModInfo.Dir;
    public __CUISpriteBatch SpriteBatch { get; } = new();
    public __CUIGraphicsDevice GraphicsDevice { get; } = new();
    public __CUIGUI CUIGUI { get; } = new();

    public TextureManager TextureManager { get; } = new();


    public void Connect()
    {
      ArgumentNullException.ThrowIfNull(Core);
      ArgumentNullException.ThrowIfNull(DataSources);

      Core.Handles = CUICoreHandles;

      AttachLifeCycleHooks();
    }

    private void AttachLifeCycleHooks()
    {
      DataSources.LifeCycle.AfterGUIDraw += (spritebatch) =>
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
      };

      DataSources.LifeCycle.BeforeGUIDraw += (spritebatch) =>
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
      };

      DataSources.LifeCycle.Update += (gameTime) =>
      {
        try
        {

          //TODO extract real totalTime from gameTime
          Core.CUIRunnerHandle.Update(
            Timing.TotalTime,
            DataSources.Input.ScanMouse(),
            DataSources.Input.ScanKeyboard(),
            DataSources.Input.ScanTextInput()
          );
          UpdateMouseOn();
        }
        catch (Exception e)
        {
          CUI.Logger.Error($"CUI Update hook: [{e.Message}] -> Stopping CUI");
          Disconnect();
        }
      };
    }

    private void UpdateMouseOn()
    {
      if (GUI.MouseOn == null && Core.CUIRunnerHandle.IsMouseOnSomeCUIComponent())
      {
        GUI.MouseOn = CUI.DummyComponent;
      }
    }

    public void Disconnect()
    {
      DataSources.DisconnectFromGame();
      SpriteBatch.XNASpriteBatch = null;
      Core.Handles = null;
      TextureManager.Clear();
    }

    public SoloCUIRunner()
    {
      CUICoreHandles = new SoloCUIRunner.CUICoreHandles_Part()
      {
        Self = this
      };
    }
  }
}