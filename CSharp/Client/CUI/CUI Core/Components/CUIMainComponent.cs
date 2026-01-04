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

    public CUIVisualFlattener Flattener = new CUIVisualFlattener();
    public CUIVisualDrawer Drawer = new CUIVisualDrawer();



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
    }

    public void AttachToEnvironment(CUIEnvironment environment)
    {
      environment.LifeCycle.DrawBeforeGUI += DrawChildren;
      environment.LifeCycle.Update += Update;
    }
  }
}