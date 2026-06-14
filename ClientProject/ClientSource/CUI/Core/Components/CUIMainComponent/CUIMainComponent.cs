using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;
using ComponentGenerator;

namespace CrabUI
{
  [GeneratedComponent]
  public partial class CUIMainComponent : CUIComponent, IComponent
  {
    public class Part : IPart { public CUIMainComponent Self { get; set; } }

    public bool Frozen { get; set; }
    public double UpdateInterval = 1.0 / 60.0;

    protected VisualFlattener VisualFlattener { get; } = new();
    protected LayoutFlattener LayoutFlattener { get; } = new();
    protected ChainDrawer ChainDrawer { get; } = new();
    protected EventDispatcher EventDispatcher { get; } = new();
    protected EventTargets EventTargets { get; } = new();


    public EventConstructor EventConstructor { get; set; } // Injected from CUICore //TODO use ComponentGenerator
    public FocusTracker FocusTracker { get; } = new(); //BRUH should this be public?
    public GrabbedHandleTracker GrabbedHandleTracker { get; } = new();  //BRUH should this be public?

    public bool MouseOverSomeElement => EventTargets.TopTarget != null;

    public void DrawChildren(CUISpriteBatch spriteBatch)
    {
      ChainDrawer.Draw(spriteBatch, VisualFlattener.Flat);
    }

    public void Step()
    {
      Update(LastUpdateTime + UpdateInterval, null);
    }


    public bool RequireLayoutUpdate { get; set; }
    public bool RequireVisualRestructure { get; set; }


    private double LastUpdateTime;
    public void Update(double totalTime, CUIInput Input)
    {
      if (Tree.Changed)
      {
        Tree.Changed = false;
        RequireVisualRestructure = true;
        RequireLayoutUpdate = true;
        LayoutFlattener.Flatten(this);
      }

      if (RequireLayoutUpdate)
      {
        RequireLayoutUpdate = false;
        UpdateLayout();
      }

      if (Input is not null && Input.SomethingHappened)
      {
        HandleInput(Input);
      }

      if (RequireVisualRestructure)
      {
        RequireVisualRestructure = false;
        VisualFlattener.Flatten(this);
      }

      LastUpdateTime = totalTime;
    }

    private void HandleInput(CUIInput Input)
    {
      FocusTracker.Reset();

      EventTargets.Find(VisualFlattener.Flat, Input.Mouse.Pos);

      EventDispatcher.Dispatch(EventTargets.PrevTargets, EventConstructor.MouseOffEvent);
      EventDispatcher.Dispatch(EventTargets.Targets, EventConstructor.MouseOnEvent);

      IEnumerable<IEventConsumer> entered = EventTargets.Targets.Except(EventTargets.PrevTargets);
      IEnumerable<IEventConsumer> leaved = EventTargets.PrevTargets.Except(EventTargets.Targets);
      EventDispatcher.Dispatch(leaved, EventConstructor.MouseLeaveEvent);
      EventDispatcher.Dispatch(entered, EventConstructor.MouseEnterEvent);

      EventDispatcher.Dispatch(GlobalEvents, EventConstructor.Events);
      EventDispatcher.Dispatch(EventTargets.Targets, EventConstructor.Events);

      FocusTracker.CheckFocusLost(Input, EventTargets);
    }



    private void UpdateLayout()
    {
      for (int i = LayoutFlattener.Flat.Count - 1; i >= 0; i--)
      {
        LayoutFlattener.Flat[i].Layout.UpdateParent();
      }

      for (int i = 0; i < LayoutFlattener.Flat.Count; i++)
      {
        LayoutFlattener.Flat[i].Layout.UpdateChildren();
      }


      // DebugChannels["Layout Updated"].Send(this);
    }

    public CUIMainComponent() : base()
    {

    }
  }
}