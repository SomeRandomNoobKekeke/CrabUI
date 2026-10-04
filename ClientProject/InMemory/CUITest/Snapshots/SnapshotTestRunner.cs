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
using System.Xml.Linq;

namespace CursedUIUser
{
  public class SnapshotTestRunner
  {
    public ILogger Logger => CUI.Logger;

    public bool SerializeTestSubject { get; set; } = false;
    // public bool PrintTestSubject { get; set; } = true;
    public SnapshotTestChamber Chamber { get; set; }


    public ComponentSnapshot Run(SnapshotTest test)
    {
      try
      {
        CUIVisualComponent TestSubject = test.TestFunc();
        TestSubject.DeepDebug = true;


        if (SerializeTestSubject)
        {
          if (CUISerializationCompare.Instance.IsOpen)
          {
            CUISerializationCompare.Instance.ComponentBefore = TestSubject;
          }

          XElement XMLBefore = TestSubject.Serialize();

          CUI.Logger.Log($"=========>> Before serialization: <<=========\n{XMLBefore}\n");
          new XDocument(XMLBefore).Save(Path.Combine(CUITest.CompareFolder, "Before.xml"));

          TestSubject = CUIVisualComponent.Deserialize(TestSubject.Serialize());
          TestSubject.DeepDebug = true;
          XElement XMLAfter = TestSubject.Serialize();


          if (CUISerializationCompare.Instance.IsOpen)
          {
            CUISerializationCompare.Instance.ComponentAfter = TestSubject;
          }

          new XDocument(XMLAfter).Save(Path.Combine(CUITest.CompareFolder, "After.xml"));
          if (XMLBefore.ToString() != XMLAfter.ToString())
          {
            CUI.Logger.Print($"=========>> XML Before doesn't match XML After <<=========", Color.Orange);
            CUI.Logger.Log($"=========>> After serialization: <<=========\n{XMLAfter}\n");
          }
          else
          {
            CUI.Logger.Print($"=========>> XML Before and After serialization matches <<=========", Color.Lime);
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