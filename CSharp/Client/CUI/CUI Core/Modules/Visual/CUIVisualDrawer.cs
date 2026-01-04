using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class CUIVisualDrawer : IModule
  {
    public void Draw(CUISpriteBatch spriteBatch, List<VI.VisualUnit> flat)
    {


      foreach (VI.VisualUnit unit in flat)
      {
        switch (unit)
        {
          case VI.PrimitiveVisualElement primitive:
            primitive.Element.Draw(spriteBatch);
            break;
          case VI.LeftContextBound left:
            // enter context
            break;
          case VI.RightContextBound right:
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