using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using CUILibs;
using CUICodeGenerator;

namespace CrabUI
{

  public partial class CUIMainComponent
  {
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
      GlobalEvents.BeforeUpdate.Raise();

      if (Tree.Changed)
      {
        Tree.Changed = false;
        RequireVisualRestructure = true;
        RequireLayoutUpdate = true;
        LayoutFlattener.Flatten(this);
      }

      if (RequireLayoutUpdate)
      {
        UpdateLayout();
      }

      if (Input is not null && Input.SomethingHappened) //TODO can i just check event count?
      {
        HandleInput(Input);
      }

      if (RequireVisualRestructure)
      {
        RequireVisualRestructure = false;
        VisualFlattener.Flatten(this);
      }

      LastUpdateTime = totalTime;
      GlobalEvents.AfterUpdate.Raise();
    }

    private void HandleInput(CUIInput Input)
    {
      EventTargets.Find(VisualFlattener.Flat, Input.Mouse.Pos);

      EventDispatcher.Dispatch(GlobalEvents, EventConstructor.Events);
      EventDispatcher.Dispatch(GlobalEvents, EventConstructor.KeyboardEvents);

      EventDispatcher.Dispatch(EventTargets.PrevTargets, EventConstructor.MouseOffEvent);
      EventDispatcher.Dispatch(EventTargets.Targets, EventConstructor.MouseOnEvent);

      IEnumerable<IEventConsumer> entered = EventTargets.Targets.Except(EventTargets.PrevTargets);
      IEnumerable<IEventConsumer> leaved = EventTargets.PrevTargets.Except(EventTargets.Targets);
      EventDispatcher.Dispatch(leaved, EventConstructor.MouseLeaveEvent);
      EventDispatcher.Dispatch(entered, EventConstructor.MouseEnterEvent);

      EventDispatcher.Dispatch(EventTargets.Targets, EventConstructor.Events);
    }


    private void UpdateLayout()
    {
      Debug_LayoutUpdated.Send();
      Tree.Changed = false; //HACK debug events might create new nodes here, change tree and create a loop

      int repeats = 0;
      while (RequireLayoutUpdate)
      {
        RequireLayoutUpdate = false;

        for (int i = LayoutFlattener.Flat.Count - 1; i >= 0; i--)
        {
          LayoutFlattener.Flat[i].Layout.UpdateParent();
        }

        for (int i = 0; i < LayoutFlattener.Flat.Count; i++)
        {
          LayoutFlattener.Flat[i].Layout.UpdateChildren();
        }

        if (repeats++ > MaxLayoutCalcItterations)
        {
          CUI.Logger.Warning($"{this} couldn't resolve layout after {MaxLayoutCalcItterations} itterations!");
          break;
        }
      }
    }
  }
}