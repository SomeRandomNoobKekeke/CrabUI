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
using System.Text;
using System.IO;

namespace CrabUIUser
{
  public static class E2ETestManager_Extensions
  {
    public static bool IsE2ETest(Type T) => T.IsAssignableTo(typeof(IE2ETest));

    public static void Add(this E2ETestManager manager, Type testCollection)
    {
      foreach (Type test in testCollection.GetNestedTypes().Where(IsE2ETest))
      {
        manager.Add(test);
      }
    }
  }
}