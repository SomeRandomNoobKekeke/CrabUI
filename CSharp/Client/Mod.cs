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

      UTestCommands.AddCommands();



      try
      {
        CUI.Instance.Connect();

        Init();
      }
      catch (Exception e)
      {
        Logger.Error(e);
      }
      Experiment();
      Logger.Log($"Compiled somehow");
    }

    public void Init()
    {
      CUIComponent component = new CUIComponent();
      component.BackgroundColor = new Color(255, 0, 0);


      component.MouseDown += (e) =>
      {
        component.BackgroundColor = component.BackgroundColor == Color.Green ?
          Color.Red : Color.Green;
        component.Absolute = new Rectangle(
          component.Absolute.Value.X + 10,
          component.Absolute.Value.Y,
          component.Absolute.Value.Width,
          component.Absolute.Value.Height
        );
      };
      component.Absolute = new Rectangle(300, 100, 200, 200);


      CUI.Instance.Main.AddChild(component);
    }

    public void OnLoadCompleted() { }
    public void PreInitPatching() { }

    public void Dispose()
    {
      Instance = null;
    }
  }
}