using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
namespace CrabUI
{
  public partial class CUIComponent : CUIComponentCore, CUIRectComponent, IDrawable, IComponentTreeNode
  {
    public TreeNodeModule TreeNodeModule { get; set; }

    public Rectangle Rect { get; set; }

    public SimpleDrawer Drawer { get; set; }

    public void Draw(CUISpriteBatch spriteBatch)
    {
      Drawer.Draw(spriteBatch);
    }

    protected virtual void InitModules()
    {
      Drawer = new SimpleDrawer(this);
      TreeNodeModule = new TreeNodeModule(this);
    }

    public CUIComponent()
    {
      InitModules();
    }


    public override string ToString() => $"{this.GetType().Name} [{this.ID}]";
  }
}