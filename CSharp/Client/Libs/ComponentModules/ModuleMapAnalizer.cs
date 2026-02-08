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

  public static class ModuleMapAnalizer
  {
    public static IEnumerable<InjectHostInstruction> CreateInjectHostInstructions(ModuleMap map)
    {
      foreach (ModuleInfo info in map.AllModules)
      {
        if (info.HostProp is not null)
        {
          yield return new InjectHostInstruction(
            info.Path.ToArray(),
            info.HostProp
          );
        }
      }
    }

    public static IEnumerable<InjectDependencyInstruction> CreateInjectDependencyInstructions(ModuleMap map)
    {
      foreach (ModuleInfo info in map.AllModules)
      {
        foreach (ModuleDependencyInfo dependency in info.Dependencies)
        {
          if (
            !map.Modules.ContainsKey(dependency.Type) ||
            !map[dependency.Type].ContainsKey(dependency.Name)
          )
          {
            ModuleManager.Logger.Warning($"Missing module dependency: in {map.RootType}: {info.StringPath} -> {dependency}");
            continue;
          }

          if (!map[dependency.Type][dependency.Name].Type.IsAssignableTo(dependency.Property.PropertyType))
          {
            ModuleManager.Logger.Warning($"Incompatible dependency: in {map.RootType}: {info.StringPath} -> {dependency}");
            continue;
          }

          yield return new InjectDependencyInstruction(
            info.Path.ToArray(),
            map[dependency.Type][dependency.Name].Path.ToArray(),
            dependency.Property
          );
        }
      }
    }
  }


}
