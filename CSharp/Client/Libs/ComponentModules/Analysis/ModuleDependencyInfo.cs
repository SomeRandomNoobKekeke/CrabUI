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
  public record ModuleDependencyInfo(Type Type, string Name, PropertyInfo Property)
  {
    public override string ToString() => $"{Type.Name} {Name}";
  }
}
