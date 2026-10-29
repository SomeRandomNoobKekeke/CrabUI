using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public partial class CUIPalettes
  {
    public CUIPalettes() => InitPalettes();

    public ICUIPaletteShader Shader { get; set; } = new DefaultShader();

    public CUIPalette this[string name]
    {
      get
      {
        PropertyInfo pi = typeof(CUIPalettes).GetProperty(name);
        if (pi is null || pi.PropertyType != typeof(CUIPalette)) return Primary;
        return (CUIPalette)pi.GetValue(this)!;
      }
      set
      {
        PropertyInfo pi = typeof(CUIPalettes).GetProperty(name);
        if (pi is null || pi.PropertyType != typeof(CUIPalette)) return;
        pi.SetValue(this, value);
      }
    }



    //CRINGE i didn't want to trigger those lazy initializers, slow af, mb should be just hardcoded
    public Dictionary<string, CUIPalette> All
    {
      get
      {
        Dictionary<string, CUIPalette> dict = [];
        foreach (PropertyInfo paletteProp in typeof(CUIPalettes).GetProperties(
          BindingFlags.Instance | BindingFlags.Public
        ))
        {
          if (paletteProp.PropertyType != typeof(CUIPalette)) continue;

          FieldInfo? realField = typeof(CUIPalettes).GetField($"_{paletteProp.Name}", BindingFlags.Instance | BindingFlags.NonPublic);

          if (realField == null) continue;

          CUIPalette? palette = (CUIPalette?)realField.GetValue(this);

          if (palette != null) dict[paletteProp.Name] = palette;
        }

        return dict;
      }
    }

    public Dictionary<string, Color> DefaultColors => new Dictionary<string, Color>()
    {
      ["Primary"] = new Color(70, 70, 70, 255),
      ["Secondary"] = new Color(112, 0, 0, 255),
      ["Tertiary"] = new Color(0, 94, 91, 255),
      ["Quaternary"] = new Color(92, 92, 92, 255),
      ["Quinary"] = new Color(92, 92, 92, 255),
      ["Senary"] = new Color(92, 92, 92, 255),
      ["Septenary"] = new Color(92, 92, 92, 255),
      ["Octonary"] = new Color(92, 92, 92, 255),
    };

    private void InitPalettes()
    {
      foreach (var (name, color) in DefaultColors)
      {
        this[name] = CUIPalette.FromColor(color);
      }
    }

    private CUIPalette _Primary = new CUIPalette(); public CUIPalette Primary
    {
      get => _Primary;
      set
      {
        _Primary.Swap(value);
        if (_Primary2 is not null) _Primary2.Swap(Shader.Shade2(Primary));
        if (_Primary3 is not null) _Primary3.Swap(Shader.Shade3(Primary));
        if (_Primary4 is not null) _Primary4.Swap(Shader.Shade4(Primary));
        if (_Primary5 is not null) _Primary5.Swap(Shader.Shade5(Primary));
      }
    }

    public CUIPalette Primary1
    {
      get => Primary;
      set => Primary = value;
    }

    private CUIPalette? _Primary2; public CUIPalette Primary2
    {
      get => _Primary2 ??= Shader.Shade2(Primary);
      set => Primary2.Swap(value);
    }

    private CUIPalette? _Primary3; public CUIPalette Primary3
    {
      get => _Primary3 ??= Shader.Shade3(Primary);
      set => Primary3.Swap(value);
    }

    private CUIPalette? _Primary4; public CUIPalette Primary4
    {
      get => _Primary4 ??= Shader.Shade4(Primary);
      set => Primary4.Swap(value);
    }

    private CUIPalette? _Primary5; public CUIPalette Primary5
    {
      get => _Primary5 ??= Shader.Shade5(Primary);
      set => Primary5.Swap(value);
    }


    private CUIPalette _Secondary = new CUIPalette(); public CUIPalette Secondary
    {
      get => _Secondary;
      set
      {
        _Secondary.Swap(value);
        if (_Secondary2 is not null) _Secondary2.Swap(Shader.Shade2(Secondary));
        if (_Secondary3 is not null) _Secondary3.Swap(Shader.Shade3(Secondary));
        if (_Secondary4 is not null) _Secondary4.Swap(Shader.Shade4(Secondary));
        if (_Secondary5 is not null) _Secondary5.Swap(Shader.Shade5(Secondary));
      }
    }

    public CUIPalette Secondary1
    {
      get => Secondary;
      set => Secondary = value;
    }

    private CUIPalette? _Secondary2; public CUIPalette Secondary2
    {
      get => _Secondary2 ??= Shader.Shade2(Secondary);
      set => Secondary2.Swap(value);
    }

    private CUIPalette? _Secondary3; public CUIPalette Secondary3
    {
      get => _Secondary3 ??= Shader.Shade3(Secondary);
      set => Secondary3.Swap(value);
    }

    private CUIPalette? _Secondary4; public CUIPalette Secondary4
    {
      get => _Secondary4 ??= Shader.Shade4(Secondary);
      set => Secondary4.Swap(value);
    }

    private CUIPalette? _Secondary5; public CUIPalette Secondary5
    {
      get => _Secondary5 ??= Shader.Shade5(Secondary);
      set => Secondary5.Swap(value);
    }




    private CUIPalette _Tertiary = new CUIPalette(); public CUIPalette Tertiary
    {
      get => _Tertiary;
      set
      {
        _Tertiary.Swap(value);
        if (_Tertiary2 is not null) _Tertiary2.Swap(Shader.Shade2(Tertiary));
        if (_Tertiary3 is not null) _Tertiary3.Swap(Shader.Shade3(Tertiary));
        if (_Tertiary4 is not null) _Tertiary4.Swap(Shader.Shade4(Tertiary));
        if (_Tertiary5 is not null) _Tertiary5.Swap(Shader.Shade5(Tertiary));
      }
    }

    public CUIPalette Tertiary1
    {
      get => Tertiary;
      set => Tertiary = value;
    }

    private CUIPalette? _Tertiary2; public CUIPalette Tertiary2
    {
      get => _Tertiary2 ??= Shader.Shade2(Tertiary);
      set => Tertiary2.Swap(value);
    }

    private CUIPalette? _Tertiary3; public CUIPalette Tertiary3
    {
      get => _Tertiary3 ??= Shader.Shade3(Tertiary);
      set => Tertiary3.Swap(value);
    }

    private CUIPalette? _Tertiary4; public CUIPalette Tertiary4
    {
      get => _Tertiary4 ??= Shader.Shade4(Tertiary);
      set => Tertiary4.Swap(value);
    }

    private CUIPalette? _Tertiary5; public CUIPalette Tertiary5
    {
      get => _Tertiary5 ??= Shader.Shade5(Tertiary);
      set => Tertiary5.Swap(value);
    }



    private CUIPalette _Quaternary = new CUIPalette(); public CUIPalette Quaternary
    {
      get => _Quaternary;
      set
      {
        _Quaternary.Swap(value);
        if (_Quaternary2 is not null) _Quaternary2.Swap(Shader.Shade2(Quaternary));
        if (_Quaternary3 is not null) _Quaternary3.Swap(Shader.Shade3(Quaternary));
        if (_Quaternary4 is not null) _Quaternary4.Swap(Shader.Shade4(Quaternary));
        if (_Quaternary5 is not null) _Quaternary5.Swap(Shader.Shade5(Quaternary));
      }
    }

    public CUIPalette Quaternary1
    {
      get => Quaternary;
      set => Quaternary = value;
    }

    private CUIPalette? _Quaternary2; public CUIPalette Quaternary2
    {
      get => _Quaternary2 ??= Shader.Shade2(Quaternary);
      set => Quaternary2.Swap(value);
    }

    private CUIPalette? _Quaternary3; public CUIPalette Quaternary3
    {
      get => _Quaternary3 ??= Shader.Shade3(Quaternary);
      set => Quaternary3.Swap(value);
    }

    private CUIPalette? _Quaternary4; public CUIPalette Quaternary4
    {
      get => _Quaternary4 ??= Shader.Shade4(Quaternary);
      set => Quaternary4.Swap(value);
    }

    private CUIPalette? _Quaternary5; public CUIPalette Quaternary5
    {
      get => _Quaternary5 ??= Shader.Shade5(Quaternary);
      set => Quaternary5.Swap(value);
    }



    private CUIPalette _Quinary = new CUIPalette(); public CUIPalette Quinary
    {
      get => _Quinary;
      set
      {
        _Quinary.Swap(value);
        if (_Quinary2 is not null) _Quinary2.Swap(Shader.Shade2(Quinary));
        if (_Quinary3 is not null) _Quinary3.Swap(Shader.Shade3(Quinary));
        if (_Quinary4 is not null) _Quinary4.Swap(Shader.Shade4(Quinary));
        if (_Quinary5 is not null) _Quinary5.Swap(Shader.Shade5(Quinary));
      }
    }

    public CUIPalette Quinary1
    {
      get => Quinary;
      set => Quinary = value;
    }

    private CUIPalette? _Quinary2; public CUIPalette Quinary2
    {
      get => _Quinary2 ??= Shader.Shade2(Quinary);
      set => Quinary2.Swap(value);
    }

    private CUIPalette? _Quinary3; public CUIPalette Quinary3
    {
      get => _Quinary3 ??= Shader.Shade3(Quinary);
      set => Quinary3.Swap(value);
    }

    private CUIPalette? _Quinary4; public CUIPalette Quinary4
    {
      get => _Quinary4 ??= Shader.Shade4(Quinary);
      set => Quinary4.Swap(value);
    }

    private CUIPalette? _Quinary5; public CUIPalette Quinary5
    {
      get => _Quinary5 ??= Shader.Shade5(Quinary);
      set => Quinary5.Swap(value);
    }



    private CUIPalette _Senary = new CUIPalette(); public CUIPalette Senary
    {
      get => _Senary;
      set => _Senary.Swap(value);
    }

    private CUIPalette _Septenary = new CUIPalette(); public CUIPalette Septenary
    {
      get => _Septenary;
      set => _Septenary.Swap(value);
    }

    private CUIPalette _Octonary = new CUIPalette(); public CUIPalette Octonary
    {
      get => _Octonary;
      set => _Octonary.Swap(value);
    }
  }
}