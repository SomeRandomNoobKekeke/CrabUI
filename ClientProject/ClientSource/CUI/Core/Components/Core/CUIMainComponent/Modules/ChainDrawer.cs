using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUILibs;
using CUICodeGenerator;

namespace CursedUI
{
  public class ChainDrawer : IModule
  {
    public ChainDrawerStateMachine StateMachine { get; } = new();

    public void Draw(CUISpriteBatch spriteBatch, List<VisualUnit> flat)
    {
      StateMachine.Init(spriteBatch);

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
              StateMachine.Enter(spriteBatch, left.Bounds);
              break;
            case VisualBounds.RightContextBound right:
              // leave context
              StateMachine.Exit(spriteBatch, right.Bounds);
              break;
            default:
              throw new Exception("Unexpected VisualUnit");
              break;
          }
        }
      }
      finally
      {
        StateMachine.Finalize(spriteBatch);
      }
    }
  }
}