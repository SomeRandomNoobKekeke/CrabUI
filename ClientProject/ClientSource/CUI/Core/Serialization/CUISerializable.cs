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
  /// If Prop implementing NestedCUISerializable has CUISerializableProp it will be scanned for nested props  
  /// They will be added to outer CUISerializable, e.g. Background.Color
  /// </summary>
  public interface NestedCUISerializable { }

  /// <summary>
  /// It has Serialize() and Deserialize methods  
  /// And can be serialized directly
  /// </summary>
  public interface CUISerializable : NestedCUISerializable
  {
    public static abstract object Deserialize(XElement element);
    public XElement Serialize() => new XElement(GetType().Name);
  }

  /// <summary>
  /// These props should be serialized whether they are CUISerializable or not
  /// </summary>
  public class CUISerializableProp : Attribute { }
}