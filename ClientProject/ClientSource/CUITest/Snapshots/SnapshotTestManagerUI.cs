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

    public void CreateUI()
    {
      Manager.Events.Add(HandleManagerEvent);

      OnOpen.Add(() =>
      {
        Manager.Setup();
        ButtonList.RemoveAllChildren();
        foreach (SnapshotTest test in Manager.Repo.Tests.Values)
        {
          ButtonList.Append(new CUIButton()
          {
            Text = test.Name,
            Absolute = new CUINullRect(h: 30),
            MasterColor = new Color(64, 64, 64),
            AddMouseDown = (c, e) => Manager.Run(test.Name),
          });
        }
      });

      OnClose.Add(() =>
      {
        ButtonList.RemoveAllChildren();
        Manager.Dismantle();
      });

      BackgroundColor = new Color(32, 32, 32);

      this["layout"] = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
      };
      this["layout"]["header"] = new CUIHorizontalList()
      {
        Absolute = new CUINullRect(h: 40),
      };
      this["layout"]["header"]["runall"] = new CUIButton()
      {
        Text = "Run All",
        Flex = new CUINullVector2(1, 1),
        MasterColor = new Color(64, 0, 64),
        AddMouseDown = (c, e) => Manager.RunAll(),
      };
      this["layout"]["header"]["accept"] = new CUIButton()
      {
        Text = "Accept",
        Flex = new CUINullVector2(1, 1),
        MasterColor = new Color(64, 0, 64),
        AddMouseDown = (c, e) => Manager.AcceptCurrent()
      };
      this["layout"]["btnlist"] = ButtonList = new CUIVerticalList()
      {
        Flex = new CUINullVector2(1, 1),
        Scrollable = true,
      };
    }
  }
}