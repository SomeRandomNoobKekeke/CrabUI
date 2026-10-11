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

  public partial class CUICore
  {
    public static Parser Parser => Instance._Parser;
    private Parser _Parser = new()
    {
      // // Logger = CUI.Logger,
      // Settings =
      // {
      //   ExtraParseMethods =
      //   {
      //     [typeof(CUITexture2D)] = (key)=>TextureManager.Get(key),
      //   },
      //   ExtraSerializeMethods =
      //   {
      //     [typeof(__CUITexture2D)] = (o) => ((CUITexture2D)o).Key,
      //   }
      // }
    };

  }
}