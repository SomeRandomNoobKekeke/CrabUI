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
        SpriteBatch.XNASpriteBatch = spritebatch;
        Core.DrawAfterGUIHandle.Draw(SpriteBatch);
      };

      DataSources.LifeCycle.BeforeGUIDraw += (spritebatch) =>
      {
        SpriteBatch.XNASpriteBatch = spritebatch;
        Core.DrawBeforeGUIHandle.Draw(SpriteBatch);
      };

      DataSources.LifeCycle.Update += () =>
      {
        Core.UpdateHandle.Update();
      };
    }

    public void Disconnect()
    {
      DataSources.LifeCycle.UnsubEvents();
    }
  }
}