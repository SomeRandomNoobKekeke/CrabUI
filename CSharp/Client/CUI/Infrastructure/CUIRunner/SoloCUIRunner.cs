using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public class SoloCUIRunner : ICUIRunner
  {
    public CUICore Core { get; set; }
    public ICUIRunnerDataSources DataSources { get; set; }

    private CUISpriteBatch SpriteBatch { get; } = new();

    public void Connect()
    {
      ArgumentNullException.ThrowIfNull(Core);
      ArgumentNullException.ThrowIfNull(DataSources);

      DataSources.LifeCycle.AfterGUIDraw += (spritebatch) =>
      {
        try
        {
          SpriteBatch.XNASpriteBatch = spritebatch;
          Core.DrawAfterGUIHandle.Draw(SpriteBatch);
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
          Core.DrawBeforeGUIHandle.Draw(SpriteBatch);
        }
        catch (Exception e)
        {
          CUI.Logger.Error($"CUI BeforeGUIDraw hook: [{e.Message}] -> Stopping CUI");
          Disconnect();
        }
      };

      DataSources.LifeCycle.Update += () =>
      {
        try
        {
          Core.UpdateHandle.Update();
        }
        catch (Exception e)
        {
          CUI.Logger.Error($"CUI Update hook: [{e.Message}] -> Stopping CUI");
          Disconnect();
        }
      };
    }

    public void Disconnect()
    {
      DataSources.LifeCycle.UnsubEvents();
      SpriteBatch.XNASpriteBatch = null;
    }
  }
}