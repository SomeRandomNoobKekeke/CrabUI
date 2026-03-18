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


    protected VisualFlattener VisualFlattener { get; } = new();
    protected LayoutFlattener LayoutFlattener { get; } = new();
    protected ChainDrawer Drawer { get; } = new();
    protected EventDispatcher EventDispatcher { get; } = new();
    protected EventConstructor EventConstructor { get; } = new();
    protected EventTargets EventTargets { get; } = new();



    private bool GlobalLayoutChanged;
    protected override void NotifyThatLayoutHasChanged() => GlobalLayoutChanged = true;

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