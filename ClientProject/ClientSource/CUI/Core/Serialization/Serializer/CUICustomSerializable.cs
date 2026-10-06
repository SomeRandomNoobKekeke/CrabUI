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

  public interface CUICustomSerializable : IParsable
  {
    public string ToText(object defValue);
  }
}