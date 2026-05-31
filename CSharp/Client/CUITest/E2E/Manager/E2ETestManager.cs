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
using System.IO;

namespace CrabUIUser
{
  public partial class E2ETestManager
  {
    public ILogger Logger { get; set; } = CUITest.Logger;

    public Dictionary<string, Type> Tests { get; } = new();


    public IE2ETest Current { get; private set; }
    public bool IsRunning => Current != null;

    public IEnumerable<string> TestNames => Tests.Keys;
    public void Add(Type test) => Tests[test.Name] = test;

    public void Run(string name) => Run(Tests[name]);
    public void Run(Type testType)
    {
      IE2ETest test = (IE2ETest)Activator.CreateInstance(testType);

      Current = test;

      try
      {
        test.Initialize();
      }
      catch (Exception e)
      {
        Logger.Error($"Error in E2E test [{testType.Name}]:{e}:{e.InnerException}");
      }
    }

    public void Stop()
    {
      if (Current != null)
      {
        Current.Dispose();
        Current = null;
      }
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
      Stop();
    }
  }
}