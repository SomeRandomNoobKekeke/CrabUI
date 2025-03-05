using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;

using System.Runtime.CompilerServices;
[assembly: IgnoresAccessChecksTo("Barotrauma")]
[assembly: IgnoresAccessChecksTo("DedicatedServer")]
[assembly: IgnoresAccessChecksTo("BarotraumaCore")]

namespace CrabUI
{
  /// <summary>
  /// In fact a static class managing static things
  /// </summary>
  public partial class CUI
  {
    // bruh
    //[CUIInternal]
    //static CUI() { InitStatic(); }

    public static Vector2 GameScreenSize => new Vector2(GameMain.GraphicsWidth, GameMain.GraphicsHeight);
    public static Rectangle GameScreenRect => new Rectangle(0, 0, GameMain.GraphicsWidth, GameMain.GraphicsHeight);


    private static string modDir;
    public static string ModDir
    {
      get => modDir;
      set
      {
        modDir = value;
        LuaFolder = Path.Combine(value, @"Lua");
      }
    }

    public static string LuaFolder { get; set; }

    private static string assetsPath;
    public static string AssetsPath
    {
      get => assetsPath;
      set
      {
        assetsPath = value;
        PalettesPath = Path.Combine(value, @"Palettes");
      }
    }
    public static string CUITexturePath = "CUI.png";
    public static string PalettesPath { get; set; }



    /// <summary>
    /// If set CUI will also check this folder when loading textures
    /// </summary>
    public static string PGNAssets
    {
      get => TextureManager.PGNAssets;
      set => TextureManager.PGNAssets = value;
    }

    /// <summary>
    /// A singleton
    /// </summary>
    public static CUI Instance;
    /// <summary>
    /// Orchestrates Drawing and updates, there could be only one
    /// CUI.Main is located under vanilla GUI
    /// </summary>
    public static CUIMainComponent Main => Instance?.main;
    /// <summary>
    /// Orchestrates Drawing and updates, there could be only one
    /// CUI.TopMain is located above vanilla GUI
    /// </summary>
    public static CUIMainComponent TopMain => Instance?.topMain;
    /// <summary>
    /// Snapshot of mouse and keyboard state
    /// </summary>
    public static CUIInput Input => Instance?.input;
    /// <summary>
    /// Safe texture manager
    /// </summary>
    public static CUITextureManager TextureManager = new CUITextureManager();
    //public static CUITextureManager TextureManager => Instance?.textureManager;
    /// <summary>
    /// Adapter to vanilla focus system, don't use
    /// </summary>
    public static CUIFocusResolver FocusResolver => Instance?.focusResolver;

    public static CUIComponent FocusedComponent
    {
      get => FocusResolver.FocusedCUIComponent;
      set => FocusResolver.FocusedCUIComponent = value;
    }

    /// <summary>
    /// This affects logging
    /// </summary>
    public static bool Debug;
    /// <summary>
    /// Will break the mod if it's compiled
    /// </summary>
    public static bool UseCursedPatches { get; set; } = false;
    public static Harmony harmony = new Harmony("crabui");
    public static Random Random = new Random();

    /// <summary>
    /// Called on first Initialize
    /// </summary>
    public static event Action OnInit;
    /// <summary>
    /// Called on last Dispose
    /// </summary>
    public static event Action OnDispose;
    public static event Action<TextInputEventArgs> OnWindowTextInput;
    public static event Action<TextInputEventArgs> OnWindowKeyDown;
    //public static event Action<TextInputEventArgs> OnWindowKeyUp;

    //TODO this doesn't trigger when you press menu button, i need to go inside thet method
    public static event Action OnPauseMenuToggled;
    public static void InvokeOnPauseMenuToggled() => OnPauseMenuToggled?.Invoke();

