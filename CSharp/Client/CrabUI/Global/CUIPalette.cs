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
  public enum PaletteOrder { Primary, Secondary, Tertiary, Quaternary }

  public record PaletteExtractResult(bool Ok, string Value = null);
  /// <summary>
  /// Contains abstract values that could be referenced in Styles
  /// </summary>
  public class CUIPalette
  {
    internal static void InitStatic()
    {
      CUI.OnInit += () => Initialize();
      CUI.OnDispose += () => LoadedPalettes.Clear();
    }

    public override string ToString() => $"CUIPalette {Name}";
    public static string PaletteSetsPath => Path.Combine(CUI.PalettesPath, "Sets");
    /// <summary>
    /// This palette set will be loadead on CUI.Initialize
    /// </summary>
    public static string DefaultPalette = "Blue";

    //TODO why are they here? how could sane person find these?
    /// <summary>
    /// Notifies when you try to set a style to a prop that doesn't exist
    /// </summary>
    public static bool NotifyExcessivePropStyles { get; set; } = false;
    /// <summary>
    /// Notifies when prop asks for style that is missing in palette
    /// </summary>
    public static bool NotifiMissingPropStyles { get; set; } = true;

    public Dictionary<string, string> Values = BackupPalette;

    public string Name = "???";
    public string BaseColor { get; set; } = "";

    public static Dictionary<string, CUIPalette> LoadedPalettes = new();
    public static CUIPalette Empty => new CUIPalette();
    public static Dictionary<string, string> BackupPalette => new Dictionary<string, string>()
    {
      ["Frame.Background"] = "0,0,0,200",
      ["Frame.Border"] = "0,0,127,227",
      ["Frame.Text"] = "229,229,255,255",
      ["Header.Background"] = "0,0,76,216",
      ["Header.Border"] = "0,0,102,222",
      ["Header.Text"] = "229,229,255,255",
      ["Nav.Background"] = "0,0,51,211",
      ["Nav.Border"] = "0,0,76,216",
      ["Nav.Text"] = "204,204,255,255",
      ["Main.Background"] = "0,0,25,205",
      ["Main.Border"] = "0,0,51,211",
      ["Main.Text"] = "204,204,255,255",
      ["Component.Background"] = "0,0,0,0",
      ["Component.Border"] = "0,0,0,0",
      ["Component.Text"] = "204,204,255,255",
      ["Button.Background"] = "0,0,255,255",
      ["Button.Border"] = "0,0,127,227",
      ["Button.Disabled"] = "12,12,63,255",
      ["CloseButton.Background"] = "51,51,255,255",
      ["DDOption.Background"] = "0,0,76,216",
      ["DDOption.Border"] = "0,0,51,211",
      ["DDOption.Hover"] = "0,0,127,227",
      ["DDOption.Text"] = "204,204,255,255",
      ["Handle.Background"] = "51,51,152,232",
      ["Handle.Grabbed"] = "51,51,255,255",
      ["Slider"] = "178,178,255,255",
      ["Input.Background"] = "0,0,51,211",
      ["Input.Border"] = "0,0,76,216",
      ["Input.Text"] = "204,204,255,255",
      ["Input.Focused"] = "0,0,255,255",
      ["Input.Invalid"] = "255,0,0,255",
      ["Input.Valid"] = "0,255,0,255",
      ["Input.Selection"] = "178,178,255,127",
      ["Input.Caret"] = "178,178,255,127",
    };


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



    public static void Initialize()
    {
      if (CUI.PalettesPath == null) return;
      Stopwatch sw = Stopwatch.StartNew();

      try
      {
        LoadedPalettes.Clear();
        LoadPalettes(CUI.PalettesPath);
        LoadSet(Path.Combine(PaletteSetsPath, DefaultPalette + ".xml"));

        //TransformToBackup(Primary);
      }
      catch (Exception e) { CUI.Warning(e); }

      CUIDebug.Log($"CUIPalette.Initialize took {sw.ElapsedMilliseconds}ms");
    }

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

    public static void LoadPalettes(string path)
    {
      foreach (string file in Directory.GetFiles(path, "*.xml"))
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
      if (path == null || path == "")
      {
        CUI.Warning($"Can't load CUIPalette set: path is empty");
        return;
      }
      if (!File.Exists(path))
      {
        CUI.Warning($"Couldn't find {Path.GetFileName(path)} CUIPalette set");
        return;
      }

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

    /// <summary>
    /// I used it once to transform xml palette to hardcoded backup palette
    /// </summary>
    public static void TransformToBackup(CUIPalette palette)
    {
      using (StreamWriter writer = new StreamWriter(Path.Combine(CUI.PalettesPath, "backup.txt"), false))
      {
        writer.WriteLine("{");
        foreach (var (key, value) in palette.Values)
        {
          writer.WriteLine($"  [\"{key}\"] = \"{value}\",");
        }
        writer.WriteLine("}");
      }
    }

  }


}