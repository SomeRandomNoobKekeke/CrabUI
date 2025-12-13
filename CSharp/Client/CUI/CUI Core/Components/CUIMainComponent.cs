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

    public TreeFlattenerModule TreeFlattenerModule;

    protected override void InitModules()
    {
      base.InitModules();

      TreeFlattenerModule = new TreeFlattenerModule(this);
    }

    public void DrawChildren(CUISpriteBatch spriteBatch)
    {
      foreach (var component in TreeFlattenerModule.Flat)
      {
        if (component is IDrawable drawable)
        {
          drawable.Draw(spriteBatch);
        }
      }
    }

    public void Update()
    {
      if (TreeNodeModule.TreeChanged)
      {
        TreeFlattenerModule.Flatten();
        TreeNodeModule.TreeChanged = false;
      }
    }

    public void AttachToEnvironment(CUIEnvironment environment)
    {
      environment.LifeCycle.DrawBeforeGUI += DrawChildren;
      environment.LifeCycle.Update += Update;
    }
  }
}