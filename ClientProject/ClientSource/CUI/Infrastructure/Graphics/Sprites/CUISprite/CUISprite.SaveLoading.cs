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
  public partial record CUISprite : IParsable
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

    //TODO, for now i decided to make outer VisualUnit parsable instead
    public static object Parse(string raw)
    {
      Dictionary<string, string> dict = JsonSerializer.Deserialize<Dictionary<string, string>>(raw);

      CUI.Logger.Log(Logger.Wrap.IDictionary(dict));

      CUISprite sprite = new CUISprite();

      if (dict.ContainsKey("texture")) sprite.Texture = CUICore.TextureManager.Get(dict["texture"]);
      if (dict.ContainsKey("color")) sprite.Color = CUICore.Parser.Parse<Color>(dict["color"]);

      return sprite;
    }

    public string ToText()
    {
      Dictionary<string, string> dict = new Dictionary<string, string>()
      {
        ["texture"] = Texture.Key ?? "",
        ["color"] = CUICore.Parser.Serialize(Color),
      };

      return JsonSerializer.Serialize(dict);
    }
  }
}