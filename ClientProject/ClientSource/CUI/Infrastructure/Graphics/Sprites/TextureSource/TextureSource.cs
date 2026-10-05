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
  public class TextureSource : ITextureSource
  {
    public CUITexture2D Texture { get; set; }
    public Rectangle? SourceRectangle { get; set; }

    public TextureSource() { }
    public TextureSource(CUITexture2D texture, Rectangle? sourceRect)
    {
      Texture = texture;
      SourceRectangle = sourceRect;
    }

    public static object Parse(string raw)
    {
      raw = raw.Replace('\'', '"');
      Dictionary<string, string> dict = JsonSerializer.Deserialize<Dictionary<string, string>>(raw);

      TextureSource source = new TextureSource();

      if (dict.ContainsKey("texture")) source.Texture = CUICore.TextureManager.Get(dict["texture"]);
      if (dict.ContainsKey("sourcerect"))
      {
        source.SourceRectangle = CUICore.Parser.Parse<Rectangle?>(dict["sourcerect"]);
      }

      return source;
    }

    public string ToText()
    {


      Dictionary<string, string> dict = new Dictionary<string, string>()
      {
        ["texture"] = Texture.Key ?? "",
      };

      if (SourceRectangle != null) dict["sourcerect"] = CUICore.Parser.Serialize(SourceRectangle);

      return JsonSerializer.Serialize(dict).Replace('"', '\'');
    }
  }
}