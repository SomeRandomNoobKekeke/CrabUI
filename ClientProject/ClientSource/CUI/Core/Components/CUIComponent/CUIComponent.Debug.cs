using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;
namespace CrabUI
{
  public partial class CUIComponent
  {
    protected event Action OnDebugOn; //CRINGE
    protected event Action OnDebugOff;//CRINGE

    private bool _IsDebugTool; public bool IsDebugTool
    {
      get => _IsDebugTool;
      set
      {
        _IsDebugTool = value;
        foreach (CUIComponent child in Tree.Children)
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

        if (value) OnDebugOn?.Invoke(); else OnDebugOff?.Invoke();
      }
    }

    public bool DeepDebug
    {
      get => Debug;
      set
      {
        if (IsDebugTool) return;

        Debug = value;

        foreach (CUIComponent child in Tree.Children)
        {
          child.DeepDebug = value;
        }
      }
    }


    public void PrintTree(string offset = "")
    {
      CUI.Logger.Log($"{offset}{this}");
      foreach (CUIComponent child in this.Tree.Children)
      {
        child.PrintTree(offset + "|    ");
      }
    }

    public void PrintVisualSplit()
    {
      VisualFlattener flattener = new VisualFlattener();
      flattener.Flatten(this);
      CUI.Logger.Log(Logger.Wrap.IEnumerable(flattener.Flat, true));
    }
  }
}