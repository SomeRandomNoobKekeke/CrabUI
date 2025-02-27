using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Button with multiple options
  /// which are rotating when you click
  /// </summary>
  public class CUIMultiButton : CUIButton
  {
    private List<string> options = new List<string>();
    /// <summary>
    /// Options are just strings
    /// </summary>
    [CUISerializable]
    public IEnumerable<string> Options
    {
      get => options;
      set => options = value.ToList();
    }
    public event Action<string> OnSelect;
    public Action<string> AddOnSelect { set { OnSelect += value; } }

    public bool CycleOnClick { get; set; } = true;
    public int SelectedIndex
    {
      get => options.IndexOf(Selected);
      set
      {
        if (options.Count == 0)
        {
          Selected = "";
        }
        else
        {
          Selected = options.ElementAtOrDefault(value % options.Count) ?? "";
        }
      }
    }
    [CUISerializable]
    public string Selected
    {
      get => Text;
      set
      {
        Text = value;
        OnSelect?.Invoke(value);
      }
    }

    public void Add(string option) => options.Add(option);
    public void Remove(string option)
    {
      int i = options.IndexOf(option);
      options.Remove(option);
      if (option == Selected) Select(i);
    }
    public void Select(int i) => SelectedIndex = i;
    public void Select(string option) => Selected = option;
    public void SelectNext() => SelectedIndex++;
    public void SelectPrev() => SelectedIndex--;

    public CUIMultiButton() : base()
    {
      Text = "MultiButton";
      OnMouseDown += (e) =>
      {
        if (CycleOnClick)
        {
          SelectNext();
          if (Command != null) DispatchUp(new CUICommand(Command, Selected));
        }
      };
    }


    /// <summary>
    /// CUITextBlock DoWrapFor but for all text
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    protected override Vector2 DoWrapFor(Vector2 size)
    {
      if ((!WrappedForThisSize.HasValue || size == WrappedForThisSize.Value) && !TextPropChanged) return WrappedSize;

      TextPropChanged = false;
      WrappedForThisSize = size;

      if (Vertical) size = new Vector2(0, size.Y);


      IEnumerable<string> WrappedTexts;
      if (Wrap || Vertical)
      {
        WrappedText = Font.WrapText(Text, size.X / TextScale - Padding.X * 2).Trim('\n');
        WrappedTexts = options.Select(o => Font.WrapText(o, size.X / TextScale - Padding.X * 2).Trim('\n'));
      }
      else
      {
        WrappedText = Text;
        WrappedTexts = options;
      }

      IEnumerable<Vector2> RealTextSizes = WrappedTexts.Select(t => Font.MeasureString(t) * TextScale);

      float maxX = 0;
      float maxY = 0;
      foreach (Vector2 s in RealTextSizes)
      {
        if (s.X > maxX) maxX = s.X;
        if (s.Y > maxY) maxY = s.Y;
      }

      Vector2 MaxTextSize = new Vector2(maxX, maxY);

      RealTextSize = Font.MeasureString(WrappedText) * TextScale;

      if (WrappedText == "") RealTextSize = new Vector2(0, 0);
      RealTextSize = new Vector2((float)Math.Round(RealTextSize.X), (float)Math.Round(RealTextSize.Y));

      Vector2 minSize = MaxTextSize + Padding * 2;

      if (!Wrap || Vertical)
      {
        SetForcedMinSize(new CUINullVector2(minSize));
      }

      WrappedSize = new Vector2(Math.Max(size.X, minSize.X), Math.Max(size.Y, minSize.Y));

      return WrappedSize;
    }

    internal override Vector2 AmIOkWithThisSize(Vector2 size)
    {
      return DoWrapFor(size);
    }
  }
}