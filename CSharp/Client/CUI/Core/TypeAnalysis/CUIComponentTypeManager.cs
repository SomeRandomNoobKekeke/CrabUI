using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using ComponentGenerator;
using BaroJunk;

namespace CrabUI
{
  /// <summary>
  /// Exposes all you need to know about CUI types
  /// </summary>
  public class CUIComponentTypeManager
  {
    public bool IsComponentType(Type T) => Analyzer.IsComponentType(T);

    public Type GetType(string name) => TypeTree.TypesByName.GetValueOrDefault(name);
    public CUIComponentInfo GetInfo(Type T)
    {
      if (!Infos.ContainsKey(T)) Infos[T] = Analyzer.Analyze(T);
      return Infos[T];
    }

    public IEnumerable<Type> GetDerivedTypes(Type T) => TypeTree.GetDerivedTypes(T);

    public void AnalyzeAssembly(Assembly assembly)
    {
      IEnumerable<Type> types = Analyzer.FindAllComponentTypesInAssembly(assembly);

      foreach (Type T in types)
      {
        Infos[T] = Analyzer.Analyze(T);
      }

      TypeTree.Add(types);
    }


    private CUIComponentAnalyzer Analyzer { get; } = new();
    private Dictionary<Type, CUIComponentInfo> Infos { get; } = new();
    private CUITypeTree TypeTree { get; } = new();
  }
}