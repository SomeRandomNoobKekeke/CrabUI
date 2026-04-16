using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;

namespace CrabUIUser
{
  public partial class Mod : IAssemblyPlugin
  {
    public event Action bruh;
    public void Experiment()
    {
      // new HowToAccessProtectedInGrandParent().Run();
    }
  }
}