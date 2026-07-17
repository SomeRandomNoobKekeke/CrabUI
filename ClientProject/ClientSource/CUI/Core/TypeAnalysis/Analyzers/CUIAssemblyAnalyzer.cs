using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;

namespace CrabUI
{
  public class CUIAssemblyAnalyzer
  {
    public CUIComponentAnalyzer CUIComponentAnalyzer { get; } = new();
    public CUISerializableAnalyzer CUISerializableAnalyzer { get; } = new();

    public CUIAssemblyInfo AnalyzeAssembly(Assembly assembly)
    {
      CUIAssemblyInfo info = new CUIAssemblyInfo();

      info.Assembly = assembly;

      foreach (Type T in assembly.GetTypes().Where(CUISerializableAnalyzer.IsCUISerializable))
      {
        info.SerializableInfos[T] = CUISerializableAnalyzer.Analyze(T);
      }

      foreach (Type T in assembly.GetTypes().Where(CUIComponentAnalyzer.IsComponentType))
      {
        info.ComponentInfos[T] = CUIComponentAnalyzer.Analyze(T);

        if (info.SerializableInfos.ContainsKey(T)) //HACK
        {
          info.ComponentInfos[T].SerializableProps = info.SerializableInfos[T].SerializableProps;
        }
      }

      return info;
    }
  }
}