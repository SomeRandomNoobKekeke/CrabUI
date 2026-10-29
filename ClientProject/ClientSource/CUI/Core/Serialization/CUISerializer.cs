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
  public partial class CUISerializer
  {
    public XElement Serialize(object o)
    {
      if (o is CUISerializable) return ((CUISerializable)o).Serialize();
      return new XElement(o.GetType().Name);
    }

    public T Deserialize<T>(XElement element) => (T)Deserialize(element, typeof(T));
    public object Deserialize(XElement element, Type T)
    {
      if (T.IsAssignableTo(typeof(CUISerializable)))
      {
        MethodInfo? deserialize = T.GetMethod("Deserialize", BindingFlags.Static | BindingFlags.Public);
        if (deserialize is null)
        {
          CUI.Logger.Warning($"Can't deserialize [{T}]");
          return T.GetDefaultValue();
        }

        return deserialize.Invoke(null, [element]);
      }

      return T.GetDefaultValue();
    }


  }
}
