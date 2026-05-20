using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIComponent
  {
    protected partial class Adapters_Part
    {
      public IResizable_Adapter IResizable { get; } = new();
      public partial class IResizable_Adapter : Part, IAdapterPart, IResizable
      {
        public CUIRect Rect => Self.Rect;
      }
    }
  }
}