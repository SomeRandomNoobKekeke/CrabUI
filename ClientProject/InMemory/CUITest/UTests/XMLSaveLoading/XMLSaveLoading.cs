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

      CUIComponent frame = new CUIDefault.Frame("bruh");
      frame.SaveTo(savePath);

      CUIComponent frame2 = CUIComponent.LoadFrom(savePath);

      return new UTest(frame.IsEqualTo(frame2), true);
    }
  }
}