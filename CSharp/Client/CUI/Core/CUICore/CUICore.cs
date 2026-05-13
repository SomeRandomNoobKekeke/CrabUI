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

    private Rectangle _GameScreenRect;
    public Rectangle GameScreenRect
    {
      get => _GameScreenRect;
      set
      {
        _GameScreenRect = value;
        UpdateGameScreenRect();
      }
    }

    public CUIMainComponent Main { get; } = new();
    public CUIInput Input { get; } = new();

    private void UpdateGameScreenRect()
    {
      Main.Rect = new CUIRect(GameScreenRect.Left, GameScreenRect.Top, GameScreenRect.Width, GameScreenRect.Height);
    }

    public CUICore()
    {
      this.Inject();

    }
  }
}