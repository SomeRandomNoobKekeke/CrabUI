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
using System.Xml.Linq;

namespace CrabUIUser
{
  public class SnapshotTestRunner
  {
    public ILogger Logger => CUI.Logger;

    public bool SerializeTestSubject { get; set; } = true;
    // public bool PrintTestSubject { get; set; } = true;
    public SnapshotTestChamber Chamber { get; set; }

    public ComponentSnapshot Run(SnapshotTest test)
    {
      try
      {
        CUIComponent TestSubject = (CUIComponent)test.TestFunc();
        TestSubject.DeepDebug = true;


        if (SerializeTestSubject)
        {
          XElement XMLBefore = TestSubject.Serialize();
          TestSubject = CUIComponent.Deserialize(TestSubject.Serialize());
          XElement XMLAfter = TestSubject.Serialize();

          if (XMLBefore.ToString() != XMLAfter.ToString())
          {
            CUI.Logger.Log($"=========>> Before serialization: <<=========\n{XMLBefore}\n");
            CUI.Logger.Log($"=========>> After serialization: <<=========\n{XMLAfter}\n");

            new XDocument(XMLBefore).Save(Path.Combine(CUITest.CompareFolder, "Before.xml"));
            new XDocument(XMLAfter).Save(Path.Combine(CUITest.CompareFolder, "After.xml"));
          }
          else
          {
            CUI.Logger.Log($"=========>> XML Before and After serialization matches <<=========\n");
          }
        }

        Chamber.Children.Clear();
        Chamber.Children.Add(TestSubject);

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