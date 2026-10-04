using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
using System.Text;

namespace CursedUI
{
  public partial class CUIMainComponent
  {
    public CUIDebugNode Debug_LayoutUpdated = new(DebugCategory.LayoutUpdated) { IsOpen = true };


    [InitMethod]
    protected override void InitDebugChannels()//CRINGE i have no idea why this works
    {
      // base.InitDebugChannels(); 
      Debug_LayoutUpdated.Map(DebugRelays[DebugCategory.LayoutUpdated]);
    }

    public new DebugRelayDict DebugRelays { get; } = new()
    {
      [DebugCategory.LayoutUpdated] = new DebugRelay(),
      [DebugCategory.LayoutMarked] = new DebugRelay(),
      [DebugCategory.LayoutCalculations] = new DebugRelay(),
      [DebugCategory.RectSet] = new DebugRelay(),
    };

    public void PrintFlatLayout()
    {
      CUI.Logger.Log($"{this}:");
      CUI.Logger.Log($"{Logger.Wrap.IEnumerable(LayoutFlattener.Flat, true)}");
    }

    public void PrintFlatVisual()
    {
      CUI.Logger.Log($"{this}:");

      string createOffset(int d)
      {
        StringBuilder sb = new();

        for (int i = 0; i < d; i++)
        {
          sb.Append("|      ");
        }
        return sb.ToString();
      }


      int depth = 0;
      string offset = "";
      foreach (VisualUnit vu in VisualFlattener.Flat)
      {
        if (vu is VisualBounds.RightContextBound)
        {
          depth--;
          offset = createOffset(depth);
        }

        if (vu is VisualBounds.LeftContextBound || vu is VisualBounds.RightContextBound)
        {
          CUI.Logger.Log($"{offset}|{Logger.WrapInColor(vu, "white")}");
        }
        else
        {
          CUI.Logger.Log($"{offset}|{vu}");
        }


        if (vu is VisualBounds.LeftContextBound)
        {
          depth++;
          offset = createOffset(depth);
        }
      }
    }
  }
}