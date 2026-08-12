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
  public partial class SnapshotTestManager : CUIPage
  {
    public CUIVerticalList ButtonList { get; set; }

    public void UpdateTests()
    {
      Children.Clear();
      Dismantle();
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



    public void OpenGroup(string name)
    {
      ButtonList.Children.Clear();
      foreach (SnapshotTest test in Repo.GroupedTests[name].Values)
      {
        ButtonList.Add(new CUIButton()
        {
          Text = test.Name,
          TextAnchor = CUIAnchor.LeftCenter,
          MasterColor = new Color(64, 64, 64),
          OnMouseDown = (e) => Run(test.Name),
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
        OnMouseDown = (e) => RunAll(),
      };
      this["layout"]["controls"]["accept"] = new CUIButton()
      {
        Text = "Accept",
        Flex = 1,
        MasterColor = new Color(64, 0, 64),
        OnMouseDown = (e) => AcceptCurrent()
      };
      this["layout"]["controls"]["serialize"] = new CUIToggleButton()
      {
        Text = "Serialize",
        Flex = 1,
        MasterColor = new Color(255, 0, 255),

        State = SerializeTestSubject,
        OnToggle = (state) => SerializeTestSubject = state,
      };

      this["layout"]["groups"] = new CUIHorizontalList() { FitContent = new CUIBool2(false, true), };
      foreach (string group in Repo.Groups)
      {
        this["layout"]["groups"].Children.Add(new CUIButton(group)
        {
          OnMouseDown = (e) => OpenGroup(group),
        });
      }

      this["layout"]["btnlist"] = ButtonList = new CUIVerticalList()
      {
        Flex = 1,
        Scrollable = true,
      };

      OpenGroup(Repo.Groups.First());
    }
  }
}