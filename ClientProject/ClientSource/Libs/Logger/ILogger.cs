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
  public interface ILogger
  {
    public void Log(object msg);
    public void Warning(object msg);
    public void Error(object msg);
  }
}
