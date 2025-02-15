using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using System.Xml.Linq;
namespace CrabUI
{
  /// <summary>
  /// Drop down list, aka Select
  /// </summary>
  public class CUIDropDown : CUIComponent
  {
    internal class DDOption : CUIButton
    {
      public DDOption() : this("") { }
      public DDOption(string text) : base(text) { }
    }
    private CUIButton MainButton;
    private CUIVerticalList OptionBox;

    /// <summary>
    /// List of options  
    /// Options are just strings
    /// </summary>
    [CUISerializable]
    public IEnumerable<string> Options
    {
      get => OptionBox.Children.Cast<DDOption>().Select(o => o.Text);
      set
      {
        Clear();
        foreach (string option in value) { Add(option); }
      }
    }
    [CUISerializable]
    public string Selected
    {
      get => MainButton.Text;
      set => Select(value);
    }

    public event Action<string> OnSelect;
    public Action<string> AddOnSelect { set { OnSelect += value; } }


    public void Open() => OptionBox.Revealed = true;
    public void Close() => OptionBox.Revealed = false;

    public void Clear()
    {
      OptionBox.RemoveAllChildren();
      Select("");
    }

    public void Add(string option)
    {
      OptionBox.Append(new DDOption(option)
      {
        AddOnMouseDown = (e) => Select(option),
      });
    }

    public void Select(int i) => Select(Options.ElementAtOrDefault(i));
    public void Select(string option)
    {
      MainButton.Text = option ?? "";
      OptionBox.Revealed = false;
      OnSelect?.Invoke(MainButton.Text);
    }

    public void Remove(int i) => Remove(Options.ElementAtOrDefault(i));
    public void Remove(string option)
    {
      if (option == null) return;
      if (!Options.Contains(option)) return;

      DDOption ddoption = OptionBox.Children.Cast<DDOption>().FirstOrDefault(o => o.Text == option);
      bool wasSelected = MainButton.Text == ddoption.Text;
      OptionBox.RemoveChild(ddoption);
      if (wasSelected) Select(0);
    }

    public CUIDropDown() : base()
    {
      BreakSerialization = true;
      OptionBox = new CUIVerticalList()
      {
        Relative = new CUINullRect(w: 1),
        FitContent = new CUIBool2(true, true),
        Ghost = new CUIBool2(false, true),
        Anchor = CUIAnchor.TopLeft,
        ParentAnchor = CUIAnchor.BottomLeft,
        ZIndex = 500,
        Style = new CUIStyle(){
          {"BackgroundColor", "CUIPalette.DDOption.Background"},
          {"Border", "CUIPalette.DDOption.Border"},
        },
      };

      MainButton = new CUIButton()
      {
        Text = "CUIDropDown",
        Relative = new CUINullRect(w: 1, h: 1),
        AddOnMouseDown = (e) => OptionBox.Revealed = !OptionBox.Revealed,
      };

      Append(MainButton);
      Append(OptionBox);

      FitContent = new CUIBool2(true, true);

      //HACK Why this main is hardcoded?
      //in static constructor CUI.Main is null and this won't work
      if (CUI.Main is not null) CUI.Main.OnMouseDown += (e) => Close();
    }
  }
}