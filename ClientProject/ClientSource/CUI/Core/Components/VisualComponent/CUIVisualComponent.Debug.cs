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
namespace CrabUI
{
  public partial class CUIVisualComponent
  {
    public virtual void OnDebugOn() { }
    public virtual void OnDebugOff() { }

    private bool _IsDebugTool; public bool IsDebugTool
    {
      get => _IsDebugTool;
      set
      {
        _IsDebugTool = value;
        foreach (CUIVisualComponent child in Children)
        {
          child.IsDebugTool = value;
        }
      }
    }


    private bool _Debug; public bool Debug
    {
      get => _Debug;
      set
      {
        if (IsDebugTool) return;
        _Debug = value;

        if (value) OnDebugOn(); else OnDebugOff();
      }
    }

    public bool DeepDebug
    {
      get => Debug;
      set
      {
        if (IsDebugTool) return;

        Debug = value;

        foreach (CUIVisualComponent child in Children)
        {
          child.DeepDebug = value;
        }
      }
    }


    public void PrintTree(string offset = "")
    {
      CUI.Logger.Log($"{offset}{this}");
      foreach (CUIVisualComponent child in Children)
      {
        child.PrintTree(offset + "|    ");
      }
    }

    public void PrintVisualSplit()
    {
      VisualFlattener flattener = new VisualFlattener();
      flattener.Flatten(this);
      CUI.Logger.Log(Logger.Wrap.IEnumerable(
        flattener.Flat.Select(vu => $"{vu} of {this}")
        , true));
    }
  }
}