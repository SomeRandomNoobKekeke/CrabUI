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

namespace CrabUI
{
  public interface IModule { }
  public interface IModuleContainer { }
  public interface IComponent : IModuleContainer { }
}
