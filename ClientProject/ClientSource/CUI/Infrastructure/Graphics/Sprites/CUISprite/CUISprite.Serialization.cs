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
  public partial class CUISprite : IParsable
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
      if (string.IsNullOrEmpty(raw)) return new CUISprite();

      raw = raw.Trim();
      if (!raw.StartsWith('{'))
      {
        return GetByName(raw);
      }

      Dictionary<string, string> dict = CUICore.Parser.SplitDict(raw);

      if (dict.ContainsKey("sources")) return CUIMultiSprite.Parse(dict);

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
      if (dict.ContainsKey("drawmode")) sprite.DrawMode = CUICore.Parser.Parse<CUISpriteDrawMode>(dict["drawmode"]);

      return sprite;
    }

    public virtual string ToText()
    {
      if (Name is not null && Equals(GetByName(Name))) return Name;

      Dictionary<string, object> dict = new()
      {
        ["texture"] = Texture.Key ?? "",
      };

      if (ColorTL == ColorTR && ColorTL == ColorBL && ColorTL == ColorBR)
      {
        if (Color != DefaultColor) dict["color"] = Color;
      }
      else
      {
        if (ColorTL != DefaultColor) dict["colorTL"] = ColorTL;
        if (ColorTR != DefaultColor) dict["colorTR"] = ColorTR;
        if (ColorBR != DefaultColor) dict["colorBR"] = ColorBR;
        if (ColorBL != DefaultColor) dict["colorBL"] = ColorBL;
      }

      if (SourceRectangle != null) dict["sourcerect"] = SourceRectangle;
      if (Rotation != 0) dict["rotation"] = Rotation;
      if (Origin != Vector2.Zero) dict["origin"] = Origin;
      if (Effects != SpriteEffects.None) dict["effects"] = Effects;
      if (LayerDepth != 0) dict["layerdepth"] = LayerDepth;
      if (DrawMode != CUISpriteDrawMode.Resize) dict["drawmode"] = DrawMode;

      return CUICore.Parser.Serialize(dict);
    }
  }
}