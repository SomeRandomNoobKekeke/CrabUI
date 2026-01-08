using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;
namespace CrabUI
{
  public class CUIMainComponent : CUIComponent
  {

    public CUIInput Input;//TODO

    public VisualFlattener Flattener = new VisualFlattener();
    public ChainDrawer Drawer = new ChainDrawer();
    public EventDispatcher EventDispatcher = new();
    public EventConstructor EventConstructor = new();
    public EventTargetFinder EventTargetFinder = new();

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

      if (CUI.Instance.Input.SomethingHappened)
      {
        HandleInput();
      }


    }

    //TODO reuse lists
    private void HandleInput()
    {
      List<IEventConsumer> targets = EventTargetFinder.FindTargets(Flattener.Flat, CUI.Instance.Input.Mouse.Pos).ToList();

      List<InputEvent> events = EventConstructor.ConstructEvents(CUI.Instance.Input).ToList();

      //TODO This should be a real debug log
      // foreach (IEventConsumer target in targets)
      // {
      //   CUI.Logger.Log($"{target} {Logger.Wrap.IEnumerable(events)}");
      // }


      EventDispatcher.Dispatch(targets, events);
    }
  }
}