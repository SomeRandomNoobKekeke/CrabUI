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
  public class SnapshotConsoleInterface
  {
    public SnapshotTestManager Manager { get; private set; }

    public void Init()
    {
      if (!PluginCommands.Exist("cuitest_accept"))
      {
        PluginCommands.Add("cuitest_accept", (args) => Manager.AcceptCurrent());
      }

      PluginCommands.Add("cuitest", CUITest_Command, () => new string[][]{
        Manager.Tests.Keys.Append("").Append("none").ToArray()
      });
    }

    public void CUITest_Command(string[] args)
    {
      if (args.Length == 0)
      {
        ModStorage.Remove("CUITest");
        Manager.Dismantle();
        return;
      }

      if (args[0].Trim() == "" || args[0] == "none")
      {
        ModStorage.Remove("CUITest");
        Manager.Dismantle();
        return;
      }

      Manager.Run(args[0]);
      ModStorage.Set("CUITest", args[0]);
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
      CUI.Logger.Log($"Snapshot Test Box Ready");
    }

    public void OnDismantle(CUIComponent testBox)
    {
      CUI.Logger.Log($"Snapshot Test Box Dismantled");
    }

    public void OnTestRunning(SnapshotTest currentTest)
    {
      CUI.Logger.Log($"Running test [{Logger.WrapInColor(currentTest.Name, "white")}]");
    }

    public void OnTestPassed(SnapshotTest currentTest, ComponentSnapshot currentSnapshot)
    {
      CUI.Logger.Log($"{Logger.WrapInColor("Snapshot Test Passed", "lime")}");
    }

    public void OnTestFailed(SnapshotTest currentTest, ComponentSnapshot actual, ComponentSnapshot stored)
    {
      CUI.Logger.Log($"{Logger.WrapInColor("Snapshot Test Failed", "red")}");
      CUI.Logger.Log(CreateDiffString(actual.ToString(), stored.ToString()));
      // DebugConsole.NewMessage(CreateDiffString(actual.ToString(), stored.ToString()));
    }
    public void OnAccepted(SnapshotTest currentTest, ComponentSnapshot currentSnapshot)
    {
      CUI.Logger.Log($"Snapshot Accepted");
    }


    //$"‖color:{color}‖{msg}‖end‖"
    public static string CreateDiffString(string a, string b)
    {
      StringBuilder sb = new StringBuilder();

      Color cl = Color.White;

      void EnterColor(Color newCl)
      {
        if (newCl == cl) return;
        cl = newCl;

        sb.Append($"‖color:{cl.R},{cl.G},{cl.B}‖");
      }

      void ExitColor()
      {
        if (Color.White == cl) return;
        cl = Color.White;

        sb.Append($"‖end‖");
      }

      void ChangeColor(Color newCl)
      {
        ExitColor();
        EnterColor(newCl);
      }

      EnterColor(Color.Gray);

      int minLength = Math.Min(a.Length, b.Length);

      for (int i = 0; i < minLength; i++)
      {
        if (a[i] != b[i])
        {
          ChangeColor(Color.Cyan);
        }
        else
        {
          ChangeColor(Color.Gray);
        }

        sb.Append(a[i]);
      }

      ExitColor();

      return sb.ToString();
    }

  }
}