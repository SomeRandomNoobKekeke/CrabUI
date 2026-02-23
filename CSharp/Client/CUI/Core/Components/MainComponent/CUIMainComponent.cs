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
    public CUICore CUICore { get; }
    public CUIInput Input => CUICore.Input;

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

    public void Update(double totalTime)
    {
      if (TreeChanged)
      {
        TreeChanged = false;
        GlobalLayoutChanged = true;
        VisualFlattener.Flatten(this);
        LayoutFlattener.Flatten(this);
      }

      if (Input.SomethingHappened)
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
      EventTargets.Find(VisualFlattener.Flat, Input.Mouse.Pos);
      EventConstructor.Construct(Input);

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

    public CUIMainComponent(CUICore core)
    {
      CUICore = core;
    }
  }
}