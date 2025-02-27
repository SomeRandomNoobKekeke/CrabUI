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


    //TODO move padding here, it makes no sense in CUIComponent
    private bool wrap;
    [CUISerializable]
    public bool Wrap
    {
      get => wrap;
      set
      {
        wrap = value;
        MeasureUnwrapped();
        TextPropChanged = true;
      }
    }
    [CUISerializable] public Color TextColor { get; set; }
    private GUIFont font = GUIStyle.Font;
    [CUISerializable]
    public GUIFont Font
    {
      get => font;
      set
      {
        font = value;
        MeasureUnwrapped();
        TextPropChanged = true;
      }
    }
    /// <summary>
    /// A Vector2 ([0..1],[0..1])
    /// </summary>
    [CUISerializable] public Vector2 TextAlign { get; set; }
    [CUISerializable] public bool Vertical { get; set; }
    /// <summary>
    /// Lil optimization: ghost text won't set forsed size and parent won't be able to fit to it  
    /// But it will increase performance in large lists
    /// </summary>
    [CUISerializable] public bool GhostText { get; set; }

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
    public Vector2 UnwrappedTextSize { get; set; }
    public Vector2 UnwrappedMinSize { get; set; }
    protected bool TextPropChanged;
    #endregion

    protected string text = ""; internal void SetText(string value)
    {
      text = value ?? "";
      OnTextChanged?.Invoke();

      MeasureUnwrapped();
      TextPropChanged = true;
      OnPropChanged();
      OnAbsolutePropChanged();
    }

    protected float textScale = 0.9f; internal void SetTextScale(float value)
    {
      textScale = value;
      MeasureUnwrapped();
      TextPropChanged = true;
      OnPropChanged();
      OnAbsolutePropChanged();
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

    // Small optimisation, doesn't seem to save much
    protected virtual void MeasureUnwrapped()
    {
      UnwrappedTextSize = Font.MeasureString(Text) * TextScale;
      UnwrappedMinSize = UnwrappedTextSize + Padding * 2;
    }

    protected virtual Vector2 DoWrapFor(Vector2 size)
    {
      //  To prevent loop
      if (!(WrappedForThisSize.HasValue && WrappedForThisSize != size) && !TextPropChanged) return WrappedSize;

      TextPropChanged = false;
      WrappedForThisSize = size;

      // There's no way to wrap vertical text
      bool isInWrapZone = Vertical ? false : size.X <= UnwrappedMinSize.X;
      bool isSolid = Vertical || !Wrap;

      if (Vertical) size = new Vector2(0, size.Y);

      if ((Wrap && isInWrapZone) || Vertical)
      {
        WrappedText = Font.WrapText(Text, size.X / TextScale - Padding.X * 2).Trim('\n');
        RealTextSize = Font.MeasureString(WrappedText) * TextScale;
      }
      else
      {
        WrappedText = Text;
        RealTextSize = UnwrappedTextSize;
      }

      if (WrappedText == "") RealTextSize = new Vector2(0, 0);

      RealTextSize = new Vector2((float)Math.Round(RealTextSize.X), (float)Math.Round(RealTextSize.Y));

      Vector2 minSize = RealTextSize + Padding * 2;

      if (isSolid && !GhostText)
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

    //Note: This is a bottleneck for large lists of text
    internal override void UpdatePseudoChildren()
    {
      if (CulledOut) return;
      TextDrawPos = CUIAnchor.GetChildPos(Real, TextAlign, Vector2.Zero, RealTextSize / Scale) + Padding * CUIAnchor.Direction(TextAlign) / Scale;

      //CUIDebug.Capture(null, this, "UpdatePseudoChildren", "", "TextDrawPos", $"{TextDrawPos - Real.Position}");
    }


    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);

      // Font.DrawString(spriteBatch, WrappedText, TextDrawPos, TextColor, rotation: 0, origin: Vector2.Zero, TextScale, spriteEffects: SpriteEffects.None, layerDepth: 0.1f);

      Font.Value.DrawString(spriteBatch, WrappedText, TextDrawPos, TextColor, rotation: 0, origin: Vector2.Zero, TextScale / Scale, se: SpriteEffects.None, layerDepth: 0.1f);
    }

    public CUITextBlock() { }

    public CUITextBlock(string text) : this()
    {
      Text = text;
    }
  }
}