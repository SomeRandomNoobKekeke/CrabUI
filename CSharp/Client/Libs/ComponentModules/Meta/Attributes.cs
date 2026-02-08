using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using System.IO;
using System.Text;

namespace BaroJunk
{
  public class ModuleAttribute : Attribute
  {
    public Type Type { get; set; }
    public ModuleAttribute(Type type = null)
    {
      Type = type;
    }
  }

  public class ModuleDependencyAttribute : Attribute
  {
    public string Name { get; set; }
    public Type Type { get; set; }

    public ModuleDependencyAttribute(Type type = null, string name = null)
    {
      Name = name;
      Type = type;
    }
  }

}
