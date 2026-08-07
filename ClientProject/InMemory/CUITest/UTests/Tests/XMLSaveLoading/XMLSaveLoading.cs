using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;
using CrabUI;
using System.IO;

namespace CrabUIUser
{
  public class XMLSaveLoadingTest : UTestPack
  {
    public UTest SimpleSaveLoading()
    {
      string savePath = Path.Combine("Test Data", "bruh.xml");

      CUIFrame frame = new CUIDefault.Frame("bruh");
      frame.SaveTo(savePath);

      CUIFrame frame2 = CUIVisualComponent.LoadFrom<CUIFrame>(savePath);

      return new UTest(frame.IsEqualTo(frame2), true);
    }
  }
}