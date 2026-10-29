using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;

namespace CUILibs
{
  public partial class UTest
  {
    static UTest()
    {
      PluginLifeCycle.Stop += Dispose;
    }

    public static bool CollapseTestPackIfSucceed
    {
      get => UTestLogger.CollapseTestPackIfSucceed;
      set => UTestLogger.CollapseTestPackIfSucceed = value;
    }

    public static void ScanThisAssembly()
    {
      UTestExplorer.TestTree.ScanAssembly(Assembly.GetCallingAssembly());
      UTestCommands.AddCommands();
    }

    public static void TryRunPrevTest()
    {
#if CLIENT
      string lastCommand = ModStorage.Get<string>("lastUtestCommand");
      if (!string.IsNullOrEmpty(lastCommand))
      {
        DebugConsole.ExecuteCommand(lastCommand);
      }
#endif
    }

    public static void Dispose()
    {
      UTestCommands.RemoveCommands();
      UTestExplorer.Clear();
    }
  }
}