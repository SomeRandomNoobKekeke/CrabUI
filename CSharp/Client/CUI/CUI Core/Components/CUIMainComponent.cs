using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;

namespace CrabUI
{
  public class CUIMainComponent : CUIComponent
  {

    public VisualFlattener Flattener = new VisualFlattener();
    public ChainDrawer Drawer = new ChainDrawer();
    public EventDispatcher EventDispatcher = new();



    public void DrawChildren(CUISpriteBatch spriteBatch)
    {
      Drawer.Draw(spriteBatch, Flattener.Flat);
    }

    public void Update()
    {
      if (TreeChanged)
      {
        TreeChanged = false;
        Flattener.Flatten(this);
      }

      EventDispatcher.Dispatch(Flattener.Flat, CUI.Instance.Input);
    }
  }
}