using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using ComponentGenerator;
using BaroJunk;

namespace CrabUI
{
  public class CUIComponentTypeManager
  {
    public CUIComponentAnalyzer Analyzer { get; } = new();
    public CUIComponentTypeCollection Infos { get; } = new();

    public void Add(CUIComponentInfo info) => Infos.Add(info);
    public void AddRange(IEnumerable<CUIComponentInfo> infos) => Infos.AddRange(infos);
    public void Clear() => Infos.Clear();

    public CUIComponentInfo Get(Type T)
    {
      if (!Infos.Has(T)) Infos.Add(Analyzer.Analyze(T));
      return Infos.Get(T);
    }

    public bool IsComponentType(Type T) => Analyzer.IsComponentType(T);

    public CUIComponentInfo Analyze(Type componentType)
      => Analyzer.Analyze(componentType);

    public IEnumerable<Type> FindAllComponentTypesInAssembly(Assembly assembly)
      => Analyzer.FindAllComponentTypesInAssembly(assembly);

    public void AnalyzeAssembly(Assembly assembly)
      => Infos.AddRange(Analyzer.AnalyzeAssembly(assembly));
  }
}