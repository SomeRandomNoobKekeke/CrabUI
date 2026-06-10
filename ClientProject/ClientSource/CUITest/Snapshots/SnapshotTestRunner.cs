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
  public class SnapshotTestRunner
  {
    public ILogger Logger => CUI.Logger;

    public SnapshotTestChamber Chamber { get; set; }

    public ComponentSnapshot Run(SnapshotTest test)
    {
      try
      {
        CUIComponent TestSubject = (CUIComponent)test.TestFunc();
        TestSubject.DeepDebug = true;

        Chamber.RemoveAllChildren();
        Chamber.Append(TestSubject);

        CUI.Main.Step();

        return ComponentSnapshot.Take(TestSubject, test.Name);
      }
      catch (Exception e)
      {
        Logger.Warning($"Error in CUISnapshotTest [{test.Name}]: {e.Message} {e.InnerException}\n{e.StackTrace}");
      }

      return null;
    }
  }
}