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
    private bool _Debug; public bool Debug
    {
      get => _Debug;
      set
      {
        _Debug = value;

        if (value) DebugRelays.Open(); else DebugRelays.Close();
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
  }
}