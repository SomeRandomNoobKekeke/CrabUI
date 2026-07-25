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
    private Dictionary<string, CUIDictStyle> MemorizedStates { get; } = new();

    public void SaveState(string name)
    {
      MemorizedStates[name] = CUIDictStyle.FromComponent($"state [{name}]", this);
    }

    public void RestoreState(string name)
    {
      if (!MemorizedStates.ContainsKey(name)) return;
      MemorizedStates[name].Apply(this);
    }
  }
}