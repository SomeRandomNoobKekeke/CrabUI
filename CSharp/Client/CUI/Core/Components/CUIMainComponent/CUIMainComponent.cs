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
  public partial class CUIMainComponent : CUIComponent, IComponent
  {
    public class Part : IPart { public CUIMainComponent Self { get; set; } }

    public bool Frozen { get; set; }
    public double UpdateInterval = 1.0 / 60.0;

    public CUIRect Rect
    {
      get => FunnyProps.Rect.Value;
      set => FunnyProps.Rect.Value = value;
    }

    protected VisualFlattener VisualFlattener { get; } = new();
    protected LayoutFlattener LayoutFlattener { get; } = new();
    protected ChainDrawer ChainDrawer { get; } = new();
    protected EventDispatcher EventDispatcher { get; } = new();
    protected EventConstructor EventConstructor { get; } = new();
    protected EventTargets EventTargets { get; } = new();

    private bool GlobalLayoutChanged;

    public bool MouseOverSomeElement => EventTargets.TopTarget != null;
    public void NotifyThatLayoutHasChanged() => GlobalLayoutChanged = true;

    public void DrawChildren(CUISpriteBatch spriteBatch)
    {
      ChainDrawer.Draw(spriteBatch, VisualFlattener.Flat);
    }

    public void Step()
    {
      Update(LastUpdateTime + UpdateInterval, null);
    }

    private double LastUpdateTime;
    public void Update(double totalTime, CUIInput Input)
    {
      if (Tree.Changed)
      {
        Tree.Changed = false;
        GlobalLayoutChanged = true;
        VisualFlattener.Flatten(this);
        LayoutFlattener.Flatten(this);
      }

      if (Input is not null && Input.SomethingHappened)
      {
        HandleInput(Input);
      }

      if (GlobalLayoutChanged)
      {
        GlobalLayoutChanged = false;
        UpdateLayout();
      }

      LastUpdateTime = totalTime;
    }

    private void HandleInput(CUIInput Input)
    {
      EventTargets.Find(VisualFlattener.Flat, Input.Mouse.Pos);
      EventConstructor.Construct(Input);


      EventDispatcher.Dispatch(EventTargets.PrevTargets, EventConstructor.MouseOffEvent);
      EventDispatcher.Dispatch(EventTargets.Targets, EventConstructor.MouseOnEvent);

      IEnumerable<IEventConsumer> entered = EventTargets.Targets.Except(EventTargets.PrevTargets);
      IEnumerable<IEventConsumer> leaved = EventTargets.PrevTargets.Except(EventTargets.Targets);
      EventDispatcher.Dispatch(leaved, EventConstructor.MouseLeaveEvent);
      EventDispatcher.Dispatch(entered, EventConstructor.MouseEnterEvent);

      Debug_MouseEnter.Send(entered);
      Debug_MouseLeave.Send(leaved);


      EventDispatcher.Dispatch(GlobalEvents, EventConstructor.Events);
      EventDispatcher.Dispatch(EventTargets.Targets, EventConstructor.Events);
    }



    private void UpdateLayout()
    {
      foreach (CUIComponent component in LayoutFlattener.Flat)
      {
        component.Layout.UpdateChildren();
      }

      // DebugChannels["Layout Updated"].Send(this);
    }

    public CUIMainComponent() : base()
    {

    }
  }
}