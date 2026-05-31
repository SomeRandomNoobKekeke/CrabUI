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
  public partial class Logger : ILogger
  {
    void ILogger.Log(object msg) => Log(msg);
    void ILogger.Warning(object msg) => Warning(msg);
    void ILogger.Error(object msg) => Error(msg);
  }
}
