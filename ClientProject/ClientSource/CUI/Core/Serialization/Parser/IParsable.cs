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
  /// <summary>
  /// This thing can be parsed to / from text
  /// </summary>
  public interface IParsable
  {
    public static abstract object Parse(string raw);
    public string ToText();
  }
}