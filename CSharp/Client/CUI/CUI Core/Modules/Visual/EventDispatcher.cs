using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public partial class EventDispatcher : IModule
  {
    private void NotifyElement(IVisualElement element, CUIInput input)
    {
      if (element.Rect.Contains(input.MousePosition))
      {
        element.HandleInput(input);
      }
    }


    public void Dispatch(List<VisualUnit> flat, CUIInput input)
    {
      for (int i = flat.Count - 1; i >= 0; i--)
      {
        switch (flat[i])
        {
          case VisualUnit.PrimitiveVisualElement primitive:
            NotifyElement(primitive.Element, input);
            break;
          case VisualUnit.LeftContextBound left:
            // leave context
            break;
          case VisualUnit.RightContextBound right:
            // enter context
            break;
          default:
            throw new Exception("Unexpected VisualUnit");
            break;
        }
      }
    }
  }
}