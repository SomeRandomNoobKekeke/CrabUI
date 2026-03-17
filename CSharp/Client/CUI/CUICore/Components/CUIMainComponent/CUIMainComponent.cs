using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUIMainComponent : CUIComponent, IComponent
  {
    public class Part : IPart { public CUIComponent Self { get; set; } }

    public VisualFlattener VisualFlattener { get; } = new();
    public LayoutFlattener LayoutFlattener { get; } = new();
    public ChainDrawer Drawer { get; } = new();
    public EventDispatcher EventDispatcher { get; } = new();
    public EventConstructor EventConstructor { get; } = new();
    public EventTargets EventTargets { get; } = new();
    public GlobalEventsWrapper GlobalEvents { get; } = new();


    private bool GlobalLayoutChanged;
    public void LayoutChanged() => GlobalLayoutChanged = true;

    public void DrawChildren(ICUISpriteBatch spriteBatch)
    {
      // Drawer.Draw(spriteBatch, VisualFlattener.Flat);
    }

    public void Update(double totalTime, CUIInput Input)
    {
      if (Tree.Changed)
      {
        Tree.Changed = false;
        GlobalLayoutChanged = true;
        VisualFlattener.Flatten(this.Visual);
        LayoutFlattener.Flatten(this);
      }

      if (Input.SomethingHappened)
      {
        HandleInput(Input);
      }

      if (GlobalLayoutChanged)
      {
        GlobalLayoutChanged = false;
        UpdateLayout();
      }
    }

    private void HandleInput(CUIInput Input)
    {
      // EventTargets.Find(VisualFlattener.Flat, Input.Mouse.Pos);
      // EventConstructor.Construct(Input);

      // //TODO This should be a real debug log
      // // foreach (IEventConsumer target in targets)
      // // {
      // //   CUI.Logger.Log($"{target} {Logger.Wrap.IEnumerable(events)}");
      // // }

      // EventDispatcher.Dispatch(GlobalEvents, EventConstructor.Events);
      // EventDispatcher.Dispatch(EventTargets, EventConstructor);
    }

    private void UpdateLayout()
    {
      // foreach (CUIComponent component in LayoutFlattener.Flat)
      // {
      //   component.Layout.UpdateChildren();
      // }
    }
  }
}