using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
namespace CrabUI
{
  public partial class CUIComponent : CUIComponentCore, CUIStructuralComponent, CUIRectComponent, IDrawable
  {
    public List<CUIStructuralComponent> TopChildren { get; } = new();
    public List<CUIStructuralComponent> Children { get; } = new();
    public Rectangle Rect { get; set; }

    public SimpleDrawer Drawer { get; set; }

    public void Draw(CUISpriteBatch spriteBatch)
    {
      Drawer.Draw(spriteBatch);
    }

    private void InitModules()
    {
      Drawer = new SimpleDrawer(this);
    }

    public CUIComponent()
    {
      InitModules();
    }


    public override string ToString() => $"{this.GetType().Name} [{this.ID}]";
  }
}