using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace CUICodeGenerator
{
  public class InAttribute : Attribute { }
  public class LocalAttribute : Attribute { }
  public class InitMethodAttribute : Attribute { }

  public class GeneratedComponentAttribute : Attribute
  {
    public string FilePath { get; }

    public GeneratedComponentAttribute([CallerFilePath] string filePath = "")
    {
      FilePath = filePath;
    }
  }
}
