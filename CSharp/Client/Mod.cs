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

    public CodeAnalizer CodeAnalizer { get; } = new();

    public void Initialize()
    {
      Instance = this;

      UTestCommands.AddCommands();
      CodeAnalizer.Analyze("CrabUI");


      try
      {
        Init();
        Experiment();
      }
      catch (Exception e)
      {
        Logger.Error(e);
      }

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
      };
      component.Absolute = new CUINullRect(300, 100, 200, 200);

      component.AddChild(new CUIComponent()
      {
        Relative = new CUINullRect(0.1f, 0.1f, 0.8f, 0.8f),
        BackgroundColor = Color.White,
        OnMouseDown = (e) => e.Consumed = true,
      });



      CUI.Main.AddChild(component);
    }

    public void OnLoadCompleted() { }
    public void PreInitPatching() { }

    public void Dispose()
    {
      Instance = null;
    }
  }
}