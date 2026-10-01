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

namespace CursedUI
{
  public static class CUIBasicSerializer
  {
    public static XElement Serialize(CUISerializable o, IDictionary<string, object> defaultValues)
    {
      XElement element = new(o.GetType().GetFullName());

      if (CUICore.Reflection.SerializableInfos.TryGetValue(o.GetType(), out CUISerializableInfo? info))
      {
        foreach (var (fullName, pp) in info.SerializableProps)
        {
          object value = pp.GetValue(o);
          if (defaultValues.ContainsKey(fullName) && Equals(value, defaultValues[fullName])) continue;

          element.SetAttributeValue(fullName, CUICore.Parser.Serialize(value));
        }

        foreach (var (fullName, pp) in info.CustomSerializableProps)
        {
          ICustomSerializable value = (ICustomSerializable)pp.GetValue(o);

          //TODO it's excessive
          if (defaultValues.ContainsKey(fullName) && Equals(value, defaultValues[fullName])) continue;

          element.SetAttributeValue(
            fullName,
            value.ToText(defaultValues.GetValueOrDefault(fullName))
          );
        }
      }

      return element;
    }

    public static object Deserialize(XElement element, Type T)
    {
      object o = Activator.CreateInstance(T);
      DeserializeProps(element, o);
      return o;
    }

    public static void DeserializeProps(XElement element, object target)
    {
      if (CUICore.Reflection.SerializableInfos.TryGetValue(target.GetType(), out CUISerializableInfo? info))
      {
        foreach (XAttribute attribute in element.Attributes())
        {
          PropertyPath pp = info.ParsableProps[attribute.Name.ToString()];

          if (!pp.CanWrite)
          {
            CUI.Logger.Warning($"Couldn't deserialize [{pp}] on [{target.GetType().GetFullName()}], it's not settable");
            continue;
          }

          pp.SetValue(target, CUICore.Parser.Parse(attribute.Value, pp.Path.Last().PropertyType));
        }
      }
    }
  }
}
