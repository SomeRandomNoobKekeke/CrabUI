using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIMainComponent : CUIComponent
  {

    public CUIInput Input;//TODO

    public VisualFlattener VisualFlattener = new();
    public LayoutFlattener LayoutFlattener = new();

    public ChainDrawer Drawer = new ChainDrawer();
    public EventDispatcher EventDispatcher = new();
    public EventConstructor EventConstructor = new();
    public EventTargets EventTargets = new();

    public GlobalEventsWrapper GlobalEvents = new();


    private bool GlobalLayoutChanged;
    public void LayoutChanged() => GlobalLayoutChanged = true;

    public void DrawChildren(CUISpriteBatch spriteBatch)
    {
      Drawer.Draw(spriteBatch, VisualFlattener.Flat);
    }

    public void Update()
    {
      if (TreeChanged)
      {
        TreeChanged = false;
        GlobalLayoutChanged = true;
        VisualFlattener.Flatten(this);
        LayoutFlattener.Flatten(this);
      }

      if (CUI.Instance.Input.SomethingHappened)
      {
        HandleInput();
      }

      if (GlobalLayoutChanged)
      {
        GlobalLayoutChanged = false;
        UpdateLayout();
      }
    }

    private void HandleInput()
    {
      EventTargets.Find(VisualFlattener.Flat, CUI.Instance.Input.Mouse.Pos);
      EventConstructor.Construct(CUI.Instance.Input);

      //TODO This should be a real debug log
      // foreach (IEventConsumer target in targets)
      // {
      //   CUI.Logger.Log($"{target} {Logger.Wrap.IEnumerable(events)}");
      // }

      EventDispatcher.Dispatch(GlobalEvents, EventConstructor.Events);
      EventDispatcher.Dispatch(EventTargets, EventConstructor);
    }

    private void UpdateLayout()
    {
      foreach (CUIComponent component in LayoutFlattener.Flat)
      {
        component.Layout.UpdateChildren();
      }
    }

    public CUIMainComponent()
    {

    }
  }
}