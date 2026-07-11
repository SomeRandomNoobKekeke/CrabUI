using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using CUICodeGenerator;
using Microsoft.Xna.Framework;
namespace CrabUI
{
  [GeneratedComponent]
  public partial class CUICore : IComponent
  {
    public class Part : IPart { public CUICore Self { get; set; } }

    public CUIAssemblyAnalyzer _Analyzer { get; }
    public CUIStyleManager CUIStyleManager { get; }
    public CUIPaletteManager CUIPaletteManager { get; }

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
    public CUIInput _Input { get; } = new();
    public EventConstructor EventConstructor { get; private set; }
    public AnimationPlayer _AnimationPlayer { get; private set; }

    public CUIParser _CUIParser { get; private set; } = new();
    public CUISerializer _CUISerializer { get; private set; } = new();



    private void UpdateGameScreenRect()
    {
      Main.Rect = new CUIRect(GameScreenRect.Left, GameScreenRect.Top, GameScreenRect.Width, GameScreenRect.Height);
      TopMain.Rect = new CUIRect(GameScreenRect.Left, GameScreenRect.Top, GameScreenRect.Width, GameScreenRect.Height);
    }

    public CUICore()
    {
      this.Inject();

      _Analyzer = new();
      //TODO different runners should analyze different assemblies, perhaps it doesn't belong here
      _Analyzer.AnalyzeAssembly(Assembly.GetExecutingAssembly());

      CUIStyleManager = new(_Analyzer);
      CUIPaletteManager = new();

      //TODO i probably want to go in base->derived order here
      foreach (CUIComponentInfo info in _Analyzer.ComponentInfos.Values)
      {
        if (info.DefaultStyle is not null)
        {
          CUIStyleManager.AddStyle(info.DefaultStyle);
        }
      }
    }

    private bool _Activated;
    //Note: this exists primerely because DebugNodes may call DebugHub on creation
    internal void Activate()
    {
      if (_Activated) return;
      _Activated = true;

      EventConstructor = new();
      _AnimationPlayer = new();

      Main = new() { EventConstructor = EventConstructor };
      TopMain = new() { EventConstructor = EventConstructor };

      UpdateGameScreenRect();

      DebugRelays.Route(Main.DebugRelays);
      DebugRelays.Map(DebugHub);

      // DebugHub.Output.Add((e) => CUI.Logger.Log(e));
    }
  }
}