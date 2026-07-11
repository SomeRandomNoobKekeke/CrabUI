using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using BaroJunk;

namespace CrabUI
{
  //BRUH This is a mess, i have no idea what calls and require what
  public class CUIAssemblyAnalyzer
  {
    public Dictionary<Type, CUIComponentInfo> ComponentInfos { get; } = new();
    public Dictionary<Type, CUISerializableInfo> SerializableTypes { get; } = new();
    public CUITypeTree TypeTree { get; } = new();

    public CUIComponentAnalyzer CUIComponentAnalyzer { get; } = new();
    public CUISerializableAnalyzer CUISerializableAnalyzer { get; } = new();



    public bool IsComponentType(Type T) => CUIComponentAnalyzer.IsComponentType(T);

    public Type GetType(string name) => TypeTree.TypesByName.GetValueOrDefault(name);
    public CUIComponentInfo GetInfo(Type T)
    {
      if (!ComponentInfos.ContainsKey(T)) CreateInfo(T);
      return ComponentInfos[T];
    }

    private void CreateInfo(Type T)
    {
      ComponentInfos[T] = new CUIComponentInfo(T);
      CUIComponentAnalyzer.Analyze(ComponentInfos[T]);
      ComponentInfos[T].SerializableProps = SerializableTypes[T].SerializableProps;
    }

    public IEnumerable<Type> GetDerivedTypes(Type T) => TypeTree.GetDerivedTypes(T);

    public void AnalyzeAssembly(Assembly assembly)
    {
      Stopwatch sw = Stopwatch.StartNew();

      foreach (Type T in assembly.GetTypes().Where(CUISerializableAnalyzer.IsCUISerializable))
      {
        SerializableTypes[T] = CUISerializableAnalyzer.Analyze(T);
      }

      foreach (Type T in assembly.GetTypes().Where(CUIComponentAnalyzer.IsComponentType))
      {
        CreateInfo(T);
      }

      TypeTree.Add(ComponentInfos.Keys);

      sw.Stop();
      // CUI.Logger.Log($"Analyzed in {sw.ElapsedMilliseconds}");
    }
  }
}