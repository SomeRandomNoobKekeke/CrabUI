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
  public class CUITestRunner
  {
    public void Use(CUIComponent root)
    {
      CUI.Main.RemoveAllChildren();
      CUI.Main.AddChild(root);
    }

    public void CUITest_Command(string[] args)
    {
      if (args.Length != 1) return;

      Dictionary<string, MethodInfo> factories = CUIFactories.AllFactoryMethods()
        .ToDictionary(mi => mi.Name, mi => mi);

      if (!factories.ContainsKey(args[0])) return;

      Use((CUIComponent)factories[args[0]].Invoke(null, new object[] { }));
    }
  }
}