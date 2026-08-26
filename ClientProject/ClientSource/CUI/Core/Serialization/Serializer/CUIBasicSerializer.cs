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
    public static XElement Serialize(CUISerializable o, IDictionary<string, object> defaultValues = null)
    {
      defaultValues ??= new Dictionary<string, object>();

      XElement element = new(o.GetType().GetFullName());

      if (CUICore.Reflection.SerializableInfos.ContainsKey(o.GetType()))
      {
        CUISerializableInfo info = CUICore.Reflection.GetSerializableInfo(o.GetType());

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
      DeserializeProps(element, o);
      return o;
    }

    public static void DeserializeProps(XElement element, object target)
    {
      if (CUICore.Reflection.SerializableInfos.ContainsKey(target.GetType()))
      {
        CUISerializableInfo info = CUICore.Reflection.GetSerializableInfo(target.GetType());

        foreach (XAttribute attribute in element.Attributes())
        {
          PropertyPath pp = info.SerializableProps[attribute.Name.ToString()];

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
