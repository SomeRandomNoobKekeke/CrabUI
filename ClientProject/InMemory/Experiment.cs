using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using System.Xml.Linq;
using Barotrauma.LuaCs;
using CUICodeGenerator;
using Microsoft.Xna.Framework;

namespace CursedUIUser
{
  public partial class Mod : IAssemblyPlugin
  {
    public void Experiment()
    {
      // Logger.LogVars(typeof(string).IsValueType, typeof(string).IsPrimitive);

      // new OverridingVirtualProps().Run();
    }
  }
}