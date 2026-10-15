using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CUILibs;
namespace CursedUI
{
  public class CUIStyleManager
  {
    public bool UseReactiveStyles { get; set; } = true;

    public DictOfLists<Type, ICUIStyle> ContextStyles { get; } = new();
    private Dictionary<Type, CUIStylePipeline> Pipelines { get; } = new();

    public CUIStylePipeline GetOrCreatePipeline<T>() => GetOrCreatePipeline(typeof(T));
    public CUIStylePipeline GetOrCreatePipeline(Type T)
    {
      if (!Pipelines.ContainsKey(T)) Pipelines[T] = CreatePipeline(T);
      return Pipelines[T];
    }


    private CUIStylePipeline CreatePipeline(Type T)
    {
      CUIStylePipeline pipeline = new CUIStylePipeline();

      List<CUIComponentInfo> infoChain =
        T.GetTypeChainTo(typeof(CUIVisualComponent))
        .Select(t => CUICore.TypeAnalysis.GetComponentInfo(t))
        .ToList();

      infoChain.Reverse();

      foreach (CUIComponentInfo info in infoChain)
      {
        if (info.DefaultStyle is not null)
        {
          pipeline.DefaultStyles.Add(info.DefaultStyle);
        }
      }

      return pipeline;
    }

    //TODO prevent adding the same style
    public void AddStyle(ICUIStyle style)
    {
      foreach (Type T in CUICore.TypeAnalysis.ComponentTypeTree.GetDerivedTypes(style.TargetType))
      {
        GetOrCreatePipeline(T).UserStyles.Add(style);
      }
    }

    public void RemoveStyle(ICUIStyle style)
    {
      foreach (Type T in CUICore.TypeAnalysis.ComponentTypeTree.GetDerivedTypes(style.TargetType))
      {
        GetOrCreatePipeline(T).UserStyles.Remove(style);
      }
    }

    public void RemoveStyleById<T>(string id) => RemoveStyleById(typeof(T), id);
    public void RemoveStyleById(Type T, string id)
    {
      ICUIStyle style = GetOrCreatePipeline(T).UserStyles.FirstOrDefault(s => s.ID == id);
      if (style is null) return;

      RemoveStyle(style);
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