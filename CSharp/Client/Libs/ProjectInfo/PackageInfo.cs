using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;

namespace BaroJunk
{
  public class PackageInfo
  {
    public string Name { get; set; }
    public Version Version { get; set; }
    public List<PackageInfo> Dependencies { get; set; }

    public override string ToString() => Logger.Wrap.Vars(Name, Version);
  }
}