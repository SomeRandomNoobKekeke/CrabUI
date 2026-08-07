using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;
using System.Xml.Linq;

namespace CrabUI
{
  public class SerializationModesTest : CUISerializationTest
  {
    public class VerySpecialFrame : CUIDefault.Frame
    {
      public CUITextBlock TextBlock { get; set; }
      public VerySpecialFrame() : base("bruh")
      {
        this["layout"]["textblock"] = TextBlock = new CUITextBlock("lul");
      }
    }

    public UTest Replace()
    {
      VerySpecialFrame frame = new VerySpecialFrame()
      {
        SerializationMode = CUISerializationMode.Replace,
      };
      frame.TextBlock.Text = "uwu";

      VerySpecialFrame frame2 = CUIComponent.Deserialize<VerySpecialFrame>(frame.Serialize());

      return new UTest(
        frame2.TextBlock != frame2["layout"]["textblock"] &&
        frame2.Get<CUITextBlock>("layout.textblock").Text == "uwu",
        true
      );
    }

    public UTest Ignore()
    {
      VerySpecialFrame frame = new VerySpecialFrame()
      {
        SerializationMode = CUISerializationMode.Ignore,
      };

      VerySpecialFrame frame2 = CUIComponent.Deserialize<VerySpecialFrame>(frame.Serialize());

      return new UTest(frame2.TextBlock, frame2["layout"]["textblock"]);
    }

    public UTest Merge()
    {
      VerySpecialFrame frame = new VerySpecialFrame()
      {
        SerializationMode = CUISerializationMode.Merge,
      };
      frame.TextBlock.Text = "uwu";

      VerySpecialFrame frame2 = CUIComponent.Deserialize<VerySpecialFrame>(frame.Serialize());

      return new UTest(
        frame2.TextBlock == frame2["layout"]["textblock"] &&
        frame2.TextBlock.Text == "uwu",
        true
      );
    }

    public UTest NotSerializable()
    {
      VerySpecialFrame frame = new VerySpecialFrame()
      {
        SerializationMode = CUISerializationMode.Replace,
      };
      frame.TextBlock.Serializable = false;

      VerySpecialFrame frame2 = CUIComponent.Deserialize<VerySpecialFrame>(frame.Serialize());

      return new UTest(
        frame2["layout"].NamedComponents.ContainsKey("textblock"),
        false
      );
    }
  }
}