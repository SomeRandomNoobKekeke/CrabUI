using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;
using System.Xml.Linq;

namespace CursedUI
{
  public class SerializationModesTest : CUISerializationTest
  {
    public class VerySpecialFrame : CUIDefault.Frame
    {
      public CUITextBlock TextBlock { get; set; }
      public VerySpecialFrame() : base("bruh")
      {
        this["layout"]["textblock"] = TextBlock = new CUITextBlock("init value");
      }
    }

    public List<UTest> Replace()
    {
      VerySpecialFrame frame = new VerySpecialFrame()
      {
        DeepSerializationMode = CUISerializationMode.Replace,
      };
      frame.TextBlock.Text = "uwu";

      VerySpecialFrame frame2 = CUIComponent.Deserialize<VerySpecialFrame>(frame.Serialize());

      return [
        new UTest(frame2.TextBlock != frame2["layout"]["textblock"], "it was replaced"),
        new UTest(frame2.Get<CUITextBlock>("layout.textblock").Text, "uwu"),
      ];
    }

    public List<UTest> Ignore()
    {
      VerySpecialFrame frame = new VerySpecialFrame()
      {
        DeepSerializationMode = CUISerializationMode.Ignore,
      };

      DebugContext.Enter("123");
      VerySpecialFrame frame2 = CUIComponent.Deserialize<VerySpecialFrame>(frame.Serialize());
      DebugContext.Exit("123");

      return [
        new UTest(frame2.TextBlock == frame2["layout"]["textblock"], "new textblock was thrown away"),
        new UTest(frame2.Get<CUITextBlock>("layout.textblock").Text, "init value", "value is the same"),
      ];
    }

    public List<UTest> Merge()
    {
      VerySpecialFrame frame = new VerySpecialFrame()
      {
        DeepSerializationMode = CUISerializationMode.Merge,
      };
      frame.TextBlock.Text = "uwu";

      VerySpecialFrame frame2 = CUIComponent.Deserialize<VerySpecialFrame>(frame.Serialize());

      return [
        new UTest(frame2.TextBlock == frame2["layout"]["textblock"],"new textblock was thrown away"),
        new UTest(frame2.Get<CUITextBlock>("layout.textblock").Text, "uwu","but props were copied"),
      ];
    }

    public List<UTest> NotSerializable()
    {
      VerySpecialFrame frame = new VerySpecialFrame()
      {
        SerializationMode = CUISerializationMode.Replace,
      };
      frame.TextBlock.Serializable = false;

      VerySpecialFrame frame2 = CUIComponent.Deserialize<VerySpecialFrame>(frame.Serialize());

      return [
        new UTest(!frame2["layout"].NamedComponents.ContainsKey("textblock"))
      ];
    }
  }
}