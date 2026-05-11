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
  public class CUITest
  {
    public CUITestRunner CUITestRunner { get; } = new();

    public CUITest()
    {
      PluginCommands.Add("cuitest", CUITestRunner.CUITest_Command, () => new string[][]{
        CUIFactories.AllFactoryMethods().Select(mi=>mi.Name).ToArray()
      });
    }
  }
}