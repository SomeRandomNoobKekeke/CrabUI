using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace CrabUI
{


  public partial class __CUITextureManager
  {
    public bool Debug { get; set; } = true;

    private Dictionary<string, StackTrace> Traces = new();

    public void PrintTrace(string key)
    {
      if (!Traces.ContainsKey(key))
      {
        CUI.Logger.Log($"no trace of [{key}]");
        return;
      }

      CUI.Logger.Log(Traces[key]);
    }

    private void MemorizeStackTrace(string key)
    {
      if (Debug)
      {
        Traces[key] = new StackTrace(1);
      }
    }
  }
}