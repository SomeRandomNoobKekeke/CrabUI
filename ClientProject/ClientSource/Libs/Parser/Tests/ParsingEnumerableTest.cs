using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;
using CursedUI;


namespace CUILibs
{
  public partial class ParserTest : UTestPack
  {
    public class ParsingEnumerableTest : ParserTest
    {


      public List<UTest> Serialize()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UTest(parser.Serialize(new int[]{123, 321}), "[123,321]"),
        };
      }

      public List<UTest> Parse()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UListTest(parser.ParseEnumerable<int>("[123,321]"), new int[]{123, 321}),
          new UListTest(parser.ParseEnumerable<string>("[123,321]"), new string[]{"123", "321"}),
          new UListTest(parser.ParseEnumerable<string>("['123 321']"), new string[]{"123 321"}),
          new UListTest(parser.ParseEnumerable<string>("['123, 321',qwe,'asd']"), new string[]{"123, 321", "qwe","asd"}),
        };
      }

      public List<UTest> ParseInvalid()
      {
        Parser parser = CreateTestParser();

        return new List<UTest>()
        {
          new UListTest(parser.ParseEnumerable<int>(null), new int[]{}),
          new UListTest(parser.ParseEnumerable<int>(""), new int[]{}),
          new UListTest(parser.ParseEnumerable<int>("[]"), new int[]{ 0 }),
          new UListTest(parser.ParseEnumerable<int>("["), new int[]{  }),
          new UListTest(parser.ParseEnumerable<int>("[,,"), new int[]{ 0,0, }),
          new UListTest(parser.ParseEnumerable<int>("[',','"), new int[]{ 0,}),
          new UListTest(parser.ParseEnumerable<int>("1[2,3,4"), new int[]{ 2,3 }),
          new UListTest(parser.ParseEnumerable<int>("1[2,3,4]5"), new int[]{ 2,3,4 }),
          new UListTest(parser.ParseEnumerable<int>(",,,"), new int[]{ }),
          new UListTest(parser.ParseEnumerable<int>("[123,qwe]"), new int[]{123, 0}),
        };
      }
    }
  }

}