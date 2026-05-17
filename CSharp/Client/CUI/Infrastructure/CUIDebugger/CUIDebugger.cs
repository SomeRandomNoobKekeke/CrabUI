using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;


namespace CrabUIUser
{
  public class CUIDebugger
  {
    public ClearableEvent<DebugEvent> Input { get; } = new();
    public ClearableEvent<double> OnUpdate { get; } = new();

    public CUIFrame DebugFrame { get; private set; }
    public CUIPages Pages { get; private set; }
    public CUIPage EventsPage { get; private set; }
    public CUIPage ComponentsPage { get; private set; }
    public CUIPage GatesPage { get; private set; }
    public bool IsOpen { get; private set; }

    public CUIVerticalList EventList { get; private set; }

    private bool UpdateFrameEnded;

    public void Init()
    {
      PluginCommands.Add("cuidebug", CUIDebug_Command, () => new string[][] { CUI.DebugHub.Gates.Names.ToArray() });

      CreateGUI();

      Input.Add(HandleDebugEvent);
      OnUpdate.Add((d) => UpdateFrameEnded = true);
    }

    public void CreateGUI()
    {
      DebugFrame = new CUIFrame()
      {
        Absolute = new CUINullRect(w: 400, h: 300),
        Anchor = CUIAnchor.LeftCenter,
        BackgroundColor = Color.Black,
      };

      DebugFrame["layout"] = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
      };

      DebugFrame["layout"]["handle"] = new CUIHorizontalList()
      {
        Absolute = new CUINullRect(h: 20),
        BackgroundColor = Color.Blue,
      };

      DebugFrame["layout"]["handle"]["spacer"] = new CUIComponent()
      {
        Flex = 1,
      };

      DebugFrame["layout"]["handle"]["close"] = new CUIButton()
      {
        Text = "X",
        Absolute = new CUINullRect(w: 20, h: 20),
        MasterColorOpaque = Color.Red,
        AddMouseDown = (e) => Close(),
      };


      DebugFrame["layout"]["header"] = new CUIHorizontalList()
      {
        Absolute = new CUINullRect(h: 30),
      };

      DebugFrame["layout"]["header"]["events"] = new CUIButton()
      {
        Text = "Events",
        Flex = 1,
        MasterColorOpaque = Color.Blue,
        AddMouseDown = (e) => Pages.Open(EventsPage),
      };

      DebugFrame["layout"]["header"]["components"] = new CUIButton()
      {
        Text = "Components",
        Flex = 1,
        MasterColorOpaque = Color.Blue,
        AddMouseDown = (e) => Pages.Open(ComponentsPage),
      };

      DebugFrame["layout"]["header"]["gates"] = new CUIButton()
      {
        Text = "Gates",
        Flex = 1,
        MasterColorOpaque = Color.Blue,
        AddMouseDown = (e) => Pages.Open(GatesPage),
      };



      DebugFrame["layout"]["pages"] = Pages = new CUIPages()
      {
        Flex = 1,
        BackgroundColor = Color.Green,
      };

      EventsPage = new CUIPage()
      {
        AddOnOpen = OnEventsPageOpen
      };

      EventsPage["list"] = EventList = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
      };

      ComponentsPage = new CUIPage()
      {
        BackgroundColor = Color.Red,
        AddOnOpen = OnComponentsPageOpen
      };

      ComponentsPage["list"] = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
      };

      GatesPage = new CUIPage()
      {
        BackgroundColor = Color.Blue,
        AddOnOpen = OnGatesPageOpen
      };
      GatesPage["list"] = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
      };

      // frame["layout"]["main"].Get<CUIPages>("main").Open()

      Pages.Open(EventsPage);
    }

    public void HandleDebugEvent(DebugEvent e)
    {
      if (Pages.OpenedPage != EventsPage) return;

      if (UpdateFrameEnded)
      {
        UpdateFrameEnded = false;
        EventList.RemoveAllChildren();
      }

      EventList.Append(new CUITextBlock()
      {
        Text = e.ToString(),
        Absolute = new CUINullRect(h: 20),
        TextAnchor = new Vector2(0, 0.5f),
      });
    }

    public void OnEventsPageOpen()
    {
      EventList.Clear();
    }

    public void OnComponentsPageOpen()
    {
      ComponentsPage.Get<CUIVerticalList>("list").Clear();

      foreach (CUIComponent child in CUI.Main.DeepChildren.ToList())
      {
        CUITextBlock node = new CUITextBlock()
        {
          Text = child.ToString(),
          Absolute = new CUINullRect(h: 20),
          TextAnchor = new Vector2(0, 0.5f),
          BackgroundColor = child.Debug ? Color.Lime : Color.Blue,
        };

        node.MouseDown += (e) =>
        {
          child.Debug = !child.Debug;
          node.BackgroundColor = child.Debug ? Color.Lime : Color.Blue;
        };

        ComponentsPage["list"].Append(node);
      }
    }


    public void OnGatesPageOpen()
    {
      GatesPage.Get<CUIVerticalList>("list").Clear();

      foreach (string name in CUI.DebugHub.Gates.Names)
      {
        CUITextBlock node = new CUITextBlock()
        {
          Text = name,
          Absolute = new CUINullRect(h: 20),
          TextAnchor = new Vector2(0, 0.5f),
          BackgroundColor = CUI.DebugHub.Gates[name].IsOpen ? Color.Lime : Color.Blue,
        };

        node.MouseDown += (e) =>
        {
          CUI.DebugHub.Gates[name].Toggle();
          node.BackgroundColor = CUI.DebugHub.Gates[name].IsOpen ? Color.Lime : Color.Blue;
        };

        GatesPage["list"].Append(node);
      }

    }


    public void Toggle()
    {
      if (IsOpen) Close(); else Open();
    }
    public void Open()
    {
      IsOpen = true;
      DebugFrame.Open();
      CUI.DebugHub.Output.Map(Input);
      CUI.Core.LifeCycle.OnUpdate.Map(OnUpdate);
    }
    public void Close()
    {
      IsOpen = false;
      DebugFrame.Close();
      CUI.DebugHub.Output.Unmap(Input);
      CUI.Core.LifeCycle.OnUpdate.Unmap(OnUpdate);
    }




    public void CUIDebug_Command(string[] args)
    {
      if (args.Length == 0)
      {
        Toggle();
        return;
      }

      string gate = String.Join(' ', args);
      CUI.DebugHub.Gates[gate].Toggle();
    }
  }
}