using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;

namespace CrabUI
{
  public class CUIMainComponent : CUIStructuralComponent
  {
    public void DrawChildren(CUISpriteBatch spriteBatch)
    {
      void DrawChildrenRec(CUISpriteBatch spriteBatch, CUIStructuralComponent component)
      {
        if (component is IDrawable drawable)
        {
          drawable.Draw(spriteBatch);
        }

        foreach (CUIStructuralComponent child in component.Children)
        {
          DrawChildrenRec(spriteBatch, child);
        }

        foreach (CUIStructuralComponent child in component.TopChildren)
        {
          DrawChildrenRec(spriteBatch, child);
        }
      }

      DrawChildrenRec(spriteBatch, this);
    }
  }
}