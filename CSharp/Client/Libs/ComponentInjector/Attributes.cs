using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace ComponentInjector
{
  public class InAttribute : Attribute { }
  public class OutAttribute : Attribute { }

  public class ThisIsAlsoAttribute : Attribute
  {
    public Type Type { get; }
    public ThisIsAlsoAttribute(Type type) => Type = type;
  }

  public class GeneratedComponentAttribute : Attribute
  {
    public string FilePath { get; }

    public GeneratedComponentAttribute([CallerFilePath] string filePath = "")
    {
      FilePath = filePath;
    }
  }

  public class GeneratorTargetAttribute : Attribute { }
}
