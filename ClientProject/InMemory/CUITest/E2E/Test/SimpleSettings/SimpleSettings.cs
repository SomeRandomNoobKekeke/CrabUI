using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;
using System.IO;

namespace CursedUIUser
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
        UI = new SettingsUI(Manager)
        {
          Absolute = new CUINullRect(w: 400, h: 600),
        };
        UI.Open();
      }

      public void Dispose()
      {
        UI.Close();
      }
    }
  }
}