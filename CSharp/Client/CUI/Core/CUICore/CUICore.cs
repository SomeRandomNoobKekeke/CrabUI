using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using ComponentGenerator;
using Microsoft.Xna.Framework;
namespace CrabUI
{
  [GeneratedComponent]
  public partial class CUICore : IComponent
  {
    public class Part : IPart { public CUICore Self { get; set; } }

    public SimpleParser Parser { get; } = new();
    public CUIComponentTypeManager CUITypes { get; } = new();

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
    public CUIInput Input { get; } = new();
    public EventConstructor EventConstructor { get; } = new();

    private void UpdateGameScreenRect()
    {
      Main.Rect = new CUIRect(GameScreenRect.Left, GameScreenRect.Top, GameScreenRect.Width, GameScreenRect.Height);
      TopMain.Rect = new CUIRect(GameScreenRect.Left, GameScreenRect.Top, GameScreenRect.Width, GameScreenRect.Height);
    }

    public CUICore()
    {
      this.Inject();

      Parser.OnError.Add(e => CUI.Logger.Warning(e));

      CUITypes.AnalyzeAssembly(Assembly.GetExecutingAssembly());
    }

    private bool _Activated;
    internal void Activate()
    {
      if (_Activated) return;
      _Activated = true;

      Main = new() { EventConstructor = EventConstructor };
      TopMain = new() { EventConstructor = EventConstructor };

      UpdateGameScreenRect();

      DebugRelays.Route(Main.DebugRelays);
      DebugRelays.Map(DebugHub);

      DebugHub.Output.Add((e) => CUI.Logger.Log(e));
    }
  }
}