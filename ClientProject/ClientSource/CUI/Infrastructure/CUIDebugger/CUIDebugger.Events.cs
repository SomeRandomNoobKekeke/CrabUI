using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;


namespace CrabUI
{
  public partial class CUIDebugger
  {
    public class EventsPageComponent : CUIPage
    {
      public EventsPageComponent() : base()
      {
        Background.Color = new Color(255, 0, 200);
      }
    }
  }
}