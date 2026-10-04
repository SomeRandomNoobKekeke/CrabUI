using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;
using Microsoft.Xna.Framework;
namespace CursedUI
{

  public partial class CUICore : IComponent
  {
    // public static Dictionary<int, WeakReference<CUIVisualComponent>> ComponentsById = new();
    // public static IEnumerable<CUIVisualComponent?> AllComponents => ComponentsById.Values
    //   .Select(wr =>
    //   {
    //     if (wr.TryGetTarget(out CUIVisualComponent component))
    //     {
    //       return component;
    //     }

    //     return null;
    //   }).Where(c => c != null);


    private static bool _DummyMode; public static bool DummyMode
    {
      get => _DummyMode;
      set
      {
        _DummyMode = value;
        TextureManager.DummyMode = value;
      }
    }




    public static int MaxID { get; private set; }
    public static int GetID() => MaxID++;

    // IDK, like why making it harder to debug?
    // public static int GetID() => DummyMode ? -1 : MaxID++;
  }
}