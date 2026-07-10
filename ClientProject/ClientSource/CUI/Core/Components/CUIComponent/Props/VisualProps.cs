using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using BaroJunk;

namespace CrabUI
{
  public partial class CUIComponent
  {
    [CUISerializable]
    public bool CullChildren { get; set; }


    public bool ConsumeMouseClicks
    {
      get => Background.ConsumeMouseClicks;
      set => Background.ConsumeMouseClicks = value;
    }
  }
}