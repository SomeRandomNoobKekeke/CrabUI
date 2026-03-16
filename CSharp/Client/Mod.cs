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

      CUIComponent component = new();
      CUIMainComponent mainComponent = new CUIMainComponent();

      CUI.Logger.Log(component.Tree.Name);
      CUI.Logger.Log((mainComponent as CUIComponent).Tree.Name);
      CUI.Logger.Log(mainComponent.Tree.Name);

      foreach (PropertyInfo pi in typeof(CUIMainComponent).GetProperties())
      {
        CUI.Logger.Log($"{pi} {pi.PropertyType}");
      }
    }

    public void OnLoadCompleted() { }
    public void PreInitPatching() { }

    public void Dispose()
    {
      Instance = null;
    }
  }
}