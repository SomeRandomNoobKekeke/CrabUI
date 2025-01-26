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

namespace CrabUI
{
  public partial class CUI
  {
    //Idk, not very usefull
    /// <summary>
    /// Just an experimant
    /// Creates empty CUIComponent from class name 
    /// </summary>
    /// <param name="componentName"></param>
    /// <returns></returns>
    public static CUIComponent Create(string componentName)
    {
      return (CUIComponent)Activator.CreateInstance(CUIReflection.GetComponentTypeByName(componentName));
    }
  }
}
