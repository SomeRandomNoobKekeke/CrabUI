using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Barotrauma;
using Microsoft.Xna.Framework;
namespace CrabUI
{
  public partial class SoloCUIRunner : ICUIRunner
  {
    public CUICore Core { get; set; }
    public ICUIRunnerDataSources DataSources { get; set; }
    public CUICore.CUICoreHandles CUICoreHandles { get; private set; }

    public GUIButton DummyComponent = new GUIButton(new RectTransform(new Point(0, 0)))
    {
      Text = "DUMMY",
    };

    public __CUISpriteBatch SpriteBatch { get; } = new();
    public __CUIGraphicsDevice GraphicsDevice { get; } = new();
    public __CUIGUI CUIGUI { get; } = new();
    public AssemblyPackageDirLookup DirLookup { get; } = new();

    public __CUITextureManager TextureManager { get; private set; } = new();
    CUITextureManager ICUIRunner.TextureManager => CUITextureManagerPublic;
    public CUITextureManager_PublicPart CUITextureManagerPublic { get; private set; } = new();
    public ResourceIOContext_Part ResourceIOContext { get; private set; } = new();
    public ResourceIOContextHandle_Part ResourceIOContextHandle { get; private set; } = new();
    public FilePathResolver FilePathResolver { get; private set; } = new();

    public RunnerMouseOnTracker RunnerMouseOnTracker { get; private set; } = new();

    public CUIAssemblyAnalyzer CUIAssemblyAnalyzer { get; } = new();

    public OtherResources _OtherResources { get; } = new();

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
          RunnerMouseOnTracker.IsMouseOnVanillaGUIComponent = GUI.MouseOn != null && GUI.MouseOn != DummyComponent; //TODO get it from DataSources

          //TODO extract real totalTime from gameTime
          //TODO mb i should pass RunnerMouseOnTracker as arg
          Core.CUIRunnerHandle.Update(
            gameTime.TotalGameTime.TotalSeconds,
            DataSources.Input.ScanMouse(),
            DataSources.Input.ScanKeyboard(),
            DataSources.Input.ScanTextInput()
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
      };

      DataSources.LifeCycle.SyncMouseOn += () =>
      {
        try
        {
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
      };
    }



    public void Disconnect()
    {
      try
      {
        DataSources.DisconnectFromGame();
        SpriteBatch.XNASpriteBatch = null;
        Core.Handles = null;
        TextureManager.Dispose();
        DirLookup.Dispose();
      }
      catch (Exception e)
      {
        CUI.Logger.Error(e);
      }
    }

    private void LoadDefaultResources()
    {
      CUITextureManagerPublic.LoadAs("Assets/PNG/dev.png", "BaroDev");
      CUITextureManagerPublic.LoadAs("Assets/PNG/CUI.png", "CUI");
    }


    public HashSet<Assembly> AlreadyAnalyzedAssemblies { get; } = new()
    {
      typeof(CUICore).Assembly, //HACK CUICore analyzes itself
    };
    public void OnStartAttempt(Assembly callingAssembly)
    {
      if (AlreadyAnalyzedAssemblies.Contains(callingAssembly)) return;
      AlreadyAnalyzedAssemblies.Add(callingAssembly);

      Core.CUIRunnerHandle.AddAssemblyInfo(
        CUIAssemblyAnalyzer.AnalyzeAssembly(callingAssembly)
      );
    }

    public SoloCUIRunner()
    {
      CUICoreHandles = new SoloCUIRunner.CUICoreHandles_Part()
      {
        Self = this
      };

      CUITextureManagerPublic.Self = this;
      ResourceIOContext.Self = this;
      ResourceIOContextHandle.Self = this;

      LoadDefaultResources();
    }
  }
}