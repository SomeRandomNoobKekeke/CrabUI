using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUILibs;

namespace CursedUI
{

  public enum ResizeStrategy
  {
    Passive, Rescale, Resist, Wrap
  }

  public partial class TextBlock
  {
    //TODO should this be public?
    public abstract class ResizeStrategyBase
    {
      public static PassiveStrategy PassiveStrategy => new();
      public static RescaleStrategy RescaleStrategy => new();
      public static ResistStrategy ResistStrategy => new();
      public static WrapStrategy WrapStrategy => new();


      public abstract void MeasureRawTextSize(string text, float scale, CUIFont font);
      public abstract void MeasureRealTextSize(CUIRect rect, Vector2 anchor, string text, float scale, CUIFont font);

      public string RealText { get; protected set; } = "";
      public Vector2 RawTextSize { get; protected set; }
      public Vector2 TextDrawPosition { get; protected set; }
      public CUINullVector2 ForcedSize { get; protected set; }
      public float RealScale { get; protected set; }

      public static ResizeStrategyBase FromEnum(ResizeStrategy strategy)
        => strategy switch
        {
          ResizeStrategy.Passive => PassiveStrategy,
          ResizeStrategy.Rescale => RescaleStrategy,
          ResizeStrategy.Resist => ResistStrategy,
          ResizeStrategy.Wrap => WrapStrategy,
        };
      public static ResizeStrategy ToEnum(ResizeStrategyBase strategyBase)
        => strategyBase switch
        {
          TextBlock.PassiveStrategy => ResizeStrategy.Passive,
          TextBlock.RescaleStrategy => ResizeStrategy.Rescale,
          TextBlock.ResistStrategy => ResizeStrategy.Resist,
          TextBlock.WrapStrategy => ResizeStrategy.Wrap
        };
    }
  }

}