using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CursedUIUser
{

  /// <summary>
  /// hehehe
  /// </summary>
  public class HowManyTypesCanBePuttedInsideGenerics : Experiment
  {
    public class RealType1 { }
    public class RealType2 { }
    public class RealType3 { }
    public class RealType4 { }
    public class RealType5 { }
    public class RealType6 { }
    public class RealType7 { }
    public class RealType8 { }
    public class RealType9 { }
    public class RealType10 { }
    public class RealType11 { }
    public class RealType12 { }
    public class RealType13 { }
    public class RealType14 { }
    public class RealType15 { }
    public class RealType16 { }
    public class RealType17 { }
    public class RealType18 { }
    public class RealType19 { }
    public class RealType20 { }

    public class MulticastCringe<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>
    {

    }

    public override void Run()
    {
      MulticastCringe<RealType1, RealType2, RealType3, RealType4, RealType5, RealType6, RealType7, RealType8, RealType9, RealType10, RealType11, RealType12, RealType13, RealType14, RealType15, RealType16, RealType17, RealType18, RealType19, RealType20> bruh = new();
    }
  }
}