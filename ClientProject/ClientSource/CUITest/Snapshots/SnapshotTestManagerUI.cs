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
using System.IO;

namespace CrabUIUser
{
  public class SnapshotTestManagerUI : CUIPage
  {
    public SnapshotTestManagerUI(SnapshotTestManager manager)
    {
      Manager = manager;
      CreateUI();
    }

    public SnapshotTestManager Manager { get; set; }

    public CUIVerticalList ButtonList { get; set; }

    public void UpdateTests()
    {
      RemoveAllChildren();
      Manager.Dismantle();
    }


    public void HandleManagerEvent(SnapshotTestManager.Event e)
    {
      if (e.Name == "passed")
      {
        CUIButton btn = (CUIButton)ButtonList.Children.First(c => (c as CUIButton).Text == e.Test.Name);
        btn.MasterColor = Color.Lime;
      }

      if (e.Name == "failed")
      {
        CUIButton btn = (CUIButton)ButtonList.Children.First(c => (c as CUIButton).Text == e.Test.Name);
        btn.MasterColor = Color.Red;
      }
    }

    public void HandleOpen()
    {
      Manager.Setup();
      Refresh();
    }

    public void HandleClose()
    {
      ButtonList.RemoveAllChildren();
      Manager.Dismantle();
    }

    public void Refresh()
    {
      ButtonList.RemoveAllChildren();
      foreach (SnapshotTest test in Manager.Repo.Tests.Values)
      {
        ButtonList.Append(new CUIButton()
        {
          Text = test.Name,
          TextAnchor = CUIAnchor.LeftCenter,
          MasterColor = new Color(64, 64, 64),
          AddMouseDown = (c, e) => Manager.Run(test.Name),
        });
      }
    }

    public void CreateUI()
    {
      Manager.Events.Add(HandleManagerEvent);

      OnOpen.Add(HandleOpen);
      OnClose.Add(HandleClose);

      BackgroundColor = new Color(32, 32, 32);

      this["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1), };

      this["layout"]["controls"] = new CUIHorizontalList() { FitContent = new CUIBool2(false, true), };
      this["layout"]["controls"]["runall"] = new CUIButton()
      {
        Text = "Run All",
        Flex = 1,
        MasterColor = new Color(64, 0, 64),
        AddMouseDown = (c, e) => Manager.RunAll(),
      };
      this["layout"]["controls"]["accept"] = new CUIButton()
      {
        Text = "Accept",
        Flex = 1,
        MasterColor = new Color(64, 0, 64),
        AddMouseDown = (c, e) => Manager.AcceptCurrent()
      };

      this["layout"]["groups"] = new CUIHorizontalList() { FitContent = new CUIBool2(false, true), };
      foreach (string group in Manager.Repo.Groups)
      {
        this["layout"]["groups"].Append(new CUIButton(group)
        {
          AddMouseDown = (c, e) => Manager.AcceptCurrent()
        });
      }


      this["layout"]["btnlist"] = ButtonList = new CUIVerticalList()
      {
        Flex = 1,
        Scrollable = true,
      };
    }
  }
}