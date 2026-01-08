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

    public CUIInput Input;//TODO

    public VisualFlattener Flattener = new VisualFlattener();
    public ChainDrawer Drawer = new ChainDrawer();
    public EventDispatcher EventDispatcher = new();
    public EventConstructor EventConstructor = new();

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

      List<CUIEvent> events = EventConstructor.ConstructEvents(CUI.Instance.Input).ToList();

      foreach (CUIEvent e in events)
      {
        CUI.Logger.Log($"{e}");
      }


      // EventDispatcher.Dispatch(Flattener.Flat, events);
    }
  }
}