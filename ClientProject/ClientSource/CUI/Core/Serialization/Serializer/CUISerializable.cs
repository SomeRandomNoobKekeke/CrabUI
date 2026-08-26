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
  //TODO centralize serialization, move parser here, define extra methods here, make it use this interface
  /// <summary>
  /// It has Serialize() method
  /// </summary>
  public interface CUISerializable
  {
    public static abstract object Deserialize(XElement element);
    public XElement Serialize() => new XElement(GetType().Name);
  }

  /// <summary>
  /// These props should be serialized whether they are CUISerializable or not
  /// </summary>
  public class CUISerializableProp : Attribute { }
}