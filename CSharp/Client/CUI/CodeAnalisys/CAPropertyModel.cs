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

  public class CAPropertyModel
  {
    public PropertyInfo Property { get; }
    public List<PropertyInfo> FullPath { get; }

    public Type Type => Property.PropertyType;
    public string Name => Property.Name;

    public string StringPath => string.Join('.', FullPath.Select(pi => pi.Name));

    public CAPropertyModel(PropertyInfo prop, IEnumerable<PropertyInfo> path)
    {
      FullPath = path.Append(prop).ToList();
      Property = prop;
    }

    public override string ToString() => $"{Type.Name} {Name}";
  }
}