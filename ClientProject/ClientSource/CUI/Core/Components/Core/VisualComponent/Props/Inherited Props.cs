using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
using System.Collections;
namespace CursedUI
{
  public partial class CUIVisualComponent
  {
    protected void InheritProps(CUIVisualComponent parent)
    {
      if (InheritPalette) DeepPalette = parent.Palette;
      // if (DeepInheritPalette) DeepPalette = parent.Palette;
    }

  }
}