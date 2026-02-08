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
  public interface IModule
  {
    public static string HostPropName = "Host";
  }
  public interface IModuleContainer { }
  public interface IComponent : IModuleContainer { }
}
