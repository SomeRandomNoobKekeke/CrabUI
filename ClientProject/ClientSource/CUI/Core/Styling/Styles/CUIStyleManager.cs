using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CUILibs;
namespace CrabUI
{
  public class CUIStyleManager(CUITypeTree typeTree)
  {
    private CUITypeTree TypeTree = typeTree;
    public bool UseReactiveStyles { get; set; } = true;

    public DictOfLists<Type, ICUIStyle> ContextStyles { get; } = new();

    private Dictionary<Type, ICUIStyle> DefaultStyles { get; } = new();
    private Dictionary<Type, CUIStylePipeline> Pipelines { get; } = new();

    public CUIStylePipeline GetOrCreatePipeline<T>() => GetOrCreatePipeline(typeof(T));
    public CUIStylePipeline GetOrCreatePipeline(Type T)
    {
      if (!Pipelines.ContainsKey(T)) Pipelines[T] = CreatePipeline(T);
      return Pipelines[T];
    }

    public void AddDefaultStyle(ICUIStyle style) => DefaultStyles[style.TargetType] = style;

    /// <summary>
    /// Note: DefaultStyles for base types should be there before creating the pipeline
    /// </summary>
    private CUIStylePipeline CreatePipeline(Type T)
    {
      CUIStylePipeline pipeline = new CUIStylePipeline();

      List<Type> typeChain = Utils.GetTypeChain(T, typeof(CUIVisualComponent)).ToList();
      typeChain.Reverse();

      foreach (Type type in typeChain)
      {
        if (DefaultStyles.ContainsKey(type))
        {
          pipeline.Add(DefaultStyles[type]);
        }
      }

      return pipeline;
    }

    public void AddStyle(ICUIStyle style)
    {
      foreach (Type T in TypeTree.GetDerivedTypes(style.TargetType))
      {
        GetOrCreatePipeline(T).Add(style);
      }
    }

    public void RemoveStyle(ICUIStyle style)
    {
      foreach (Type T in TypeTree.GetDerivedTypes(style.TargetType))
      {
        GetOrCreatePipeline(T).Remove(style);
      }
    }



    public void EnterContextStyle(Type T, ICUIStyle style)
    {
      ContextStyles.Add(T, style);
    }

    public void ExitContextStyle(Type T, ICUIStyle style)
    {
      ContextStyles.Remove(T, style);
    }



  }


}