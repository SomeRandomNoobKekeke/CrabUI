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
  public class CUITest
  {
    public static Logger Logger { get; } = new()
    {
      PrintFilePath = false
    };


    public string TestDataFolder => Path.Combine(ModInfo.Dir, "Test Data");
    public string SnaphotsFolder => Path.Combine(TestDataFolder, "Snapshots");
    public string SnaphotsTempFolder => Path.Combine(TestDataFolder, "Temp");

    public VirtualFileSystem VirtualFileSystem { get; } = new();

    public void Init()
    {

    }

    public void Dispose()
    {

    }
  }
}