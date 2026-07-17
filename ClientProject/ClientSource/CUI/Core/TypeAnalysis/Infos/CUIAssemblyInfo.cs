using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;

namespace CrabUI
{
  public class CUIAssemblyInfo
  {
    public Assembly Assembly { get; set; }
    public Dictionary<Type, CUIComponentInfo> ComponentInfos { get; } = new();
    public Dictionary<Type, CUISerializableInfo> SerializableInfos { get; } = new();
  }
}