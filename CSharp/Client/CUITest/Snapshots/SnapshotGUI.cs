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

    public CUIFrame ControlPannel { get; private set; }

    public void Init()
    {
      ControlPannel ??= new CUIFrame()
      {
        Absolute = new CUINullRect(0, 0, 300, 600),
        Anchor = CUIAnchor.RightCenter,
        BackgroundColor = new Color(32, 0, 64),
        Draggable = false,
      };
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
      Manager.TestBox["control pannel"] = ControlPannel;
    }

    public void OnDismantle(CUIComponent testBox)
    {
      testBox.RemoveChild(ControlPannel);
    }

    public void OnTestRunning(SnapshotTest currentTest)
    {

    }

    public void OnTestPassed(SnapshotTest currentTest, ComponentSnapshot currentSnapshot)
    {

    }

    public void OnTestFailed(SnapshotTest currentTest, ComponentSnapshot actual, ComponentSnapshot stored)
    {

    }
    public void OnAccepted(SnapshotTest currentTest, ComponentSnapshot currentSnapshot)
    {

    }



  }
}