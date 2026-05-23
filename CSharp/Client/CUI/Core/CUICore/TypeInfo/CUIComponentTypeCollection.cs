using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using ComponentGenerator;
using BaroJunk;

namespace CrabUI
{
  public class CUIComponentTypeCollection
  {
    private Dictionary<Type, CUIComponentInfo> Infos = new();

    public bool Has(Type T) => Infos.ContainsKey(T);
    public CUIComponentInfo Get(Type T) => Infos.GetValueOrDefault(T);

    public void Add(CUIComponentInfo info) => Infos.Add(info.ComponentType, info);
    public void AddRange(IEnumerable<CUIComponentInfo> infos)
    {
      foreach (CUIComponentInfo info in infos)
      {
        Add(info);
      }
    }

    public void Clear() => Infos.Clear();
  }
}