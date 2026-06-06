using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class VisualBounds
  {
    public class LeftContextBound : VisualUnit
    {
      public LeftContextBound(VisualBounds bounds) => Bounds = bounds;
      public VisualBounds Bounds { get; }
      public void Enter(CUISpriteBatch spriteBatch, Rectangle? scissorRect) => Bounds.Enter(spriteBatch, scissorRect);
    }


    public class RightContextBound : VisualUnit
    {
      public RightContextBound(VisualBounds bounds) => Bounds = bounds;
      public VisualBounds Bounds { get; }
      public Rectangle? Exit(CUISpriteBatch spriteBatch) => Bounds.Exit(spriteBatch);
    }

    public VisualBounds()
    {
      LeftBound = new LeftContextBound(this);
      RightBound = new RightContextBound(this);
    }

    public LeftContextBound LeftBound { get; }
    public RightContextBound RightBound { get; }

    public Rectangle? PrevState { get; set; }
    public Rectangle? ScissorRect { get; set; }

    public void Enter(CUISpriteBatch spriteBatch, Rectangle? prevState)
    {
      PrevState = prevState;
      if (ScissorRect.HasValue)
      {
        spriteBatch.StopStart(ScissorRect.Value);
      }
    }
    public Rectangle? Exit(CUISpriteBatch spriteBatch)
    {
      if (ScissorRect.HasValue && PrevState.HasValue)
      {
        spriteBatch.StopStart(PrevState.Value);
      }

      return PrevState;
    }




  }


}