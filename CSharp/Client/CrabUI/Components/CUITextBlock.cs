using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;
namespace CrabUI
{
  /// <summary>
  /// Passive block of text
  /// </summary>
  public class CUITextBlock : CUIComponent
  {
    public event Action OnTextChanged;
    public Action AddOnTextChanged { set { OnTextChanged += value; } }

    [CUISerializable] public bool Wrap { get; set; }
    [CUISerializable] public Color TextColor { get; set; }
    [CUISerializable] public GUIFont Font { get; set; } = GUIStyle.Font;
    [CUISerializable] public Vector2 TextAlign { get; set; }
    [CUISerializable] public bool Vertical { get; set; }


    [CUISerializable]
    public string Text { get => text; set => SetText(value); }
    [CUISerializable]
    public float TextScale { get => textScale; set => SetTextScale(value); }

    #region Cringe
    protected Vector2 RealTextSize;
    [Calculated] protected Vector2 TextDrawPos { get; set; }
    [Calculated] protected string WrappedText { get; set; } = "";
    protected Vector2? WrappedForThisSize;
    [Calculated] protected Vector2 WrappedSize { get; set; }
    protected bool NeedReWrapping;
    #endregion

    protected string text = ""; internal void SetText(string value)
    {
      text = value ?? "";
      OnTextChanged?.Invoke();

      if (Ghost.X && Ghost.Y)
      {
        WrappedText = text;
      }
      else
      {
        NeedReWrapping = true;
        OnPropChanged();
        OnAbsolutePropChanged();
      }
    }

    protected float textScale = 0.9f; internal void SetTextScale(float value)
    {
      textScale = value; OnDecorPropChanged();
    }

    //Note: works only on unwrapped text for now because WrappedText is delayed
    /// <summary>
    /// X coordinate of caret if there was one  
    /// Used in CUITextInput, you don't need this
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public float CaretPos(int i)
    {
      return Font.MeasureString(Text.SubstringSafe(0, i)).X * TextScale + Padding.X;
    }

    //Note: works only on unwrapped text for now because WrappedText is delayed
    /// <summary>
    /// Tndex of caret if there was one  
    /// Used in CUITextInput, you don't need this
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public int CaretIndex(float x)
    {
      int Aprox = (int)Math.Round((x - Padding.X) / Font.MeasureString(Text).X * Text.Length);

      int closestCaretPos = Aprox;
      float smallestDif = Math.Abs(x - CaretPos(Aprox));

      for (int i = Aprox - 2; i <= Aprox + 2; i++)
      {
        float dif = Math.Abs(x - CaretPos(i));
        if (dif < smallestDif)
        {
          closestCaretPos = i;
          smallestDif = dif;
        }
      }

      return closestCaretPos;
    }

    protected Vector2 DoWrapFor(Vector2 size)
    {
      if ((!WrappedForThisSize.HasValue || size == WrappedForThisSize.Value) && !NeedReWrapping) return WrappedSize;

      WrappedForThisSize = size;

      if (Vertical) size = new Vector2(0, size.Y);

      if (Wrap || Vertical)
      {
        WrappedText = Font.WrapText(Text, size.X / TextScale - Padding.X * 2).Trim('\n');
      }
      else
      {
        WrappedText = Text;
      }

      RealTextSize = Font.MeasureString(WrappedText) * TextScale;

      Vector2 minSize = RealTextSize + Padding * 2;

      if (!Wrap || Vertical)
      {
        SetForcedMinSize(new CUINullVector2(minSize));
      }

      WrappedSize = new Vector2(Math.Max(size.X, minSize.X), Math.Max(size.Y, minSize.Y));
      NeedReWrapping = false;

      return WrappedSize;
    }

    //HACK WHY i need to call this to calculate text position?
    internal override Vector2 AmIOkWithThisSize(Vector2 size)
    {
      return DoWrapFor(size);
    }

    internal override void UpdatePseudoChildren()
    {

      TextDrawPos = CUIAnchor.GetChildPos(Real, TextAlign, Vector2.Zero, RealTextSize / Scale)
      + Padding * CUIAnchor.Direction(TextAlign) / Scale;

      CUIDebug.Capture(null, this, "UpdatePseudoChildren", "", "TextDrawPos", $"{TextDrawPos - Real.Position}");
    }


    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);

      // Font.DrawString(spriteBatch, WrappedText, TextDrawPos, TextColor, rotation: 0, origin: Vector2.Zero, TextScale, spriteEffects: SpriteEffects.None, layerDepth: 0.1f);

      Font.Value.DrawString(spriteBatch, WrappedText, TextDrawPos, TextColor, rotation: 0, origin: Vector2.Zero, TextScale / Scale, se: SpriteEffects.None, layerDepth: 0.1f);
    }

    public CUITextBlock()
    {
      Padding = new Vector2(4, 0);

      BackgroundColor = Color.Transparent;
      BorderColor = Color.Transparent;
      TextColor = CUIPallete.Default.Secondary.Text;
    }

    public CUITextBlock(string text) : this()
    {
      Text = text;
    }
  }
}