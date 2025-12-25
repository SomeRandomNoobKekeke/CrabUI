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



    protected override void InitModules()
    {

    }

    public void DrawChildren(CUISpriteBatch spriteBatch)
    {

    }

    public void Update()
    {

    }

    public void AttachToEnvironment(CUIEnvironment environment)
    {
      environment.LifeCycle.DrawBeforeGUI += DrawChildren;
      environment.LifeCycle.Update += Update;
    }
  }
}