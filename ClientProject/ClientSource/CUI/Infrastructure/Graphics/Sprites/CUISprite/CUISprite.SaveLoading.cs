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
  public partial class CUISprite : ICustomSerializable<CUISprite>
  {
    public static CUISprite Get(string key)
    {
      CUICore.ResourceIOContext.CallingAssembly = Assembly.GetCallingAssembly();
      CUISprite sprite = new CUISprite(CUICore.TextureManager.Get(key));
      CUICore.ResourceIOContext.CallingAssembly = null;
      return sprite;
    }

    public static CUISprite LoadAs(string path, string key)
    {
      CUICore.ResourceIOContext.CallingAssembly = Assembly.GetCallingAssembly();
      CUISprite sprite = new CUISprite(CUICore.TextureManager.LoadAs(path, key));
      CUICore.ResourceIOContext.CallingAssembly = null;
      return sprite;
    }

    public static object Parse(string raw)
    {
      raw = raw.Trim();
      if (!raw.StartsWith('{'))
      {
        return GetByName(raw);
      }

      raw = raw.Replace('\'', '"');
      Dictionary<string, string> dict = JsonSerializer.Deserialize<Dictionary<string, string>>(raw);

      CUISprite sprite = new CUISprite();

      if (dict.ContainsKey("texture")) sprite.Texture = CUICore.TextureManager.Get(dict["texture"]);
      if (dict.ContainsKey("colorTL")) sprite.ColorTL = CUICore.Parser.Parse<Color>(dict["colorTL"]);
      if (dict.ContainsKey("colorTR")) sprite.ColorTR = CUICore.Parser.Parse<Color>(dict["colorTR"]);
      if (dict.ContainsKey("colorBR")) sprite.ColorBR = CUICore.Parser.Parse<Color>(dict["colorBR"]);
      if (dict.ContainsKey("colorBL")) sprite.ColorBL = CUICore.Parser.Parse<Color>(dict["colorBL"]);
      if (dict.ContainsKey("color")) sprite.Color = CUICore.Parser.Parse<Color>(dict["color"]);
      if (dict.ContainsKey("sourcerect"))
      {
        sprite.SourceRectangle = CUICore.Parser.Parse<Rectangle?>(dict["sourcerect"]);
      }

      if (dict.ContainsKey("rotation")) sprite.Rotation = CUICore.Parser.Parse<float>(dict["rotation"]);
      if (dict.ContainsKey("origin")) sprite.Origin = CUICore.Parser.Parse<Vector2>(dict["origin"]);
      if (dict.ContainsKey("effects")) sprite.Effects = CUICore.Parser.Parse<SpriteEffects>(dict["effects"]);
      if (dict.ContainsKey("layerdepth")) sprite.LayerDepth = CUICore.Parser.Parse<float>(dict["layerdepth"]);

      return sprite;
    }

    //TODO mb don't use json, it uses "" and they are not allowed in xml
    public string ToText()
    {
      if (Name is not null && Equals(GetByName(Name))) return Name;

      Dictionary<string, string> dict = new Dictionary<string, string>()
      {
        ["texture"] = Texture.Key ?? "",
      };

      if (ColorTL == ColorTR && ColorTL == ColorBL && ColorTL == ColorBR)
      {
        if (Color != DefaultColor) dict["color"] = CUICore.Parser.Serialize(Color);
      }
      else
      {
        if (ColorTL != DefaultColor) dict["colorTL"] = CUICore.Parser.Serialize(ColorTL);
        if (ColorTR != DefaultColor) dict["colorTR"] = CUICore.Parser.Serialize(ColorTR);
        if (ColorBR != DefaultColor) dict["colorBR"] = CUICore.Parser.Serialize(ColorBR);
        if (ColorBL != DefaultColor) dict["colorBL"] = CUICore.Parser.Serialize(ColorBL);
      }

      if (SourceRectangle != null) dict["sourcerect"] = CUICore.Parser.Serialize(SourceRectangle);
      if (Rotation != 0) dict["rotation"] = CUICore.Parser.Serialize(Rotation);
      if (Origin != Vector2.Zero) dict["origin"] = CUICore.Parser.Serialize(Origin);
      if (Effects != SpriteEffects.None) dict["effects"] = CUICore.Parser.Serialize(Effects);
      if (LayerDepth != 0) dict["layerdepth"] = CUICore.Parser.Serialize(LayerDepth);

      return JsonSerializer.Serialize(dict).Replace('"', '\'');
    }

    public string ToText(CUISprite defValue)
    {
      //TODO is this too slow?
      if (Name is not null && Equals(GetByName(Name))) return Name;

      if (defValue is null) return ToText();

      Dictionary<string, string> dict = new();

      if (Texture != defValue.Texture)
      {
        dict["texture"] = Texture.Key;
      }

      if (SourceRectangle != defValue.SourceRectangle)
      {
        dict["sourcerect"] = CUICore.Parser.Serialize(SourceRectangle);
      }

      if (Rotation != defValue.Rotation)
      {
        dict["rotation"] = CUICore.Parser.Serialize(Rotation);
      }

      if (Origin != defValue.Origin)
      {
        dict["origin"] = CUICore.Parser.Serialize(Origin);
      }

      if (Effects != defValue.Effects)
      {
        dict["effects"] = CUICore.Parser.Serialize(Effects);
      }

      if (LayerDepth != defValue.LayerDepth)
      {
        dict["layerdepth"] = CUICore.Parser.Serialize(LayerDepth);
      }

      if (ColorTL == ColorTR && ColorTL == ColorBL && ColorTL == ColorBR)
      {
        if (Color != defValue.Color)
        {
          dict["color"] = CUICore.Parser.Serialize(Color);
        }
      }
      else
      {
        if (ColorTL != defValue.ColorTL)
        {
          dict["colorTL"] = CUICore.Parser.Serialize(ColorTL);
        }
        if (ColorTR != defValue.ColorTR)
        {
          dict["colorTR"] = CUICore.Parser.Serialize(ColorTR);
        }
        if (ColorBR != defValue.ColorBR)
        {
          dict["colorBR"] = CUICore.Parser.Serialize(ColorBR);
        }
        if (ColorBL != defValue.ColorBL)
        {
          dict["colorBL"] = CUICore.Parser.Serialize(ColorBL);
        }
      }

      return JsonSerializer.Serialize(dict).Replace('"', '\'');
    }
  }
}