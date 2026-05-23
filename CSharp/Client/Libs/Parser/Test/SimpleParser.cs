using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.IO;

namespace BaroJunk
{
  public class SimpleParserTest : UTestPack
  {
    public record FunnyObject(string Value)
    {
      public static FunnyObject Parse(string raw) => new FunnyObject(raw);
      public static string Serialize(FunnyObject o) => o.ToString();

      public override string ToString() => $"Funny {Value}";
    }

    public override void CreateTests()
    {
      SimpleParser parser = new SimpleParser();

      parser.OnError.Add((s) => Logger.Default.Warning(s));

      Tests.Add(new UTest(parser.Parse<int>("123"), 123));
      Tests.Add(new UTest(parser.Parse<string>("123"), "123"));
      Tests.Add(new UTest(parser.Parse<bool>("true"), true));

      FunnyObject funny = new("123");
      Tests.Add(new UTest(parser.Serialize(funny), "Funny 123"));

      Tests.Add(new UTest(parser.Parse<FunnyObject>("123"), new FunnyObject("123")));
      Tests.Add(new UTest(parser.Serialize(123.1f), "123.1"));

      Tests.Add(new UTest(parser.Parse<BindingFlags>("Instance"), BindingFlags.Instance));

      Tests.Add(new UTest(parser.Serialize(new Vector2(3, 5)), "[3,5]"));
      Tests.Add(new UTest(parser.Parse<Vector2>("[3,5]"), new Vector2(3, 5)));
    }
  }
}
