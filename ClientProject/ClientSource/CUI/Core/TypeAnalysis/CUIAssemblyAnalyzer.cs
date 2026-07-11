using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using BaroJunk;

namespace CrabUI
{
  public class CUIAssemblyAnalyzer
  {
    public Dictionary<Type, CUIComponentInfo> ComponentInfos { get; } = new();
    public Dictionary<Type, CUISerializableInfo> SerializableTypes { get; } = new();
    public CUITypeTree TypeTree { get; } = new();

    private CUIComponentAnalyzer CUIComponentAnalyzer { get; } = new();
    private CUISerializableAnalyzer CUISerializableAnalyzer { get; } = new();



    public bool IsComponentType(Type T) => CUIComponentAnalyzer.IsComponentType(T);

    public Type GetType(string name) => TypeTree.TypesByName.GetValueOrDefault(name);
    public CUIComponentInfo GetInfo(Type T)
    {
      if (!ComponentInfos.ContainsKey(T)) ComponentInfos[T] = CUIComponentAnalyzer.Analyze(T);
      return ComponentInfos[T];
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
        ComponentInfos[T] = CUIComponentAnalyzer.Analyze(T);
        ComponentInfos[T].SerializableProps = SerializableTypes[T].SerializableProps;
      }

      TypeTree.Add(ComponentInfos.Keys);

      sw.Stop();
      // CUI.Logger.Log($"Analyzed in {sw.ElapsedMilliseconds}");
    }
  }
}