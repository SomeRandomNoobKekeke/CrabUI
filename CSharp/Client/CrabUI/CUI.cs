using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;

namespace CrabUI
{
  /// <summary>
  /// In fact a static class managing static things
  /// </summary>
  public partial class CUI
  {
    /// <summary>
    /// I need to init all reflction stuff at once, and not one by one when i touch it
    /// </summary>
    [CUIInternal]
    static CUI() { InitStatic(); }

    public static Vector2 GameScreenSize => new Vector2(GameMain.GraphicsWidth, GameMain.GraphicsHeight);
    public static Rectangle GameScreenRect => new Rectangle(0, 0, GameMain.GraphicsWidth, GameMain.GraphicsHeight);

    public static string ModDir = "";
    public static string LuaFolder => Path.Combine(ModDir, @"Lua");
    public static string CUIPath => GetCallerFolderPath();
    public static string CUITexturePath => Path.Combine(CUIPath, @"CUIAssets\CUI.png");


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
    public static CUITextureManager TextureManager => Instance?.textureManager;
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
    private CUITextureManager textureManager = new CUITextureManager();
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
        // this should init only static stuff that doesn't depend on instance
        OnInit?.Invoke();

        Instance = new CUI();

        FindModFolder();

        GameMain.Instance.Window.TextInput += ReEmitWindowTextInput;
        GameMain.Instance.Window.KeyDown += ReEmitWindowKeyDown;
        //GameMain.Instance.Window.KeyUp += ReEmitWindowKeyUp;

        PatchAll();
        AddCommands();
        Instance.LuaRegistrar.Register();

        //HACK this works, but i still think that i shouldn't make aby assumptions about
        // file layout outside of CSharp folder, and i shouldn't store pngs in CSharp
        // perhaps i should generate default textures at runtime
        // or pack them with dll when plugin system settles
        //Log(GetCallerFilePath());
      }

      UserCount++;
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

        Instance.LuaRegistrar.Deregister();

        Instance = null;
        UserCount = 0;
      }

      GameMain.Instance.Window.TextInput -= ReEmitWindowTextInput;
      GameMain.Instance.Window.KeyDown -= ReEmitWindowKeyDown;
      //GameMain.Instance.Window.KeyUp -= ReEmitWindowKeyUp;
    }

    // This is a hacky solution that won't work in compiled version
    public static void FindModFolder()
    {
      ModDir = CUIPath.Substring(0, CUIPath.IndexOf("CSharp"));
    }

    internal static void InitStatic()
    {
      CUIExtensions.InitStatic();
      CUIReflection.InitStatic();
      CUIMap.CUIMapLink.InitStatic();
      CUIComponent.InitStatic();
      CUITypeMetaData.InitStatic();
    }
  }
}