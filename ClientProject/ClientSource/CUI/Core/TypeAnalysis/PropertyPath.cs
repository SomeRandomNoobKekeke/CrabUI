using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;

namespace CrabUI
{
  public class PropertyPath
  {
    public List<PropertyInfo> Path { get; set; }
    public Type Type => Path.Last().PropertyType;

    public bool CanRead => Path.Last().CanRead;
    public bool CanWrite => Path.Last().CanWrite;

    public object GetValue(object target)
    {
      if (Path.Count == 0) return null;

      object o = target;

      for (int i = 0; i < Path.Count - 1; i++)
      {
        o = Path[i].GetValue(o);
      }

      return Path.Last().GetValue(o);
    }

    public void SetValue(object target, object value)
    {
      if (Path.Count == 0) return;

      object o = target;

      for (int i = 0; i < Path.Count - 1; i++)
      {
        o = Path[i].GetValue(o);
      }

      Path.Last().SetValue(o, value);
    }

    public PropertyPath(IEnumerable<PropertyInfo> path) => Path = path.ToList();
    public PropertyPath(List<PropertyInfo> path) => Path = path;

    public override string ToString() => String.Join('.', Path.Select(pi => pi.Name));
  }
}