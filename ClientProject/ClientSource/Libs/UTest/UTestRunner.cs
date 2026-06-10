using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text;

namespace BaroJunk
{

  public class UTestNotFoundException : System.Exception
  {
    public string TestName;
    public UTestNotFoundException(string testname) : base($"[{testname}] Not Found")
    {
      TestName = testname;
    }
    public override string ToString() => $"[{TestName}] Not Found";
  }


  public class UTestRunException : System.Exception
  {
    public Type TestType;
    public UTestRunException(Type testType, System.Exception inner) : base($"[{testType.Name}] test pack failed, [{inner?.Message}]", inner)
    {
      TestType = testType;
    }
    public override string ToString() => $"[{TestType.Name}] test pack failed, [{InnerException.Message}]";
  }


  public class UTestRunner
  {
    public static UTestPack Run(string name)
    {
      if (!UTestExplorer.TestByName.ContainsKey(name))
      {
        return new UTestPack() { Error = new UTestNotFoundException(name) };
      }

      return Run(UTestExplorer.TestByName[name]);
    }
    public static UTestPack Run<T>() => Run(typeof(T));
    public static UTestPack Run(Type T)
    {
      if (!T.IsAssignableTo(typeof(UTestPack)))
      {
        return new UTestPack() { Error = new ArgumentException($"[{T}] is not a UTestPack") };
      }

      UTestPack pack = Activator.CreateInstance(T) as UTestPack;

      try
      {
        pack.CreateTests();
      }
      catch (Exception e)
      {
        //HACK inner exception should be handled more gracefully
        //mb intercept TargetInvocationException?
        pack.Error = new UTestRunException(T, e.InnerException ?? e);
      }

      return pack;
    }


    public static List<UTestPack> RunRecursive<T>() => RunRecursive(typeof(T));
    public static List<UTestPack> RunRecursive(Type T = null)
    {
      List<UTestPack> results = new List<UTestPack>();

      UTestExplorer.TestTree.RunRecursive((test) =>
      {
        results.Add(Run(test));
      }, T);

      return results;
    }
  }
}