using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CUILibs;

namespace CrabUI
{
  public class CUIStylePipeline
  {
    private List<ICUIStyle> styles = new();
    public IReadOnlyList<ICUIStyle> Styles { get; }

    public event Action Changed;


    public void Remove(ICUIStyle style)
    {
      styles.Remove(style);
      Changed?.Invoke();
    }

    public void Remove(string id)
    {
      styles.RemoveAll(style => style.ID == id);
      Changed?.Invoke();
    }

    public void Remove(CUIStyleCategory category)
    {
      styles.RemoveAll(style => style.Category == category);
      Changed?.Invoke();
    }

    public void Clear()
    {
      styles.Clear();
      Changed?.Invoke();
    }


    public void Add(ICUIStyle style)
    {
      if (styles.Count == 0 || styles.Last().Priority >= style.Priority)
      {
        styles.Add(style);
      }
      else
      {
        styles.Add(style);
        Sort();
      }

      Changed?.Invoke();
    }

    public void Sort()
    {
      styles.Sort((a, b) => b.Priority - a.Priority);
    }

    public void AddSilent(ICUIStyle style) => styles.Add(style);
    public void RemoveSilent(ICUIStyle style) => styles.Remove(style);

    public void Apply(CUIComponent component)
    {
      foreach (ICUIStyle style in styles)
      {
        style.Apply(component);
      }
    }

    public CUIStylePipeline()
    {
      Styles = styles.AsReadOnly();
    }
  }
}