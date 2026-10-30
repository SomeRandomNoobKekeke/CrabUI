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


    //HACK build script in this project compiles it with RELEASE var, but luatrauma "in-memory" mods are compiled without RELEASE or DEBUG
    // these CallerFilePath leak file system info into compiled dll and are only used when deving as "in-memory" mod
    // so i'll just brick them in dll 
#if RELEASE
    public GeneratedComponentAttribute()
    {
      FilePath = "???";
    }
#else
    public GeneratedComponentAttribute([CallerFilePath] string filePath = "")
    {
      FilePath = filePath;
    }
#endif



  }
}
