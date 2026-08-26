using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;
using System.Xml.Linq;

namespace CursedUI
{
  public enum CUISerializationMode
  {
    Replace, Merge, Ignore,
  }
}