    public static bool InputBlockingMenuOpen
    {
      get
      {
        if (IsBlockingPredicates == null) return false;
        return IsBlockingPredicates.Any(p => p());
      }
    }
    public static List<Func<bool>> IsBlockingPredicates => Instance?.isBlockingPredicates;
    private List<Func<bool>> isBlockingPredicates = new List<Func<bool>>();
    /// <summary>
    /// In theory multiple mods could use same CUI instance, 
    /// i clean it up when UserCount drops to 0
    /// </summary>
    public static int UserCount = 0;

    /// <summary>
    /// An object that contains current mouse and keyboard states
    /// It scans states at the start on Main.Update
    /// </summary>
    private CUIInput input = new CUIInput();
    private CUIMainComponent main = new CUIMainComponent() { AKA = "Main Component" };
    private CUIMainComponent topMain = new CUIMainComponent() { AKA = "Top Main Component" };
    //private CUITextureManager textureManager = new CUITextureManager();
    private CUIFocusResolver focusResolver = new CUIFocusResolver();
    private CUILuaRegistrar LuaRegistrar = new CUILuaRegistrar();

    public static void ReEmitWindowTextInput(object sender, TextInputEventArgs e) => OnWindowTextInput?.Invoke(e);
    public static void ReEmitWindowKeyDown(object sender, TextInputEventArgs e) => OnWindowKeyDown?.Invoke(e);
    //public static void ReEmitWindowKeyUp(object sender, TextInputEventArgs e) => OnWindowKeyUp?.Invoke(e);

    /// <summary>
    /// Should be called in IAssemblyPlugin.Initialize 
    /// \todo make it CUI instance member when plugin system settle
    /// </summary>
    public static void Initialize()
    {
      if (Instance == null)
      {
        Stopwatch sw = Stopwatch.StartNew();

        InitStatic();
        // this should init only static stuff that doesn't depend on instance
        OnInit?.Invoke();

        Instance = new CUI();

        GameMain.Instance.Window.TextInput += ReEmitWindowTextInput;
        GameMain.Instance.Window.KeyDown += ReEmitWindowKeyDown;
        //GameMain.Instance.Window.KeyUp += ReEmitWindowKeyUp;
        CUIDebug.Log($"CUI.OnInit?.Invoke took {sw.ElapsedMilliseconds}ms");

        sw.Restart();
        PatchAll();
        CUIDebug.Log($"CUI.PatchAll took {sw.ElapsedMilliseconds}ms");

        AddCommands();

        sw.Restart();
        Instance.LuaRegistrar.Register();
        CUIDebug.Log($"CUI.LuaRegistrar.Register took {sw.ElapsedMilliseconds}ms");
      }

      UserCount++;
    }

    public static void OnLoadCompleted()
    {
      //Idk doesn't work
      //CUIMultiModResolver.FindOtherInputs();
    }


    /// <summary>
    /// Should be called in IAssemblyPlugin.Dispose
    /// </summary>
    public static void Dispose()
    {
      UserCount--;

      if (UserCount <= 0)
      {
        RemoveCommands();
        harmony.UnpatchAll(harmony.Id);
        TextureManager.Dispose();
        CUIDebugEventComponent.CapturedIDs.Clear();
        OnDispose?.Invoke();

        Instance.isBlockingPredicates.Clear();

        Instance.LuaRegistrar.Deregister();

        Instance = null;
        UserCount = 0;
      }

      GameMain.Instance.Window.TextInput -= ReEmitWindowTextInput;
      GameMain.Instance.Window.KeyDown -= ReEmitWindowKeyDown;
      //GameMain.Instance.Window.KeyUp -= ReEmitWindowKeyUp;
    }

    //HACK Why it's set to run in static constructor?
    // it runs perfectly fine in CUI.Initialize
    internal static void InitStatic()
    {
      CUIExtensions.InitStatic();
      CUIReflection.InitStatic();
      CUIMultiModResolver.InitStatic();
      CUIPalette.InitStatic();
      CUIMap.CUIMapLink.InitStatic();
      CUIComponent.InitStatic();
      CUITypeMetaData.InitStatic();
      CUIStyleLoader.InitStatic();
    }
  }
}