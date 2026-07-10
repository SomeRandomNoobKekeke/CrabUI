using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using BaroJunk;

namespace CrabUI
{
  public class CUIAssemblyAnalyzer
  {
    public Dictionary<Type, CUIComponentInfo> Infos { get; } = new();
    public CUIComponentAnalyzer CUIComponentAnalyzer { get; } = new();
    public CUITypeTree TypeTree { get; } = new();

    public bool IsComponentType(Type T) => CUIComponentAnalyzer.IsComponentType(T);

    public Type GetType(string name) => TypeTree.TypesByName.GetValueOrDefault(name);
    public CUIComponentInfo GetInfo(Type T)
    {
      if (!Infos.ContainsKey(T)) Infos[T] = CUIComponentAnalyzer.Analyze(T);
      return Infos[T];
    }

    public IEnumerable<Type> GetDerivedTypes(Type T) => TypeTree.GetDerivedTypes(T);

    public void AnalyzeAssembly(Assembly assembly)
    {
      Stopwatch sw = Stopwatch.StartNew();
      IEnumerable<Type> types = CUIComponentAnalyzer.FindAllComponentTypesInAssembly(assembly);

      foreach (Type T in types)
      {
        Infos[T] = CUIComponentAnalyzer.Analyze(T);
      }

      TypeTree.Add(types);

      sw.Stop();
      // CUI.Logger.Log($"Analyzed in {sw.ElapsedMilliseconds}");
    }
  }
}