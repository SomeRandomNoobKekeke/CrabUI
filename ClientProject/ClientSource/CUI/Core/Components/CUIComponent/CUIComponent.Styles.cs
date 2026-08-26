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
namespace CursedUI
{
  public partial class CUIComponent
  {
    public new Action<CUIComponent> Style
    {
      set => PersonalStyle = new CUIActionStyle<CUIComponent>("personal", value);
    }
  }
}