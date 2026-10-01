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
  public interface ICustomSerializable : IParsable
  {
    public string ToText(object defValue);
  }

  public interface ICustomSerializable<T> : ICustomSerializable
  {
    string ICustomSerializable.ToText(object defValue) => ToText((T)defValue);
    public string ToText(T defValue);
  }
}