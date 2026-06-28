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

namespace CrabUIUser
{
  public partial class E2ETestPack
  {
    public partial class SimpleSettings : IE2ETest
    {
      public MicroSettingsManager Manager { get; } = new();
      public SettingsUI UI { get; set; }
      public Settings ModSettings => Manager.Settings;

      public void Initialize()
      {
        UI = new SettingsUI(Manager);
        UI.Open();
        UI.Refresh();
      }

      public void Dispose()
      {
        UI.Close();
      }
    }
  }
}