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
namespace CursedUI
{
  public interface ITextureSource : IParsable
  {
    public CUITexture2D Texture { get; }
    public Rectangle? SourceRectangle { get; }

    bool IParsable.WrapInQuotes => false;
    static object IParsable.Parse(string raw) => Parse(raw); // Reimplemented!
    public static new object Parse(string raw)
    {
      if (string.IsNullOrEmpty(raw)) return new CUISprite();

      raw = raw.Trim();
      if (!raw.StartsWith('{'))
      {
        return CUISprite.GetByName(raw);
      }

      Dictionary<string, string> dict = CUICore.Parser.SplitDict(raw);

      TextureSource source = new TextureSource();

      if (dict.ContainsKey("texture")) source.Texture = CUICore.TextureManager.Get(dict["texture"]);
      if (dict.ContainsKey("sourcerect"))
      {
        source.SourceRectangle = CUICore.Parser.Parse<Rectangle?>(dict["sourcerect"]);
      }

      return source;
    }

  }
}