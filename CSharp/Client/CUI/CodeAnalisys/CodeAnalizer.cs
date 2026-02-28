using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using System.IO;

namespace CrabUI
{

  public class CodeAnalizer
  {
    public static event Action OnClearCache;
    public static void ClearCache() => OnClearCache?.Invoke();
    public Dictionary<Type, CAComponentModel> Components = new();


    public CAComponentModel AnalyzeComponent(Type componentType)
    {
      Components[componentType] = new CAComponentModel(componentType);
      return Components[componentType];
    }
  }
}