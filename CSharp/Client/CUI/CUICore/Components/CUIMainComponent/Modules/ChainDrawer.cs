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
    public DebugNode<VisualUnit> Debug_DrawVisualUnit = new();

    public void Draw(ICUISpriteBatch spriteBatch, List<VisualUnit> flat)
    {
      foreach (VisualUnit unit in flat)
      {
        Debug_DrawVisualUnit.Send(unit);

        switch (unit)
        {
          case VisualUnit.PrimitiveVisualElement primitive:
            primitive.Element.Draw(spriteBatch);
            break;
          case VisualUnit.LeftContextBound left:
            // enter context
            break;
          case VisualUnit.RightContextBound right:
            // leave context
            break;
          default:
            throw new Exception("Unexpected VisualUnit");
            break;
        }
      }
    }
  }
}