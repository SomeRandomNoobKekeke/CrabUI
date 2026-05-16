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

using System.Text;

namespace CrabUIUser
{
  public class SnapshotGUI
  {
    public SnapshotTestManager Manager { get; private set; }

    public CUIButton OpenButton { get; private set; }
    public CUIFrame ControlPannel { get; private set; }

    public void Init()
    {
      OpenButton = new CUIButton()
      {
        Text = "Test",
        Absolute = new CUINullRect(0, 0, 30, 20),
        Anchor = CUIAnchor.RightCenter,
        AddMouseDown = (e) => Manager.Setup(),
      };

      CUI.Main["open control panel"] = OpenButton;

      ControlPannel = new CUIFrame()
      {
        Absolute = new CUINullRect(0, 0, 300, 600),
        Anchor = CUIAnchor.RightCenter,
        BackgroundColor = Color.Yellow,
        Draggable = false,
      };

      ControlPannel["layout"] = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
      };

      ControlPannel["layout"]["header"] = new CUIComponent()
      {
        Absolute = new CUINullRect(h: 40),
        BackgroundColor = new Color(0, 0, 255),
      };

      ControlPannel["layout"]["main"] = new CUIComponent()
      {
        Flex = 1,
        BackgroundColor = new Color(0, 0, 0),
      };

      ControlPannel["layout"]["main"]["testlist"] = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
      };

      ControlPannel["layout"]["header"]["accept"] = new CUIButton()
      {
        Text = "Accept",
        Relative = new CUINullRect(0, 0, 0.5f, 1),
        AddMouseDown = (e) => Manager.AcceptCurrent(),
      };
      ControlPannel["layout"]["header"]["close"] = new CUIButton()
      {
        Text = "Close",
        Relative = new CUINullRect(0.5f, 0, 0.5f, 1),
        AddMouseDown = (e) => Manager.Dismantle(),
      };

      if (Manager.IsSetup) OnSetup(Manager.TestBox);
    }

    public void AttachTo(SnapshotTestManager manager)
    {
      Manager = manager;

      manager.OnSetup += OnSetup;
      manager.OnDismantle += OnDismantle;
      manager.OnTestRunning += OnTestRunning;
      manager.OnTestPassed += OnTestPassed;
      manager.OnTestFailed += OnTestFailed;
      manager.OnAccepted += OnAccepted;
    }

    public void OnSetup(CUIComponent testBox)
    {
      OpenButton.RemoveSelf();
      testBox["overlay"]["control panel"] = ControlPannel;
      UpdateTests();
    }

    public void OnDismantle(CUIComponent testBox)
    {
      ControlPannel.RemoveSelf();
      CUI.Main["open control panel"] = OpenButton;
    }

    public void OnTestRunning(SnapshotTest currentTest)
    {

    }

    public void OnTestPassed(SnapshotTest currentTest, ComponentSnapshot currentSnapshot)
    {
      ControlPannel["layout"]["main"]["testlist"].Children.Select(c => c as CUIButton)
        .First(btn => btn.Text == currentTest.Name)
        .MasterColor = Color.Lime;
    }

    public void OnTestFailed(SnapshotTest currentTest, ComponentSnapshot actual, ComponentSnapshot stored)
    {
      ControlPannel["layout"]["main"]["testlist"].Children.Select(c => c as CUIButton)
        .First(btn => btn.Text == currentTest.Name)
        .MasterColor = Color.Red;
    }
    public void OnAccepted(SnapshotTest currentTest, ComponentSnapshot currentSnapshot)
    {
      ControlPannel["layout"]["main"]["testlist"].Children.Select(c => c as CUIButton)
        .First(btn => btn.Text == currentTest.Name)
        .MasterColor = Color.Lime;
    }

    private void UpdateTests()
    {
      ControlPannel["layout"]["main"]["testlist"].RemoveAllChildren();
      foreach (var (key, test) in Manager.Tests)
      {
        ControlPannel["layout"]["main"]["testlist"].Append(new CUIButton()
        {
          Text = key,
          Absolute = new CUINullRect(h: 30),
          AddMouseDown = (e) => Manager.Run(key),
          MasterColor = Color.Cyan,
        });
      }
    }



  }
}