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
        public event Action Updated;

        public double FPS
        {
          get => 1.0 / UpdateInterval;
          set => UpdateInterval = 1.0 / value;
        }

        public double UpdateInterval { get; set; } = 1.0 / 60.0;

        private double lastUpdated;
        private void MapUpdate(double t)
        {
          if (Timing.TotalTime - lastUpdated < UpdateInterval) return;
          lastUpdated = Timing.TotalTime;

          Updated?.Invoke();
        }

        protected override void OnAttachedToMainComponent(CUIMainComponent mainComponent)
        {
          base.OnAttachedToMainComponent(mainComponent);
          CUICore.OnUpdate += MapUpdate;
        }

        protected override void OnDetachedFromMainComponent(CUIMainComponent mainComponent)
        {
          base.OnDetachedFromMainComponent(mainComponent);
          CUICore.OnUpdate -= MapUpdate;
        }
        public DynamicContainer()
        {
          Relative = new CUINullRect(0, 0, 1, 1);
        }
      }


    }
  }
}