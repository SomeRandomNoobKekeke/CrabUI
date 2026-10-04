using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;
using System.Xml.Linq;

namespace CursedUI
{
  public class SerializationMatchTest : CUISerializationTest
  {

    public UTest Default()
    {
      CUIDefault.Frame frame1 = new CUIDefault.Frame("guh");
      CUIDefault.Frame frame2 = CUIComponent.Deserialize<CUIDefault.Frame>(frame1.Serialize());

      // frame2.Caption = "not guh";

      return new UTest(frame1.IsDeepEqualTo(frame2));
    }
  }
}