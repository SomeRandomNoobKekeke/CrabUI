using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CrabUI;
using Microsoft.Xna.Framework;
using System.IO;

namespace CrabUIUser
{
  public partial class E2ETestPack
  {
    public partial class Calculator : IE2ETest
    {
      public CalculatorCore Core { get; } = new();
      public CalculatorUI UI { get; private set; }

      public void Initialize()
      {
        UI = new(Core);

        UI.Open();
      }

      public void Dispose()
      {
        UI.Close();
      }
    }
  }
}