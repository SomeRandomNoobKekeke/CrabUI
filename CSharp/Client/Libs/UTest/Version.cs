using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BaroJunk
{
  public partial class UTest
  {
    static UTest() => ProjectInfo.Add(new PackageInfo()
    {
      Name = "UTest",
      Version = new Version(0, 0, 0)
      {
        Branch = "BaroJunk"
      },
      Dependencies = new List<PackageInfo>(){
        new PackageInfo(){
          Name = "ModStorage",
          Version = new Version(0, 0, 0) { Branch = "BaroJunk" }
        }
      }
    });
  }
}