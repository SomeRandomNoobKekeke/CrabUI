using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;

namespace CrabUI
{
  public class ComponentCore
  {
    public static int MaxID { get; private set; }

    public int ID { get; set; }


    public ComponentCore()
    {
      ID = MaxID++;
    }
  }
}