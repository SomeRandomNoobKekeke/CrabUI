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

      CUI.Instance.Connect();

      CUIComponent component = new CUIComponent()
      {
        Rect = new Rectangle(100, 100, 200, 200),
      };

      component.Drawer.Color = Color.Cyan;

      CUI.Instance.Main.Children.Add(component);

      Experiment();
    }

    public void OnLoadCompleted() { }
    public void PreInitPatching() { }

    public void Dispose()
    {
      Instance = null;
    }
  }
}