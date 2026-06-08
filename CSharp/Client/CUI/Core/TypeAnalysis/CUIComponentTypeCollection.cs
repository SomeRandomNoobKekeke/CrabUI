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
    private Dictionary<string, Type> TypesByName = new();


    //BRUH Why it returns type and not CUIComponentInfo?
    //TODO because it's doing 2 jobs, tracking types and infos, it should be 2 different classes
    public Type ByName(string name) => TypesByName[name];

    public bool Has(Type T) => Infos.ContainsKey(T);
    public CUIComponentInfo Get(Type T) => Infos.GetValueOrDefault(T);

    public void Add(CUIComponentInfo info)
    {
      Infos.Add(info.ComponentType, info);
      TypesByName.Add(info.ComponentType.Name, info.ComponentType);
    }
    public void AddRange(IEnumerable<CUIComponentInfo> infos)
    {
      foreach (CUIComponentInfo info in infos)
      {
        Add(info);
      }
    }

    public void Clear()
    {
      Infos.Clear();
      TypesByName.Clear();
    }
  }
}