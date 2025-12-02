using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;

namespace CrabUI
{
  public partial class CUIComponent : CUIStructuralComponent, IDrawable
  {


    public void Draw(CUISpriteBatch spriteBatch)
    {

    }

    private void InitModules()
    {

    }

    public CUIComponent()
    {
      InitModules();
    }


    public override string ToString() => $"{this.GetType().Name} [{this.ID}]";
  }
}