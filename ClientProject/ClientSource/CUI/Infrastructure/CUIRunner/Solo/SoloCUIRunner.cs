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

namespace CursedUI
{
  [GeneratedComponent]
  public partial class SoloCUIRunner : ICUIRunner, IComponent
  {
    public class Part : IPart { public SoloCUIRunner Self { get; set; } }

    public CUICore Core { get; set; }
    public ICUIRunnerDataSources DataSources { get; set; }
    public CUICoreHandles_Part CUICoreHandles { get; } = new();

    public GUIButton DummyComponent = new GUIButton(new RectTransform(new Point(0, 0)))
    {
      Text = "DUMMY",
    };

    public __CUISpriteBatch SpriteBatch { get; } = new();
    public __CUIGraphicsDevice GraphicsDevice { get; } = new();
    public __CUIGUI CUIGUI { get; } = new();
    public AssemblyPackageDirLookup DirLookup { get; } = new();


    public ResourceIOContext_Part ResourceIOContext { get; private set; } = new();
    public ResourceIOContextHandle_Part ResourceIOContextHandle { get; private set; } = new();
    public FilePathResolver FilePathResolver { get; private set; } = new();

    public CUIAssemblyAnalyzer CUIAssemblyAnalyzer { get; } = new();



    public void Connect()
    {
      ArgumentNullException.ThrowIfNull(Core);
      ArgumentNullException.ThrowIfNull(DataSources);

      Core.Handles = CUICoreHandles;


      AttachHooks();
    }

    private void AttachHooks()
    {
      DataSources.AfterGUIDraw += AfterGUIDrawHook;
      DataSources.BeforeGUIDraw += BeforeGUIDrawHook;
      DataSources.Update += UpdateHook;
      DataSources.SyncMouseOn += SyncMouseOn;
      DataSources.VanillaGUIElementFocused += ClearCUIFocus;
    }

    private void DetachHooks()
    {
      DataSources.AfterGUIDraw -= AfterGUIDrawHook;
      DataSources.BeforeGUIDraw -= BeforeGUIDrawHook;
      DataSources.Update -= UpdateHook;
      DataSources.SyncMouseOn -= SyncMouseOn;
      DataSources.VanillaGUIElementFocused -= ClearCUIFocus;
    }

    public void Disconnect()
    {
      try
      {
        DetachHooks();

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



    private HashSet<Assembly> AlreadyAnalyzedAssemblies { get; } = new()
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
      this.Inject();
      LoadDefaultTextures();
    }
  }
}