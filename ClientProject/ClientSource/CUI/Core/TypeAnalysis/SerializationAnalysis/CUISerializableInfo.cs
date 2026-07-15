using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;

namespace CrabUI
{
  public class CUISerializableInfo
  {
    public Dictionary<string, PropertyPath> SerializableProps { get; set; }
  }
}