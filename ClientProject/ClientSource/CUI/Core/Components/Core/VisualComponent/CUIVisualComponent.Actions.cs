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
  public partial class CUIVisualComponent
  {
    public void Do(string what, params object[] args)
    {
      if (Actions.ContainsKey(what))
      {
        Actions[what].DynamicInvoke(args);
      }
    }

    private Dictionary<string, Delegate> _Actions;
    public Dictionary<string, Delegate> Actions
    {
      get => _Actions ??= new();
      set => _Actions = value;
    }
  }
}