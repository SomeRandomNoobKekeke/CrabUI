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


namespace CrabUI
{
  public class CUIDebuggerOld
  {
    public ClearableEvent<DebugEvent> Input { get; } = new();
    public ClearableEvent<double> OnUpdate { get; } = new();

    public CUIButton OpenButton { get; private set; }

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

      Close();
    }

    public void Dispose() { }

    public void CreateGUI()
    {
      OpenButton = new CUIButton()
      {
        Text = "Debug",
        Anchor = CUIAnchor.LeftCenter,
        BackgroundColor = Color.Blue,
        TextAnchor = CUIAnchor.LeftCenter,
        Absolute = new CUINullRect(w: 50, h: 20),
        AddMouseDown = (c, e) => Open(),
        IsDebugTool = true,
      };


      DebugFrame = new CUIFrame()
      {
        Absolute = new CUINullRect(w: 400, h: 300),
        Anchor = CUIAnchor.LeftCenter,
        BackgroundColor = Color.Black,
        Resizable = true,
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
        Flex = new CUINullVector2(1, 1),
      };

      DebugFrame["layout"]["handle"]["close"] = new CUIButton()
      {
        Text = "X",
        Absolute = new CUINullRect(w: 20, h: 20),
        MasterColor = Color.Red,
        AddMouseDown = (c, e) => Close(),
      };


      DebugFrame["layout"]["header"] = new CUIHorizontalList()
      {
        Absolute = new CUINullRect(h: 30),
      };

      DebugFrame["layout"]["header"]["events"] = new CUIButton()
      {
        Text = "Events",
        Flex = new CUINullVector2(1, 1),
        MasterColor = Color.Blue,
        AddMouseDown = (c, e) => Pages.Open(EventsPage),
      };

      DebugFrame["layout"]["header"]["components"] = new CUIButton()
      {
        Text = "Components",
        Flex = new CUINullVector2(1, 1),
        MasterColor = Color.Blue,
        AddMouseDown = (c, e) => Pages.Open(ComponentsPage),
      };

      DebugFrame["layout"]["header"]["gates"] = new CUIButton()
      {
        Text = "Gates",
        Flex = new CUINullVector2(1, 1),
        MasterColor = Color.Blue,
        AddMouseDown = (c, e) => Pages.Open(GatesPage),
      };



      DebugFrame["layout"]["pages"] = Pages = new CUIPages()
      {
        Flex = new CUINullVector2(1, 1),
        BackgroundColor = Color.Green,
      };

      EventsPage = new CUIPage()
      {
        AddOnOpen = OnEventsPageOpen
      };

      EventsPage["list"] = EventList = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
        Scrollable = true,
      };

      ComponentsPage = new CUIPage()
      {
        BackgroundColor = Color.Red,
        AddOnOpen = OnComponentsPageOpen
      };

      ComponentsPage["list"] = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
        Scrollable = true,
      };

      GatesPage = new CUIPage()
      {
        BackgroundColor = Color.Blue,
        AddOnOpen = OnGatesPageOpen
      };
      GatesPage["list"] = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
        Scrollable = true,
      };

      // frame["layout"]["main"].Get<CUIPages>("main").Open()

      DebugFrame.IsDebugTool = true;

      Pages.Open(EventsPage);
    }

    public void HandleDebugEvent(DebugEvent e)
    {
      if (Pages.OpenedPage != EventsPage) return;

      if (EventList.Children.Count > 10)
      {
        EventList.RemoveChild(EventList.Children.Last());
      }

      EventList.Insert(new CUITextBlock()
      {
        Text = e.ToString(),
        Absolute = new CUINullRect(h: 20),
        TextAnchor = new Vector2(0, 0.5f),
      }, 0);
    }

    public void OnEventsPageOpen()
    {
      EventList.Clear();
    }

    public void OnComponentsPageOpen()
    {
      ComponentsPage.Get<CUIVerticalList>("list").Clear();

      foreach (CUIComponent child in CUI.Main.DeepChildren.Append(CUI.Main).ToList())
      {
        CUITextBlock node = new CUITextBlock()
        {
          Text = child.ToString(),
          Absolute = new CUINullRect(h: 20),
          TextAnchor = new Vector2(0, 0.5f),
          BackgroundColor = child.Debug ? new Color(0, 200, 0) : Color.Blue,
          IsDebugTool = true,
        };

        node.MouseDown += (c, e) =>
        {
          child.DeepDebug = !child.DeepDebug;
          OnComponentsPageOpen();
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
          BackgroundColor = CUI.DebugHub.Gates[name].IsOpen ? new Color(0, 200, 0) : Color.Blue,
        };

        node.MouseDown += (c, e) =>
        {
          CUI.DebugHub.Gates[name].Toggle();
          node.BackgroundColor = CUI.DebugHub.Gates[name].IsOpen ? new Color(0, 200, 0) : Color.Blue;
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
      OpenButton.RemoveSelf();
      DebugFrame.Open(CUI.TopMain);
      CUI.DebugHub.Output.Map(Input);
      CUI.Core.LifeCycle.OnUpdate.Map(OnUpdate);
    }
    public void Close()
    {
      IsOpen = false;
      CUI.TopMain["open debug button"] = OpenButton;

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