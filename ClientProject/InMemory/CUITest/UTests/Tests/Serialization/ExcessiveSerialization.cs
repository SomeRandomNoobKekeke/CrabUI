using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;

namespace CursedUI
{
  public class ExcessiveSerialization : CUISerializationTest
  {
    public UTest Empty() => new UTest(new CUIComponent().Serialize().ToString(), "<CUIComponent />");
    public UTest OneProp() => new UTest(new CUIComponent()
    {
      Absolute = new CUINullRect(0, 0, null, 100),
    }.Serialize().ToString(), "<CUIComponent Absolute=\"[0,0,,100]\" />");

    public UTest DeepProp() => new UTest(new CUIComponent()
    {
      Background = { Color = Color.Red },
    }.Serialize().ToString(), "<CUIComponent Background.Color=\"255,0,0,255\" />");
  }
}