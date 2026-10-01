using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;

namespace CursedUI
{
  public class CUISerializableInfo
  {
    public Dictionary<string, PropertyPath> ParsableProps { get; set; } = new();
    public Dictionary<string, PropertyPath> SerializableProps { get; set; } = new();
    public Dictionary<string, PropertyPath> CustomSerializableProps { get; set; } = new();
  }
}