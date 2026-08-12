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

    public CUIStyleManager CUIStyleManager { get; private set; }
    public CUIPalettes CUIPalettes { get; private set; }

    private Rectangle _GameScreenRect; public Rectangle GameScreenRect
    {
      get => _GameScreenRect;
      set
      {
        _GameScreenRect = value;
        if (_Activated) UpdateGameScreenRect();
      }
    }

    public CUIMainComponent Main { get; private set; }
    public CUIMainComponent TopMain { get; private set; }
    public CUIInput _Input { get; private set; } = new();
    public EventConstructor EventConstructor { get; private set; }
    public AnimationPlayer _AnimationPlayer { get; private set; }

    public CUIParser _CUIParser { get; private set; } = new();
    public CUISerializer _CUISerializer { get; private set; } = new();
    public CUIAssemblyAnalyzer CUIAssemblyAnalyzer { get; } = new();



    private void UpdateGameScreenRect()
    {
      Main.Rect = new CUIRect(GameScreenRect.Left, GameScreenRect.Top, GameScreenRect.Width, GameScreenRect.Height);
      TopMain.Rect = new CUIRect(GameScreenRect.Left, GameScreenRect.Top, GameScreenRect.Width, GameScreenRect.Height);
    }

    public CUICore()
    {
      this.Inject();
    }

    private bool _Activated;
    //Note: this exists primarily because DebugNodes may call DebugHub on creation
    internal void Activate()
    {
      if (_Activated) return;

      try
      {
        CUIStyleManager = new(Reflection.TypeTree);
        CUIPalettes = new();

        EventConstructor = new();
        _AnimationPlayer = new();

        //CUICore analyzes itself because it needs infos for MainComponents right here
        Reflection.AddAssemblyInfo(
          CUIAssemblyAnalyzer.AnalyzeAssembly(typeof(CUICore).Assembly)
        );

        Main = new() { EventConstructor = EventConstructor };
        TopMain = new() { EventConstructor = EventConstructor };

        UpdateGameScreenRect();

        InitDebug();

        _Activated = true;
      }
      catch (Exception e)
      {
        CUI.Logger.Warning($"CUICore Activation failed with:\n{e}");
        CUI.Logger.Warning($"Stopping CUI");
        CUI.Stop();
        if (CUI.ErrorHandlingStrategy == ErrorHandlingStrategy.FailFast) throw;
      }
    }
  }
}