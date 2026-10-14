using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CUILibs
{
  public partial class UTest
  {
    public static bool CollapseTestPackIfSucceed
    {
      get => UTestLogger.CollapseTestPackIfSucceed;
      set => UTestLogger.CollapseTestPackIfSucceed = value;
    }

    public static void Init()
    {
      UTestExplorer.TestTree = new UTestTree(Assembly.GetExecutingAssembly());
      UTestCommands.AddCommands();
    }

    public static void Dispose()
    {
      UTestCommands.RemoveCommands();
      UTestExplorer.Clear();
    }
  }
}