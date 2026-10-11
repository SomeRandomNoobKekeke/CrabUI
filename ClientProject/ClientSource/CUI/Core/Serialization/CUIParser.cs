using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.IO;
using System.Xml.Linq;
using CUILibs;

namespace CursedUI
{
  public class CUIParser : Parser
  {

    public static string CUITexture2DToString(CUITexture2D texture) => texture.Key;
    public static CUITexture2D ParseCUITexture2D(string raw) => CUICore.TextureManager.Get(raw);

    public CUIParser()
    {
      Logger = CUI.Logger;

      Settings.ExtraParseMethods = DefaultExtraParseMethods;
      Settings.ExtraSerializeMethods = DefaultExtraSerializeMethods;

      Settings.ExtraParseMethods[typeof(CUITexture2D)] = ParseCUITexture2D;
      Settings.ExtraSerializeMethods[typeof(__CUITexture2D)] = (o) => CUITexture2DToString(o as CUITexture2D);
    }
  }
}
