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

  public partial class CUICore
  {
    public class MainComponents_Part : Part
    {
      private bool Activated => Self.Activated;
      private EventConstructor EventConstructor => Self._EventConstructor;


      private Rectangle _GameScreenRect; public Rectangle GameScreenRect
      {
        get => _GameScreenRect;
        set
        {
          _GameScreenRect = value;
          if (Activated) UpdateGameScreenRect();
        }
      }


      public CUIMainComponent Main { get; private set; }
      public CUIMainComponent TopMain { get; private set; }


      private void UpdateGameScreenRect()
      {
        Main.Rect = new CUIRect(GameScreenRect.Left, GameScreenRect.Top, GameScreenRect.Width, GameScreenRect.Height);
        TopMain.Rect = new CUIRect(GameScreenRect.Left, GameScreenRect.Top, GameScreenRect.Width, GameScreenRect.Height);
      }

      public void Setup()
      {
        Main = new() { EventConstructor = EventConstructor };
        TopMain = new() { EventConstructor = EventConstructor };

        UpdateGameScreenRect();

        Main.FocusHandle.FocusRequested += Self.FocusHandle.RequestFocus;
        Main.FocusHandle.BlurRequested += Self.FocusHandle.RequestBlur;

        TopMain.FocusHandle.FocusRequested += Self.FocusHandle.RequestFocus;
        TopMain.FocusHandle.BlurRequested += Self.FocusHandle.RequestBlur;
      }
    }

    private MainComponents_Part MainComponents { get; } = new();
  }
}