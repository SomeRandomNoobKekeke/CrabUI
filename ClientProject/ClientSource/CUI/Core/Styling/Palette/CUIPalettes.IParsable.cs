using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  //TODO palette serialization is cringe and wip
  public partial class CUIPalettes : IParsable
  {
    static object IParsable.Parse(string raw) => Parse(raw);
    public static CUIPalettes Parse(string raw)
    {
      CUIPalettes palettes = new();

      Dictionary<string, string> dict = CUICore.Parser.SplitDict(raw);

      foreach (var (name, palette) in palettes.All)
      {
        if (dict.ContainsKey(name))
        {
          palette.Swap(CUICore.Parser.Parse<CUIPalette>(dict[name]));
        }
      }

      return palettes;
    }


    public string ToText()
    {
      return CUICore.Parser.Serialize(All);
    }
  }
}