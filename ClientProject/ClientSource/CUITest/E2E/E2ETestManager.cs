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
  public partial class E2ETestManager
  {
    public ILogger Logger => CUI.Logger;


    public IE2ETest CurrentTest { get; set; }

    public E2ETestManager()
    {
      UI = new(this);
    }

    public E2ETestRepo Repo { get; } = new();
    public E2ETestManagerUI UI { get; }

    public ClearableEvent<Event> Events { get; } = new();

    public void CleanUp()
    {
      if (CurrentTest != null)
      {
        CurrentTest.Dispose();
        CurrentTest = null;
      }

      CUI.Main.Children.Clear();
    }

    public void RunAll()
    {
      foreach (string name in Repo.Tests.Keys)
      {
        Run(name);
      }
    }
    public void Run(string name)
    {
      if (!Repo.Tests.ContainsKey(name))
      {
        Logger.Warning($"Can't find E2E test: [{name}]");
        return;
      }

      ModStorage.Set("CUITest", name);
      Run(Repo.Tests[name]);
    }

    public void Run(Type testType)
    {
      try
      {
        CleanUp();

        CurrentTest = (IE2ETest)Activator.CreateInstance(testType);
        CurrentTest.Initialize();
      }
      catch (Exception e)
      {
        Logger.Warning($"Error in CUIE2ETest [{testType}]: {e.Message} {e.InnerException}\n{e.StackTrace}");
      }
    }
  }
}