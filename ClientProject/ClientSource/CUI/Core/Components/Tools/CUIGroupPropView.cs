using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  //Test LINK:\ClientProject\InMemory\CUITest\Snapshots\Tests\Components\CUIGroupPropView.cs
  public class CUIGroupPropView : CUIPage
  {

    private CUIVisualComponent _Target; public CUIVisualComponent Target
    {
      get => _Target;
      set
      {
        _Target = value;
      }
    }


    public CUIGroupPropView() : base()
    {
      Background.Color = Color.Cyan;
    }
  }
}