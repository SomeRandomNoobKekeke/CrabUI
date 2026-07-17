using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public partial class CUICore
  {
    public Reflection_Part _Reflection { get; } = new();
    public class Reflection_Part : Part
    {
      public CUITypeTree TypeTree { get; } = new();
      public Dictionary<Type, CUIComponentInfo> ComponentInfos { get; } = new();
      public Dictionary<Type, CUISerializableInfo> SerializableInfos { get; } = new();

      public HashSet<Assembly> KnownAssemblies { get; } = new();


      public Type GetType(string name) => TypeTree.TypesByName.GetValueOrDefault(name);
      public CUIComponentInfo GetComponentInfo(Type T)
      {
        if (!ComponentInfos.ContainsKey(T)) throw new Exception($"CUIComponentInfo for [{T}] is missing");
        return ComponentInfos[T];
      }
      public CUISerializableInfo GetSerializableInfo(Type T)
      {
        if (!SerializableInfos.ContainsKey(T)) throw new Exception($"CUISerializableInfo for [{T}] is missing");
        return SerializableInfos[T];
      }


      public IEnumerable<Type> GetDerivedTypes(Type T) => TypeTree.GetDerivedTypes(T);

      public void AddAssemblyInfo(CUIAssemblyInfo assemblyInfo)
      {
        if (KnownAssemblies.Contains(assemblyInfo.Assembly)) return;
        KnownAssemblies.Add(assemblyInfo.Assembly);

        foreach (var (type, componentInfo) in assemblyInfo.ComponentInfos)
        {
          ComponentInfos[type] = componentInfo;
        }

        foreach (var (type, serializableInfo) in assemblyInfo.SerializableInfos)
        {
          SerializableInfos[type] = serializableInfo;
        }


        TypeTree.Add(assemblyInfo.ComponentInfos.Keys);

        foreach (CUIComponentInfo info in assemblyInfo.ComponentInfos.Values)
        {
          if (info.DefaultStyle is not null)
          {
            Self.CUIStyleManager.AddDefaultStyle(info.DefaultStyle);
          }
        }

        //Note: it's important to create defaults only after setting DefaultStyles or they'll create empty style pipelines
        //TODO these defaults should somehow be created with dummy resources, textures, sounds etc
        //CRINGE why here?
        foreach (var (T, info) in assemblyInfo.ComponentInfos)
        {
          info.DefaultValue = Self.CUIAssemblyAnalyzer.CUIComponentAnalyzer.CreateDefault(T);
        }
      }
    }
  }
}