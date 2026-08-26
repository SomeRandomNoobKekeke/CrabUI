using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;
using System.Xml.Linq;

namespace CursedUI
{
  public class SerializationPersistenceTest : CUISerializationTest
  {
    public UTest Default()
    {
      CUIFrame frame = new CUIDefault.Frame("bruh");

      CUI.Logger.Log(frame.GetType().GetFullName());

      XElement element = frame.Serialize();
      CUI.Logger.Log(element);

      return new UTest(frame.IsEqualTo(CUIComponent.Deserialize(element)), true);
    }
  }
}