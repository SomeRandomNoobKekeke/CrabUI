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

  public class ModuleMap
  {
    public static TypeInfoCache<ModuleMap> Cache = new();
    public static ModuleMap GetFor<T>() => Cache.Get(typeof(T));
    public static ModuleMap GetFor(Type T) => Cache.Get(T);

    public Type RootType { get; }

    public Dictionary<string, ModuleInfo> this[Type T]
    {
      get => Modules[T];
      set => Modules[T] = value;
    }
    public Dictionary<Type, Dictionary<string, ModuleInfo>> Modules = new();

    public IEnumerable<ModuleInfo> AllModules
    {
      get
      {
        foreach (var dict in Modules.Values)
        {
          foreach (ModuleInfo info in dict.Values)
          {
            yield return info;
          }
        }
      }
    }

    private record ScanContext(List<PropertyInfo> Path, bool InsideAContainer);

    private void ScanModules()
    {
      List<ModuleInfo> modules = new();

      void ScanContainer(Type containerType, ScanContext context)
      {
        PropExplorer.ForProps<IModuleContainer>(containerType, (pi) =>
        {
          ScanContainer(
            pi.PropertyType,
            context with
            {
              Path = context.Path.Append(pi).ToList(),
              InsideAContainer = true,
            }
          );
        }, PropExplorer.All);

        //TODO do i need to track modules that don't implement IModule? i think no
        PropExplorer.ForProps<IModule>(containerType, (pi) =>
        {
          ModuleAttribute attribute = pi.GetCustomAttribute<ModuleAttribute>();

          if (!context.InsideAContainer && attribute is null) return;

          modules.Add(new ModuleInfo(context.Path, pi, attribute?.Type));
        }, PropExplorer.All);
      }

      ScanContainer(RootType, new ScanContext(new List<PropertyInfo>(), false));


      Modules = modules.GroupBy(m => m.Type).ToDictionary(
        types => types.Key,
        types => types.GroupBy(m => m.Name).ToDictionary(
          names => names.Key,
          names => names.First() // TODO detect collisions
        )
      );
    }

    private ModuleMap(Type T)
    {
      if (!T.IsAssignableTo(typeof(IComponent)))
      {
        throw new ArgumentException($"can't create ModuleMap for [{T}], it must be an [{typeof(IComponent)}] type");
      }

      RootType = T;
      ScanModules();
    }

    public override string ToString() => Logger.Wrap.IDictionary(Modules.ToDictionary(
      kvp => kvp.Key.Name,
      kvp => Logger.Wrap.IEnumerable(kvp.Value.Keys)
    ));
  }


}
