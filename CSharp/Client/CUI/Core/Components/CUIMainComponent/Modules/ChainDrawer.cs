using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  public class ChainDrawer : IModule
  {
    public void StopStartSpritebatch(CUISpriteBatch spriteBatch, Rectangle ScissorRect)
    {
      spriteBatch.StopStart(ScissorRect);
    }


    public void Draw(CUISpriteBatch spriteBatch, List<VisualUnit> flat)
    {

      Rectangle OriginalSRect = spriteBatch.ScissorRect;
      Rectangle? CurrentState = OriginalSRect;

      try
      {
        foreach (VisualUnit unit in flat)
        {
          switch (unit)
          {
            case VisualUnit.PrimitiveVisualElement primitive:
              primitive.Element.Draw(spriteBatch);
              break;
            case VisualBounds.LeftContextBound left:
              // enter context
              left.Enter(spriteBatch, CurrentState);
              break;
            case VisualBounds.RightContextBound right:
              // leave context
              CurrentState = right.Exit(spriteBatch);
              break;
            default:
              throw new Exception("Unexpected VisualUnit");
              break;
          }
        }
      }
      finally
      {
        if (spriteBatch.ScissorRect != OriginalSRect)
        {
          spriteBatch.StopStart(OriginalSRect);
        }
      }
    }
  }
}