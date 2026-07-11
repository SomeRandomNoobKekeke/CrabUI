using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using BaroJunk;
using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{
  public partial class CUIComponent : CUISerializable
  {

    public static CUIComponent CreateByName(string name)
    {
      return (CUIComponent)Activator.CreateInstance(CUI.CUITypes.GetType(name));
    }

    public virtual XElement Serialize()
    {
      XElement element = CUIDefaultSerializer.Serialize(this);

      foreach (CUIComponent child in Tree.Children)
      {
        element.Add(child.Serialize());
      }

      return element;
    }
  }
}