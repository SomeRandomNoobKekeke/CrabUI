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

namespace CursedUI
{
  public partial class CUICore
  {
    public static TypeAnalysis_Part TypeAnalysis => Instance._TypeAnalysis;
    public TypeAnalysis_Part _TypeAnalysis { get; } = new();
    public class TypeAnalysis_Part : Part
    {
      public TypeTree ComponentTypeTree { get; } = new();
      public Dictionary<Type, CUIComponentInfo> ComponentInfos { get; } = new();
      public Dictionary<Type, CUISerializableInfo> SerializableInfos { get; } = new();

      private ComponentAnalyzer ComponentAnalyzer = new();
      private CUISerializableAnalyzer CUISerializableAnalyzer = new();

      public bool IsComponent(Type T) => T.IsAssignableTo(typeof(CUIVisualComponent));
      public bool IsSerializable(Type T) => T.IsAssignableTo(typeof(CUISerializable));

      public void AnalyzeType(Type T)
      {
        if (IsSerializable(T)) SerializableInfos[T] = CUISerializableAnalyzer.Analyze(T);

        if (IsComponent(T))
        {
          ComponentInfos[T] = ComponentAnalyzer.Analyze(T);
          if (IsSerializable(T))
          {
            ComponentInfos[T].SerializableProps = SerializableInfos[T].SerializableProps;
          }
          ComponentTypeTree.Add(T);
        }
      }

      private HashSet<string> AnalyzedAssemblies = new(); //TODO is fullName enough?
      public void AnalyzeAssembly(Assembly assembly)
      {
        if (AnalyzedAssemblies.Contains(assembly.FullName)) return;
        AnalyzedAssemblies.Add(assembly.FullName);

        List<Type> componentTypes = new();
        foreach (Type T in assembly.GetTypes())
        {
          if (IsSerializable(T)) SerializableInfos[T] = CUISerializableAnalyzer.Analyze(T);

          if (IsComponent(T))
          {
            ComponentInfos[T] = ComponentAnalyzer.Analyze(T);

            if (IsSerializable(T))
            {
              ComponentInfos[T].SerializableProps = SerializableInfos[T].SerializableProps;
            }

            componentTypes.Add(T);
          }
        }

        ComponentTypeTree.Add(componentTypes);
      }


      public CUIComponentInfo GetComponentInfo(Type T)
      {
        if (!T.IsAssignableTo(typeof(CUIVisualComponent))) return null;
        if (!ComponentInfos.ContainsKey(T)) AnalyzeType(T);
        return ComponentInfos[T];
      }

      public CUISerializableInfo GetSerializableInfo(Type T)
      {
        if (!T.IsAssignableTo(typeof(CUIVisualComponent))) return null;
        if (!SerializableInfos.ContainsKey(T)) AnalyzeType(T);
        return SerializableInfos[T];
      }
    }
  }
}