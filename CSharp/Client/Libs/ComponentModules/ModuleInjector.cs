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

  public static class ModuleInjector
  {
    public static void InjectModules(IComponent host)
    {
      ModuleMap map = ModuleMap.GetFor(host.GetType());
      InjectHosts(host, ModuleMapAnalizer.CreateInjectHostInstructions(map));
      InjectDependencies(host, ModuleMapAnalizer.CreateInjectDependencyInstructions(map));
    }

    public static object GetNested(object target, IEnumerable<PropertyInfo> path)
    {
      foreach (PropertyInfo pi in path)
      {
        target = pi.GetValue(target);
        if (target is null) return null;
      }

      return target;
    }

    private static void InjectHost(IComponent host, InjectHostInstruction instruction)
    {
      object module = GetNested(host, instruction.FullPath);

      if (module is null)
      {
        ModuleManager.Logger.Warning($"Module is not initialized: {host}.{String.Join('.', instruction.FullPath.Select(pi => pi.Name))}");
        return;
      }

      instruction.HostProp.SetValue(module, host);
    }

    private static void InjectDependency(IComponent host, InjectDependencyInstruction instruction)
    {
      object dependency = GetNested(host, instruction.DependencyPath);
      object target = GetNested(host, instruction.TargetPath);

      if (dependency is null)
      {
        ModuleManager.Logger.Warning($"Dependency module is not initialized: {host}.{String.Join('.', instruction.DependencyPath.Select(pi => pi.Name))}");
        return;
      }

      if (target is null)
      {
        ModuleManager.Logger.Warning($"Module {host}.{String.Join('.', instruction.TargetPath.Select(pi => pi.Name))} is not initialized");
        return;
      }

      instruction.Property.SetValue(target, dependency);
    }


    public static void InjectHosts(IComponent host, IEnumerable<InjectHostInstruction> instructions)
    {
      foreach (var instruction in instructions) { InjectHost(host, instruction); }
    }

    public static void InjectDependencies(IComponent host, IEnumerable<InjectDependencyInstruction> instructions)
    {
      foreach (var instruction in instructions) { InjectDependency(host, instruction); }
    }
  }


}
