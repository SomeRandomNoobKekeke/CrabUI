using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;

namespace CursedUIUser
{
  public partial class SnapshotTests
  {
    public static partial class Dynamic
    {
      public class DynamicContainer : CUIComponent
      {
        protected override void OnDetachedFromMainComponent(CUIMainComponent mainComponent)
        {
          base.OnDetachedFromMainComponent(mainComponent);
          CUI.Logger.Log($"Oh no, i'm detached");
        }
        public DynamicContainer()
        {
          Relative = new CUINullRect(0, 0, 1, 1);
        }
      }

      public static CUIComponent TextBlocks()
      {
        DynamicContainer container = new DynamicContainer();

        container["frame"] = new CUIDefault.Frame("bruh", 400, 600);

        return container;
      }
    }
  }
}