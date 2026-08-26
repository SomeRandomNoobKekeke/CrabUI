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
  public partial class SnapshotTestManager
  {
    public record Event(string Name, SnapshotTest Test, string Message = "");
  }
}