using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;
using Barotrauma.Extensions;
using CUILibs;

namespace CursedUI
{
  public partial class CUIRadioButton
  {
    public class RadioGroup
    {
      static RadioGroup()
      {
        PluginLifeCycle.Stop += static () => Groups = null;
      }

      public static Dictionary<string, RadioGroup> Groups { get; private set; } = new();

      public static RadioGroup GetOrCreate(string name)
      {
        if (!Groups.ContainsKey(name)) Groups[name] = new(name);
        return Groups[name];
      }

      public string Name { get; }

      public CUIRadioButton Previous { get; private set; }
      public CUIRadioButton Current { get; private set; }

      public event Action<CUIRadioButton> Selected;

      public void Select(CUIRadioButton btn)
      {
        if (btn == Current) return;

        Previous = Current;
        Current = btn;
        Selected?.Invoke(btn);

        Previous?.HandleDeselect();
        Current?.HandleSelect();
      }

      public void SelectSilent(CUIRadioButton btn)
      {
        if (btn == Current) return;

        Previous = Current;
        Current = btn;
      }

      public void Deselect(CUIRadioButton btn)
      {
        if (!IsSelected(btn)) return;
        Select(null);
      }

      public void DeselectSilent(CUIRadioButton btn)
      {
        if (!IsSelected(btn)) return;
        SelectSilent(null);
      }

      public void ClearSelection()
      {
        Select(null);
      }

      public bool IsSelected(CUIRadioButton btn) => Current == btn;
      public bool WasSelected(CUIRadioButton btn) => Previous == btn;

      private RadioGroup(string name)
      {
        Name = name;
        if (!Groups.ContainsKey(name)) Groups[name] = this;
      }
    }
  }
}