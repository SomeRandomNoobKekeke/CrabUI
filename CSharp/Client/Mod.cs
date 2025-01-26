using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;
using CrabUI;

namespace SomeNamespace
{
  public partial class Mod : IAssemblyPlugin
  {
    public void Initialize()
    {
      CUI.Initialize();

      CUIFrame frame = new CUIFrame()
      {
        Relative = new CUINullRect(w: 0.2f, h: 0.2f),
        Anchor = CUIAnchor.Center,
      };

      frame["guh button"] = new CUIButton("Press me")
      {
        Anchor = CUIAnchor.Center,
        AddOnMouseDown = (e) => CUI.Log("Guh"),
      };

      CUI.Main.Append(frame);
    }

    public void OnLoadCompleted() { }
    public void PreInitPatching() { }

    public void Dispose()
    {
      CUI.Dispose();
    }

    public static void Log(object msg, Color? cl = null)
    {
      cl ??= Color.Cyan;
      LuaCsLogger.LogMessage($"{msg ?? "null"}", cl * 0.8f, cl);
    }
  }
}