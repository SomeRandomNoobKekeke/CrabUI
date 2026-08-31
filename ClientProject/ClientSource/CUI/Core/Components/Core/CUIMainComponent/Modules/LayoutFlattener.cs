using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CursedUI
{
  public class LayoutFlattener : IModule
  {
    // public CUIDebugNode<List<CUIVisualComponent>> Debug_LayoutFlatten { get; } = new(DebugCategory.LayoutFlatten)
    // {
    //   IsOpen = true,
    //   MsgFactory = (list) => $"New Flat: [\n{String.Join(",\n", list.Select(c => $"  {c}"))}\n]",
    // };

    public List<CUIVisualComponent> Flat { get; } = new();
    public void Flatten(CUIVisualComponent root)
    {
      Flat.Clear();

      void FlattenRec(CUIVisualComponent component)
      {
        Flat.Add(component);

        foreach (CUIVisualComponent child in component.StructuralSplit())
        {
          FlattenRec(child);
        }
      }

      FlattenRec(root);

      // Doesn't work 
      // Debug_LayoutFlatten.Send(Flat);

      // CUI.Logger.Log($"New Flat: [\n{String.Join(",\n", Flat.Select(c => $"  {c}"))}\n]");
    }
  }

}