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
using System.IO;
using System.Text.Json;

namespace CrabUIUser
{
  public partial class E2ETestPack
  {
    public partial class SimpleSettings : IE2ETest
    {
      public class NestedSettings
      {
        public string StringProp { get; set; } = "hurb";
        public int IntProp { get; set; } = 321;
      }

      public class Settings
      {
        public NestedSettings Nested { get; set; } = new();
        public string StringProp { get; set; } = "bruh";
        public int IntProp { get; set; } = 123;
      }

    }
  }
}