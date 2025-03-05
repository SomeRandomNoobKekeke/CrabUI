using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{
  public enum PaletteOrder
  {
    Primary, Secondary, Tertiary, Quaternary
  }
  public record PaletteExtractResult(bool Ok, string Value = null);
  /// <summary>
  /// Contains abstract values that could be referenced in Styles
  /// </summary>
  public class CUIPalette
  {
    internal static void InitStatic()
    {
      CUI.OnInit += () =>
      {
        Initialize();
      };
      CUI.OnDispose += () =>
      {
        LoadedPalettes.Clear();
      };
    }

    public override string ToString() => $"CUIPalette {Name}";
    public static string PaletteSetsPath => Path.Combine(CUI.PalettesPath, "Sets");
    public static string DefaultPalette = "Blue";


    //TODO why is it here? how could sane person find these?
    public static bool NotifyExcessivePropStyles { get; set; } = false;
    public static bool NotifiMissingPropStyles { get; set; } = true;

    public static PaletteExtractResult Extract(string nestedName, PaletteOrder order)
    {
      CUIPalette palette = order switch
      {
        PaletteOrder.Primary => Primary,
        PaletteOrder.Secondary => Secondary,
        PaletteOrder.Tertiary => Tertiary,
        PaletteOrder.Quaternary => Quaternary,
        _ => Empty,
      };
      if (!palette.Values.ContainsKey(nestedName)) return new PaletteExtractResult(false);
      return new PaletteExtractResult(true, palette.Values[nestedName]);
    }

    public static CUIPalette Empty => new CUIPalette();

    public static Dictionary<string, CUIPalette> LoadedPalettes = new();
    public static string Default = "Blue";

    private static CUIPalette primary = new CUIPalette();
    public static CUIPalette Primary
    {
      get => primary;
      set
      {
        if (value == null) return;
        primary = value;
        CUIGlobalStyleResolver.OnPaletteChange(primary);
      }
    }

    private static CUIPalette secondary = new CUIPalette();
    public static CUIPalette Secondary
    {
      get => secondary;
      set
      {
        if (value == null) return;
        secondary = value;
        CUIGlobalStyleResolver.OnPaletteChange(secondary);
      }
    }

    private static CUIPalette tertiary = new CUIPalette();
    public static CUIPalette Tertiary
    {
      get => tertiary;
      set
      {
        if (value == null) return;
        tertiary = value;
        CUIGlobalStyleResolver.OnPaletteChange(tertiary);
      }
    }

    private static CUIPalette quaternary = new CUIPalette();
    public static CUIPalette Quaternary
    {
      get => quaternary;
      set
      {
        if (value == null) return;
        quaternary = value;
        CUIGlobalStyleResolver.OnPaletteChange(quaternary);
      }
    }


    public Dictionary<string, string> Values = new();
    public string Name = "???";
    public string BaseColor { get; set; } = "";



    public static void Initialize()
    {
      Stopwatch sw = Stopwatch.StartNew();
      if (CUI.PalettesPath == null) return;

      LoadedPalettes.Clear();

      LoadPalettes();

      LoadSet(Path.Combine(PaletteSetsPath, DefaultPalette + ".xml"));
      // Primary = LoadedPalettes.GetValueOrDefault("red");
      // Secondary = LoadedPalettes.GetValueOrDefault("purple");
      // Tertiary = LoadedPalettes.GetValueOrDefault("blue");
      // Quaternary = LoadedPalettes.GetValueOrDefault("cyan");

      CUIDebug.Log($"CUIPalette.Initialize took {sw.ElapsedMilliseconds}ms");
    }

    public static void LoadPalettes()
    {
      foreach (string file in Directory.GetFiles(CUI.PalettesPath, "*.xml"))
      {
        CUIPalette palette = LoadFrom(file);
        LoadedPalettes[palette.Name] = palette;
      }
    }

    public static CUIPalette FromXML(XElement root)
    {
      CUIPalette palette = new CUIPalette();

      palette.Name = root.Attribute("Name")?.Value.ToString();

      foreach (XElement element in root.Elements())
      {
        foreach (XAttribute attribute in element.Attributes())
        {
          palette.Values[$"{element.Name}.{attribute.Name}"] = attribute.Value;
        }

        if (element.Value != "")
        {
          palette.Values[$"{element.Name}"] = element.Value;
        }
      }

      return palette;
    }

    public static CUIPalette LoadFrom(string path)
    {
      CUIPalette palette = new CUIPalette();
      try
      {
        XDocument xdoc = XDocument.Load(path);
        XElement root = xdoc.Root;

        palette = CUIPalette.FromXML(root);
        palette.Name ??= Path.GetFileNameWithoutExtension(path);
      }
      catch (Exception e)
      {
        CUI.Warning($"Failed to load palette from {path}");
        CUI.Warning(e);
      }

      return palette;
    }


    public XElement ToXML()
    {
      XElement root = new XElement("Palette");
      root.Add(new XAttribute("Name", Name));
      root.Add(new XAttribute("BaseColor", BaseColor));

      foreach (string key in Values.Keys)
      {
        string component = key.Split('.').ElementAtOrDefault(0);
        string prop = key.Split('.').ElementAtOrDefault(1);

        if (component == null) continue;

        if (root.Element(component) == null) root.Add(new XElement(component));

        if (prop != null)
        {
          root.Element(component).Add(new XAttribute(prop, Values[key]));
        }
        else
        {
          root.Element(component).Value = Values[key];
        }
      }
      return root;
    }
    public void SaveTo(string path)
    {
      try
      {
        XDocument xdoc = new XDocument();
        xdoc.Add(this.ToXML());
        xdoc.Save(path);
      }
      catch (Exception e)
      {
        CUI.Warning($"Failed to save palette to {path}");
        CUI.Warning(e);
      }
    }


    public static void PaletteDemo()
    {
      if (CUI.AssetsPath == null)
      {
        CUI.Warning($"Can't load PaletteDemo, CUI.AssetsPath is null");
        return;
      }

      void loadFrame(Vector2 offset, PaletteOrder order)
      {
        CUIFrame frame = CUIComponent.LoadFromFile<CUIFrame>(Path.Combine(CUI.AssetsPath, $"PaletteDemo.xml"));
        frame.DeepPalette = order;
        frame.Absolute = frame.Absolute with { Position = offset };
        frame.AddCommand("Close", (o) => frame.RemoveSelf());
        CUI.TopMain.Append(frame);
      }

      loadFrame(new Vector2(0, 0), PaletteOrder.Primary);
      loadFrame(new Vector2(180, 0), PaletteOrder.Secondary);
      loadFrame(new Vector2(360, 0), PaletteOrder.Tertiary);
      loadFrame(new Vector2(540, 0), PaletteOrder.Quaternary);
    }




    public static CUIPalette CreatePaletteFromColors(string name, Color colorA, Color? colorb = null)
    {
      CUIPalette palette = new CUIPalette()
      {
        Name = name,
        BaseColor = CUIExtensions.ColorToString(colorA),
      };

      Color colorB = colorb ?? Color.Black;

      Dictionary<string, Color> colors = new();

      colors["Frame.Background"] = colorA.To(colorB, 1.0f);
      colors["Header.Background"] = colorA.To(colorB, 0.7f);
      colors["Nav.Background"] = colorA.To(colorB, 0.8f);
      colors["Main.Background"] = colorA.To(colorB, 0.9f);

      colors["Frame.Border"] = colorA.To(colorB, 0.5f);
      colors["Header.Border"] = colorA.To(colorB, 0.6f);
      colors["Nav.Border"] = colorA.To(colorB, 0.7f);
      colors["Main.Border"] = colorA.To(colorB, 0.8f);

      colors["Frame.Text"] = colorA.To(Color.White, 0.9f);
      colors["Header.Text"] = colorA.To(Color.White, 0.9f);
      colors["Nav.Text"] = colorA.To(Color.White, 0.8f);
      colors["Main.Text"] = colorA.To(Color.White, 0.8f);

      colors["Component.Background"] = Color.Transparent;
      colors["Component.Border"] = Color.Transparent;
      colors["Component.Text"] = colors["Main.Text"];
      colors["Button.Background"] = colorA.To(colorB, 0.0f);
      colors["Button.Border"] = colorA.To(colorB, 0.5f);
      colors["Button.Disabled"] = colorA.To(new Color(16, 16, 16), 0.8f);
      colors["CloseButton.Background"] = colorA.To(Color.White, 0.2f);
      colors["DDOption.Background"] = colors["Header.Background"];
      colors["DDOption.Border"] = colors["Main.Border"];
      colors["DDOption.Hover"] = colorA.To(colorB, 0.5f);
      colors["DDOption.Text"] = colors["Main.Text"];
      colors["Handle.Background"] = colorA.To(colorB, 0.5f).To(Color.White, 0.2f);
      colors["Handle.Grabbed"] = colorA.To(colorB, 0.0f).To(Color.White, 0.2f);
      colors["Slider"] = colorA.To(Color.White, 0.7f);
      colors["Input.Background"] = colors["Nav.Background"];
      colors["Input.Border"] = colors["Nav.Border"];
      colors["Input.Text"] = colors["Main.Text"];
      colors["Input.Focused"] = colorA;
      colors["Input.Invalid"] = Color.Red;
      colors["Input.Valid"] = Color.Lime;
      colors["Input.Selection"] = colorA.To(Color.White, 0.7f) * 0.5f;
      colors["Input.Caret"] = colorA.To(Color.White, 0.7f) * 0.5f;

      foreach (var (key, cl) in colors)
      {
        palette.Values[key] = CUIExtensions.ColorToString(cl);
      }

      palette.SaveTo(Path.Combine(CUI.PalettesPath, $"{name}.xml"));
      LoadedPalettes[name] = palette;
      CUI.Log($"Created {name} palette and saved it to {Path.Combine(CUI.PalettesPath, $"{name}.xml")}");

      return palette;
    }

    /// <summary>
    /// Packs 4 palettes into 1 set
    /// </summary>
    /// <param name="setName"></param>
    /// <param name="primary"></param>
    /// <param name="secondary"></param>
    /// <param name="tertiary"></param>
    /// <param name="quaternary"></param>
    public static void SaveSet(string setName, string primary = "", string secondary = "", string tertiary = "", string quaternary = "")
    {
      if (setName == null || setName == "") return;

      string savePath = Path.Combine(PaletteSetsPath, $"{setName}.xml");

      try
      {
        XDocument xdoc = new XDocument(new XElement("PaletteSet"));
        XElement root = xdoc.Root;

        root.Add(new XAttribute("Name", setName));

        root.Add((LoadedPalettes.GetValueOrDefault(primary ?? "") ?? Primary).ToXML());
        root.Add((LoadedPalettes.GetValueOrDefault(secondary ?? "") ?? Secondary).ToXML());
        root.Add((LoadedPalettes.GetValueOrDefault(tertiary ?? "") ?? Tertiary).ToXML());
        root.Add((LoadedPalettes.GetValueOrDefault(quaternary ?? "") ?? Quaternary).ToXML());

        xdoc.Save(savePath);

        CUI.Log($"Created {setName} palette set and saved it to {savePath}");
        LoadSet(savePath);
      }
      catch (Exception e)
      {
        CUI.Warning($"Failed to save palette set to {savePath}");
        CUI.Warning(e);
      }
    }

    public static void LoadSet(string path)
    {
      if (path == null || path == "") return;
      try
      {
        XDocument xdoc = XDocument.Load(path);
        XElement root = xdoc.Root;

        List<CUIPalette> palettes = new();

        foreach (XElement element in root.Elements("Palette"))
        {
          palettes.Add(CUIPalette.FromXML(element));
        }

        Primary = palettes.ElementAtOrDefault(0) ?? Empty;
        Secondary = palettes.ElementAtOrDefault(1) ?? Empty;
        Tertiary = palettes.ElementAtOrDefault(2) ?? Empty;
        Quaternary = palettes.ElementAtOrDefault(3) ?? Empty;
      }
      catch (Exception e)
      {
        CUI.Warning($"Failed to load palette set from {path}");
        CUI.Warning(e);
      }
    }

  }


}