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
using System.IO;

namespace CrabUIUser
{
  public partial class E2ETestPack
  {
    public partial class Pool : IE2ETest
    {
      public CUIWater CUIWater { get; private set; }

      public void Initialize()
      {
        CUIFrame frame = new CUIDefault.Frame("Pool")
        {
          Absolute = new CUINullRect(w: 512, h: 512),
        };

        frame["layout"]["pool"] = CUIWater = new CUIWater()
        {
          Flex = 1,
        };

        CUIWater.MouseOn += (c, e) =>
        {
          if (!e.Mouse.Pressed) return;
          Vector2 v = CUIAnchor.AnchorFromPos(c.Rect, e.Pos);
          CUIWater.Drop(v.X, v.Y);
        };

        frame.Open();
        frame.OnClose += (c) => Dispose();

        CUI.Main.GlobalEvents.AfterUpdate.Add(UpdatePool);
      }

      public void UpdatePool() => CUIWater.Update(Timing.TotalTime);

      public void Dispose()
      {
        CUI.Main.GlobalEvents.AfterUpdate.Remove(UpdatePool);
      }
    }
  }
}