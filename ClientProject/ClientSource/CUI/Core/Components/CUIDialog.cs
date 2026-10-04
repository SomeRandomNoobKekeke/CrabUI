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

  public abstract class CUIDialog : CUIDefault.Frame
  {
    protected override bool SingleInstance => true;
    public CUIDialog(string caption, float width, float height) : base(caption, width, height)
    {

    }

  }
}