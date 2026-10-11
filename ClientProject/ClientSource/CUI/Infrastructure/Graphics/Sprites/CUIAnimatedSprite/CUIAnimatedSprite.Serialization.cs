using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using CUILibs;
using System.Collections;

namespace CursedUI
{
  public partial class CUIAnimatedSprite : CUIMultiSprite
  {
    public static new object Parse(string raw)
    {
      if (string.IsNullOrEmpty(raw)) return new CUIMultiSprite();

      Dictionary<string, string> dict = CUICore.Parser.SplitDict(raw);
      return Parse(dict);
    }

    public static object Parse(Dictionary<string, string> dict)
    {
      CUIAnimatedSprite sprite = new();

      if (dict.ContainsKey("sources"))
      {
        List<string> rawSourses = CUICore.Parser.SplitList(dict["sources"]);
        foreach (string rawSource in rawSourses)
        {
          sprite.Add(CUICore.Parser.Parse<ITextureSource>(rawSource));
        }
      }

      if (dict.ContainsKey("current"))
      {
        sprite.CurrentSource = CUICore.Parser.Parse<int>(dict["current"]);
      }

      if (dict.ContainsKey("msPerFrame"))
      {
        sprite.MsPerFrame = CUICore.Parser.Parse<double>(dict["msPerFrame"]);
      }

      if (dict.ContainsKey("isRunning"))
      {
        sprite.IsRunnung = CUICore.Parser.Parse<bool>(dict["isRunning"]);
      }

      CUI.Logger.Log(sprite.Animation);

      return sprite;
    }

    public override string ToText()
    {
      Dictionary<string, object> dict = new();

      dict["msPerFrame"] = MsPerFrame;
      if (CurrentSource != 0) dict["current"] = CurrentSource;
      if (IsRunnung == false) dict["isRunning"] = IsRunnung;

      dict["sources"] = Sources;

      return CUICore.Parser.Serialize(dict);
    }
  }
}