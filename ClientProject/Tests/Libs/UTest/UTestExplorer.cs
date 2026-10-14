using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CUILibs
{
  [System.AttributeUsage(System.AttributeTargets.All, Inherited = true, AllowMultiple = true)]
  public class UTestCategory : System.Attribute
  {
    public string Category { get; set; }
    public UTestCategory(string category = null) => Category = category;
  }

  [System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
  public class UTestSubPackOf : System.Attribute
  {
    public Type SubPackOf { get; set; }
    public UTestSubPackOf(Type parent = null) => SubPackOf = parent;
  }


  public static class UTestExplorer
  {

    public static UTestTree TestTree { get; set; }

    public static HashSet<string> Categories = new();
    public static void ScanCategories(params string[] categories)
      => Categories = new HashSet<string>(categories);
    public static void ScanCategory(string category)
      => Categories.Add(category);

    public static IEnumerable<string> TestNames => TestTree.TypeByPath.Keys;
    public static Dictionary<string, Type> TestByName => TestTree.TypeByPath;

    public static void PrintAllTests()
    {
      UTestLogger.Log($"----------- Available UTests: -----------");
      UTestLogger.Log(TestTree);
    }

    public static void Clear()
    {
      TestTree = null;
      Categories = null;
    }
  }
}