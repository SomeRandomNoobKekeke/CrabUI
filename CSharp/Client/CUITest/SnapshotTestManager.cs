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
  public class SnapshotTestManager
  {
    public static string TestCommandName = "cuitest";
    public static bool IsSnapshotTestFunc(MethodInfo mi)
      => mi.ReturnType.IsAssignableTo(typeof(CUIComponent)) && mi.GetParameters().Length == 0;

    public static string GetFullMethodName(MethodInfo mi)
    {
      List<string> parts = new List<string>() { mi.Name };
      Type declaringType = mi.DeclaringType;
      while (declaringType != null)
      {
        parts.Add(declaringType.Name);
        declaringType = declaringType.DeclaringType;
      }
      parts.Reverse();

      return string.Join('.', parts);
    }

    public Dictionary<string, SnapshotTest> Tests { get; } = new();
    public SnapshotTest CurrentTest { get; private set; }

    public CUIComponent TestBox { get; } = new CUIComponent()
    {
      Relative = new CUINullRect(0, 0, 1, 1),
      BackgroundColor = Color.Blue,
    };

    public bool IsSetup => TestBox.Parent is CUIMainComponent;

    public void Add(SnapshotTest test) => Tests[test.Name] = test;
    public void Add(Func<CUIComponent> TestFunc, string Name) => Add(new SnapshotTest(TestFunc, Name));
    public void Add(MethodInfo mi) => Add(
      (Func<CUIComponent>)Delegate.CreateDelegate(typeof(Func<CUIComponent>), mi),
      GetFullMethodName(mi)
    );

    public void Add(Type testPack)
    {
      foreach (MethodInfo mi in testPack.GetMethods(BindingFlags.Public | BindingFlags.Static))
      {
        if (IsSnapshotTestFunc(mi)) Add(mi);
      }

      foreach (Type nested in testPack.GetNestedTypes())
      {
        Add(nested);
      }
    }

    public void Run(string name)
    {
      if (!Tests.ContainsKey(name))
      {
        CUI.Logger.Warning($"Can't find snapshot test: [{name}]");
        return;
      }

      Run(Tests[name]);
    }

    public void Run(SnapshotTest test)
    {
      if (!IsSetup) Setup();

      CurrentTest = test;
      try
      {
        CUIComponent child = (CUIComponent)test.TestFunc();

        TestBox.RemoveAllChildren();
        TestBox.Append(child);
      }
      catch (Exception e)
      {
        CUI.Logger.Warning($"Error in CUISnapshotTest [{test.Name}]: {e.Message} {e.InnerException} {e.StackTrace}");
      }
    }

    public void Setup()
    {
      if (TestBox.Parent is CUIMainComponent) return;
      CUI.Main["Snapshot Testings TextBox"] = TestBox;
      CurrentTest = null;
    }

    public void Dismantle()
    {
      if (TestBox.Parent != null) TestBox.RemoveSelf();
      CurrentTest = null;
    }

    public void Init()
    {
      if (PluginCommands.Exist(TestCommandName)) return;

      PluginCommands.Add(TestCommandName, CUITest_Command, () => new string[][]{
        Tests.Keys.Append("").Append("none").ToArray()
      });

      if (ModStorage.Has("CUITest"))
      {
        Run((string)ModStorage.Get("CUITest"));
      }
    }

    public void CUITest_Command(string[] args)
    {
      if (args.Length == 0)
      {
        ModStorage.Remove("CUITest");
        Dismantle();
        return;
      }

      if (args[0].Trim() == "" || args[0] == "none")
      {
        ModStorage.Remove("CUITest");
        Dismantle();
        return;
      }

      Run(args[0]);
      ModStorage.Set("CUITest", args[0]);
    }
  }
}