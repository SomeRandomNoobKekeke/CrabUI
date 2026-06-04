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
    public class SimpleSettings : IE2ETest
    {
      public void Initialize()
      {
        CUI.Logger.Log(123);
      }

      public void Dispose()
      {

      }
    }
  }
}