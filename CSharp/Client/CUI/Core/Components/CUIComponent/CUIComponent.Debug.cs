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
    public Debug_Part Debug { get; } = new();
    public class Debug_Part : Part
    {
      public void PrintTree(string offset = "")
      {
        CUI.Logger.Log($"{offset}{Self}");
        foreach (CUIComponent child in Self.Children)
        {
          child.Debug.PrintTree(offset + "|    ");
        }
      }
    }
  }
}