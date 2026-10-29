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
  public partial class CUIMultiSprite
  {

    public static new object Parse(string raw)
    {
      if (string.IsNullOrEmpty(raw)) return new CUIMultiSprite();
      Dictionary<string, string> dict = CUICore.Parser.SplitDict(raw);

      return Parse(dict);
    }

    public static object Parse(Dictionary<string, string> dict)
    {
      if (dict.ContainsKey("msPerFrame")) return CUIAnimatedSprite.Parse(dict);

      CUIMultiSprite sprite = new();

      if (dict.ContainsKey("sources"))
      {
        IEnumerable<string> rawSourses = CUICore.Parser.Parse<IEnumerable<string>>(dict["sources"]);
        foreach (string rawSource in rawSourses)
        {
          sprite.Add(CUICore.Parser.Parse<ITextureSource>(rawSource));
        }
      }

      if (dict.ContainsKey("current"))
      {
        sprite.CurrentSource = CUICore.Parser.Parse<int>(dict["current"]);
      }

      return sprite;
    }

    public override string ToText()
    {
      Dictionary<string, object> dict = new();

      dict["sources"] = Sources;
      if (CurrentSource != 0) dict["current"] = CurrentSource;

      return CUICore.Parser.Serialize(dict);
    }
  }
}