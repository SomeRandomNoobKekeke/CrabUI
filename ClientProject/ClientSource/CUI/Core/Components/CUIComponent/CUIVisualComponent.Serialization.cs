using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{
  public partial class CUIComponent : CUISerializable
  {
    public static new T Deserialize<T>(XElement element) where T : CUIComponent => (T)Deserialize(element);
    public static new CUIComponent Deserialize(XElement element)
      => CUIVisualComponent.Deserialize(element) as CUIComponent;
  }
}