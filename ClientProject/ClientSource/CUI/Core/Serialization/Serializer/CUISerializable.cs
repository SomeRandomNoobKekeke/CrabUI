using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using BaroJunk;
using System.Xml.Linq;

namespace CrabUI
{
  //TODO centralize serialization, move parser here, define extra methods here, make it use this interface
  /// <summary>
  /// It has Serialize() method
  /// </summary>
  public interface CUISerializable : CUISerializableContainer
  {
    // public static object Deserialize(XElement element);
    public XElement Serialize();
  }

  /// <summary>
  /// It can contain CUISerializableProp or CUISerializableContainer
  /// </summary>
  public interface CUISerializableContainer
  {

  }

  /// <summary>
  /// These props should be serialized whether they are CUISerializable or not
  /// </summary>
  public class CUISerializableProp : Attribute { }
}