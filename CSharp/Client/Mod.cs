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

namespace CrabUIUser
{
  public partial class Mod : IAssemblyPlugin
  {
    public static Mod Instance;

    public static Logger Logger { get; set; } = new();

    public void Initialize()
    {
      Instance = this;
      Logger.Log($"Compiled somehow");
      UTestCommands.AddCommands();




      try
      {
        Init();
        Experiment();
      }
      catch (Exception e) { Logger.Error(e); }


    }

    public void Init()
    {
      CUI.Start();

      // CUIComponent component1 = new();
      // CUIComponent component2 = new();
      // component1.AddChild(component2);

      // CUIMainComponent mainComponent = new CUIMainComponent();

      // component1.MainComponentTracker.OnAttachedTo(mainComponent);

      // Logger.Default.Log(component2.MainComponentTracker.MainComponent);
    }

    public void OnLoadCompleted() { }
    public void PreInitPatching() { }

    public void Dispose()
    {
      Instance = null;

    }
  }
}