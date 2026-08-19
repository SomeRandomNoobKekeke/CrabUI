using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;
using Microsoft.Xna.Framework;
namespace CrabUI
{
  [GeneratedComponent]
  public partial class CUICore : IComponent
  {
    public class Part : IPart { public CUICore Self { get; set; } }

    public Rectangle GameScreenRect
    {
      get => MainComponents.GameScreenRect;
      set => MainComponents.GameScreenRect = value;
    }

    private CUIStyleManager CUIStyleManager;
    private CUIPalettes _Palettes;

    public CUIMainComponent Main => MainComponents.Main;
    public CUIMainComponent TopMain => MainComponents.TopMain;

    private CUIInput _Input = new();
    private EventConstructor _EventConstructor;
    private AnimationPlayer _AnimationPlayer;
    private CUIParser _CUIParser = new();
    private CUISerializer _CUISerializer = new();
    private CUIAssemblyAnalyzer CUIAssemblyAnalyzer = new();




    private bool Activated;
    //Note: this exists primarily because DebugNodes may call DebugHub on creation
    internal void Activate()
    {
      if (Activated) return;

      try
      {
        CUIStyleManager = new(Reflection.TypeTree);
        _Palettes = new();

        _EventConstructor = new();
        _AnimationPlayer = new();

        //CUICore analyzes itself because it needs infos for MainComponents right here
        Reflection.AddAssemblyInfo(
          CUIAssemblyAnalyzer.AnalyzeAssembly(typeof(CUICore).Assembly)
        );

        MainComponents.Setup();

        InitDebug();

        Activated = true;
      }
      catch (Exception e)
      {
        CUI.Logger.Warning($"CUICore Activation failed with:\n{e}");
        CUI.Logger.Warning($"Stopping CUI");
        CUI.Stop();
        if (CUI.ErrorHandlingStrategy == ErrorHandlingStrategy.FailFast) throw;
      }
    }

    public CUICore()
    {
      this.Inject();
    }
  }
}