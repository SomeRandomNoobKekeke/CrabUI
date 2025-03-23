#define CUIDEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace CrabUI
{
  public class CUIDebugWindow : CUIFrame
  {
    public static CUIDebugWindow Main;

    public CUIVerticalList EventsComponent;
    public CUIVerticalList DebugIDsComponent;
    public CUIPages Pages;
    public CUIMultiButton PickIDButton;

    public List<CUIDebugEventComponent> Events = new List<CUIDebugEventComponent>();
    public int target;
    public bool Loop { get; set; } = true;



    public void Capture(CUIDebugEvent e)
    {
      if (EventsComponent == null) return;

      if (target > 200) return;

      if (Events.Count < target + 1)
      {
        CUIDebugEventComponent ec = new CUIDebugEventComponent(e);
        Events.Add(ec);
        EventsComponent.Append(ec);

        ec.OnMouseEnter += (m) => ec.Value.Target.DebugHighlight = true;
        ec.OnMouseLeave += (m) => ec.Value.Target.DebugHighlight = false;
      }
      else
      {
        Events[target].Value = e;
      }

      target++;
    }

    public void Flush()
    {
      if (Loop) target = 0;
      //Events.ForEach(e => e.Flush());
    }

    public void MakeIDList()
    {
      DebugIDsComponent.Visible = false;
      DebugIDsComponent.RemoveAllChildren();

      List<CUIComponent> l = new List<CUIComponent>();

      if (CUI.Main is not null)
      {
        RunRecursiveOn(CUI.Main, (component) =>
        {
          if (!component.IgnoreDebug) l.Add(component);
        });
      }

      foreach (CUIComponent c in l)
      {
        CUIToggleButton b = new CUIToggleButton(c.ToString())
        {
          State = c.Debug,
          IgnoreDebug = true,
          Style = new CUIStyle(){
            {"TextAlign", "[0,0]"}
          },
          AddOnMouseDown = (m) =>
          {
            c.Debug = !c.Debug;
            MakeIDList();
          },
          AddOnMouseEnter = (m) => c.DebugHighlight = true,
          AddOnMouseLeave = (m) => c.DebugHighlight = false,
        };

        DebugIDsComponent.Append(b);
      }
      DebugIDsComponent.Visible = true;
      l.Clear();
    }

    public CUIDebugWindow() : base()
    {
      this.ZIndex = 1000;
      this.Layout = new CUILayoutVerticalList();

      this["handle"] = new CUIComponent()
      {
        Absolute = new CUINullRect(null, null, null, 20),
      };

      this["handle"]["closebutton"] = new CUIButton("X")
      {
        Anchor = new Vector2(1, 0.5f),
        Style = new CUIStyle(){
          {"MasterColor", "Red"}
        },
        AddOnMouseDown = (e) =>
        {
          CUIDebugWindow.Close();
        },
      };

      this["controls"] = new CUIComponent()
      {
        FitContent = new CUIBool2(false, true),
      };

      this["controls"]["loop"] = new CUIToggleButton("loop")
      {
        Relative = new CUINullRect(0, 0, 0.5f, null),
        AddOnStateChange = (state) =>
        {
          Loop = state;
          Events?.Clear();
          EventsComponent?.RemoveAllChildren();
        },
        State = Loop,
      };


      this["controls"].Append(PickIDButton = new CUIMultiButton()
      {
        Relative = new CUINullRect(0.5f, 0, 0.5f, null),
        Style = new CUIStyle(){
          {"InactiveColor", "0,0,0,128"},
          {"MousePressedColor", "0,255,255,64"}
        },
        ConsumeDragAndDrop = false,

        Options = new string[]{
          "Debug events", "Debugged components"
        }
      });

      Append(Pages = new CUIPages()
      {
        FillEmptySpace = new CUIBool2(false, true),
        Style = new CUIStyle(){
          {"BackgroundColor", "0,0,32,128"}
        },
        IgnoreDebug = true,
      });

      EventsComponent = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
        Scrollable = true,
        IgnoreDebug = true,
      };

      DebugIDsComponent = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
        Scrollable = true,
        IgnoreDebug = true,
      };

      PickIDButton.OnSelect += (s) =>
      {
        if (PickIDButton.SelectedIndex == 0)
        {
          MakeIDList();
          Pages.Open(EventsComponent);
        }
        else Pages.Open(DebugIDsComponent);
      };
      PickIDButton.Select(0);

      this["controls"].Get<CUIToggleButton>("loop").State = true;

      IgnoreDebug = true;
    }

    public static CUIDebugWindow Open()
    {
      if (CUI.Main == null) return null;

      CUIDebugWindow w = new CUIDebugWindow()
      {
        Absolute = new CUINullRect(10, 370, 500, 370),
      };
      CUI.Main.Append(w);
      CUIDebugWindow.Main = w;
      CUI.Main.OnTreeChanged += () => w.MakeIDList();
      return w;
    }

    public static void Close()
    {
      if (CUIDebugWindow.Main == null) return;

      CUIDebugWindow.Main.RemoveSelf();
      CUIDebugWindow.Main.Revealed = false;
      CUIDebugWindow.Main = null;
    }

    public CUIDebugWindow(float? x = null, float? y = null, float? w = null, float? h = null) : this()
    {
      Relative = new CUINullRect(x, y, w, h);
    }
  }
}