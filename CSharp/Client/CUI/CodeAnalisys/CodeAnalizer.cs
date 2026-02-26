using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using System.IO;

namespace CrabUI
{

  public class CodeAnalizer
  {
    // public string OutputDir => Path.Combine(ModInfo.ModDir<Mod>(), "Ignore", "Trash");

    public void Analyze(string @namespace)
    {
      foreach (Type T in Assembly.GetExecutingAssembly().GetTypes())
      {
        if (T.Namespace != @namespace) continue;
        if (!T.IsAssignableTo(typeof(IModule))) continue;

        Logger.Default.Log(T);
      }
    }
  }
}