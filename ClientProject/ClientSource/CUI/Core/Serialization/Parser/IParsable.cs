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
  public interface IParsable
  {
    public static abstract object Parse(string raw);
    public string ToText();
  }
}