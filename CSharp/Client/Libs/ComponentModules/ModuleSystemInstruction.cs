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
  public record ModuleSystemInstruction;

  public record InjectHostInstruction(
    PropertyInfo[] FullPath,
    PropertyInfo HostProp
  ) : ModuleSystemInstruction();

  public record InjectDependencyInstruction(
    PropertyInfo[] TargetPath,
    PropertyInfo[] DependencyPath,
    PropertyInfo Property
  ) : ModuleSystemInstruction();
}
