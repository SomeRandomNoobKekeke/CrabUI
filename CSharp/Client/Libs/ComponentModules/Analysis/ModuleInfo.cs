using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using System.IO;
using System.Text;

namespace BaroJunk
{
  public class ModuleInfo
  {
    public List<PropertyInfo> Path { get; }
    public PropertyInfo Property { get; }

    public Type Type { get; }
    public string Name => Property.Name;
    public string StringPath => String.Join('.', Path.Select(pi => pi.Name));

    public PropertyInfo HostProp { get; }

    public List<ModuleDependencyInfo> Dependencies { get; } = new();

    private void ScanDependencies()
    {
      Dependencies.Clear();

      PropExplorer.ForProps<IModule>(Type, (pi) =>
      {
        ModuleDependencyAttribute attribute = pi.GetCustomAttribute<ModuleDependencyAttribute>();
        if (attribute is null) return;

        Dependencies.Add(
          new ModuleDependencyInfo(
            attribute.Type ?? pi.PropertyType,
            attribute.Name ?? pi.Name,
            pi
          )
        );
      }, PropExplorer.All);
    }


    public ModuleInfo(List<PropertyInfo> path, PropertyInfo property, Type type = null)
    {
      Path = path.Append(property).ToList();
      Property = property;
      Type = type ?? Property.PropertyType;

      ScanDependencies();
      HostProp = property.PropertyType.GetProperty(IModule.HostPropName, BindingFlags.Instance | BindingFlags.Public);
    }

    public override string ToString() => $"{Type.Name} {StringPath}";
  }

}
