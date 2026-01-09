using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text;

namespace BaroJunk
{
  public class UTestPack
  {
    public enum UTestPackState { AllPassed, SomePassed, AllFailed, Error }

    public static bool IsTestGenerator(MethodInfo mi)
      => mi.ReturnType.IsAssignableTo(typeof(UTest)) && mi.GetParameters().Length == 0;

    public static bool IsTestListGenerator(MethodInfo mi)
      => mi.ReturnType.IsAssignableTo(typeof(IEnumerable<UTest>)) && mi.GetParameters().Length == 0;
    public static bool IsTestCreator(MethodInfo mi)
      => mi.Name.StartsWith("Create") && mi.Name != "CreateTests";

    public static string GetNameFromMethodInfo(MethodInfo mi)
      => $"{mi.DeclaringType.Name}.{mi.Name}";



    public List<UTest> Tests = new();
    public int PassedCount => Tests.Count(test => test.Passed);
    public UTestPackState State
    {
      get
      {
        if (Error is not null) return UTestPackState.Error;
        int passed = PassedCount;
        if (Tests.Count == 0) return UTestPackState.AllPassed;
        if (passed == Tests.Count) return UTestPackState.AllPassed;
        if (passed == 0) return UTestPackState.AllFailed;
        return UTestPackState.SomePassed;
      }
    }

    /// <summary>
    /// Set in test runner
    /// </summary>
    public Exception Error { get; set; }
    public bool NotEmpty => Error != null || Tests.Count > 0;

    public UTest AddTest(UTest test)
    {
      Tests.Add(test);
      return test;
    }

    public virtual void CreateTests()
    {
      foreach (MethodInfo mi in this.GetType().GetMethods())
      {
        if (mi.DeclaringType == this.GetType())
        {
          if (IsTestGenerator(mi))
          {
            UTest test = mi.Invoke(mi.IsStatic ? null : this, null) as UTest;
            test.Name = $"{mi.Name} | {test.Name}";
            Tests.Add(test);
            continue;
          }

          if (IsTestListGenerator(mi))
          {
            IEnumerable<UTest> list = mi.Invoke(mi.IsStatic ? null : this, null) as IEnumerable<UTest>;
            foreach (UTest test in list)
            {
              test.Name = $"{mi.Name} | {test.Name}";
            }
            Tests.AddRange(list);
            continue;
          }

          if (IsTestCreator(mi))
          {
            mi.Invoke(mi.IsStatic ? null : this, null);
            continue;
          }
        }
      }
    }

    public override string ToString()
    {
      if (Error is not null) return Error.ToString();

      StringBuilder sb = new StringBuilder();

      sb.Append($"UTestPack {this.GetType()} [{PassedCount}/{Tests.Count}]:\n");
      foreach (UTest test in Tests)
      {
        sb.Append(test.ToString());
        sb.Append('\n');
      }
      return sb.ToString();
    }

    public void Log() { if (NotEmpty) UTestLogger.LogPack(this); }
  }

  public static class UTestPackExtentions
  {
    public static void Log(this List<UTestPack> packs) => packs.ForEach(pack => pack.Log());
  }
}