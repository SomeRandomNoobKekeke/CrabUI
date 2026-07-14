using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.IO;
using System.Xml.Linq;

namespace CrabUI
{
  public static class CUIDefaultSerializer
  {
    public static XElement Serialize(CUISerializable o, IDictionary<string, object> defaultValues = null)
    {
      defaultValues ??= new Dictionary<string, object>();

      XElement element = new(o.GetType().Name);

      if (CUICore.CUITypes.SerializableTypes.ContainsKey(o.GetType()))
      {
        CUISerializableInfo info = CUICore.CUITypes.SerializableTypes[o.GetType()];

        foreach (var (name, pp) in info.SerializableProps)
        {
          object value = pp.GetValue(o);
          if (defaultValues.ContainsKey(name) && Equals(value, defaultValues[name])) continue;

          element.SetAttributeValue(name, CUICore.Parser.Serialize(value));
        }
      }

      return element;
    }

    public static object Deserialize(XElement element, Type T)
    {
      object o = Activator.CreateInstance(T);

      if (CUICore.CUITypes.SerializableTypes.ContainsKey(T))
      {
        CUISerializableInfo info = CUICore.CUITypes.SerializableTypes[T];

        foreach (XAttribute attribute in element.Attributes())
        {
          PropertyPath pp = info.SerializableProps[attribute.Name.ToString()];
          pp.SetValue(o, CUICore.Parser.Parse(attribute.Value, pp.Path.Last().PropertyType));
        }
      }

      return o;
    }
  }
}
