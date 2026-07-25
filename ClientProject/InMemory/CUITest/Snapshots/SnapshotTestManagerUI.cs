using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CrabUI;
using Microsoft.Xna.Framework;
using System.IO;

namespace CrabUIUser
{
  public class SnapshotPage : CUIPage
  {
    public SnapshotPage(SnapshotTestManager manager)
    {
      Manager = manager;
      Manager.Events.Add(HandleManagerEvent);

      OnOpen.Add(HandleOpen);
      OnClose.Add(HandleClose);
    }

    public SnapshotTestManager Manager { get; set; }

    public CUIVerticalList ButtonList { get; set; }

    public void UpdateTests()
    {
      Children.Clear();
      Manager.Dismantle();
    }


    public void HandleManagerEvent(SnapshotTestManager.Event e)
    {
      foreach (CUIVisualComponent child in ButtonList.Children)
      {
        if (child is not CUIButton button) continue;

        if (button.Text == e.Test.Name)
        {
          button.MasterColor = e.Name == "passed" ? Color.Lime : Color.Red;
        }
      }
    }

    public void HandleOpen()
    {
      Manager.Setup();
      Refresh();
    }

    public void HandleClose()
    {
      Manager.Dismantle();
    }

    public void OpenGroup(string name)
    {
      ButtonList.Children.Clear();
      foreach (SnapshotTest test in Manager.Repo.GroupedTests[name].Values)
      {
        ButtonList.Add(new CUIButton()
        {
          Text = test.Name,
          TextAnchor = CUIAnchor.LeftCenter,
          MasterColor = new Color(64, 64, 64),
          OnMouseDown = (c, e) => Manager.Run(test.Name),
        });
      }
    }

    public void Refresh()
    {
      Children.Clear();
      Background.Color = new Color(32, 32, 32);

      this["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1), };

      this["layout"]["controls"] = new CUIHorizontalList() { FitContent = new CUIBool2(false, true), };
      this["layout"]["controls"]["runall"] = new CUIButton()
      {
        Text = "Run All",
        Flex = 1,
        MasterColor = new Color(64, 0, 64),
        OnMouseDown = (c, e) => Manager.RunAll(),
      };
      this["layout"]["controls"]["accept"] = new CUIButton()
      {
        Text = "Accept",
        Flex = 1,
        MasterColor = new Color(64, 0, 64),
        OnMouseDown = (c, e) => Manager.AcceptCurrent()
      };
      this["layout"]["controls"]["serialize"] = new CUIToggleButton()
      {
        Text = "Serialize",
        Flex = 1,
        MasterColor = new Color(255, 0, 255),

        State = Manager.SerializeTestSubject,
        OnToggle = (state) => Manager.SerializeTestSubject = state,
      };

      this["layout"]["groups"] = new CUIHorizontalList() { FitContent = new CUIBool2(false, true), };
      foreach (string group in Manager.Repo.Groups)
      {
        this["layout"]["groups"].Children.Add(new CUIButton(group)
        {
          OnMouseDown = (c, e) => OpenGroup(group),
        });
      }

      this["layout"]["btnlist"] = ButtonList = new CUIVerticalList()
      {
        Flex = 1,
        Scrollable = true,
      };

      OpenGroup(Manager.Repo.Groups.First());
    }
  }
}