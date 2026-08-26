using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Barotrauma;

namespace CursedUI
{
  public static class CUI
  {
    static CUI()
    {
      CUICommands.Add();
      PluginLifeCycle.Stop += Dispose;

      Setup = CUISetup.Default();
    }

    public static ErrorHandlingStrategy ErrorHandlingStrategy { get; set; } = ErrorHandlingStrategy.FailFast;

    //BRUH why is it here?
    public static SamplerState NoSmoothing = new SamplerState()
    {
      Filter = TextureFilter.Point,
      AddressU = TextureAddressMode.Clamp,
      AddressV = TextureAddressMode.Clamp,
      AddressW = TextureAddressMode.Clamp,
      BorderColor = Color.White,
      MaxAnisotropy = 4,
      MaxMipLevel = 0,
      MipMapLevelOfDetailBias = -0.8f,
      ComparisonFunction = CompareFunction.Never,
      FilterMode = TextureFilterMode.Default,
    };



    public static Logger Logger = new()
    {
      PrintFilePath = false,
    };

    public static Random Random { get; } = new();

    public static Rectangle GameScreenRect => Core.GameScreenRect;


    public static CUISetup Setup { get; set; }
    public static CUICore Core => Setup.Core;


    public static bool Started => Setup?.Started == true;
    //Akshually in MasterRunners calling this 
    public static void Start()
    {
      Setup.Start(Assembly.GetCallingAssembly());
    }
    public static void Stop() => Setup?.Stop();

    public static CUIMainComponent Main => Setup.Core.Main;
    public static CUIMainComponent TopMain => Setup.Core.TopMain;
    public static DebugHub DebugHub => CUICore.DebugHub;
    public static CUITextureManager TextureManager => Setup.Runner.TextureManager;

    public static double UpdateFPS
    {
      get => Setup.Core.UpdateFPS;
      set => Setup.Core.UpdateFPS = value;
    }

    public static event Action<double> OnUpdate
    {
      add => Setup.Core.LifeCycle.OnUpdate.Add(value);
      remove => Setup.Core.LifeCycle.OnUpdate.Remove(value);
    }

    public static event Action<CUISpriteBatch> OnDrawAfterGUI
    {
      add => Setup.Core.LifeCycle.OnDrawAfterGUI.Add(value);
      remove => Setup.Core.LifeCycle.OnDrawAfterGUI.Remove(value);
    }

    public static event Action<CUISpriteBatch> OnDrawBeforeGUI
    {
      add => Setup.Core.LifeCycle.OnDrawBeforeGUI.Add(value);
      remove => Setup.Core.LifeCycle.OnDrawBeforeGUI.Remove(value);
    }

    public static void Dispose()
    {
      try
      {
        Setup?.Stop();
        Setup = null;
      }
      catch (Exception e)
      {
        CUI.Logger.Error($"Error in CUI Dispose: {e}");
      }
    }
  }
}