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
  public partial class CUISprite : ITextureSource
  {
    private CUITexture2D _Texture; public CUITexture2D Texture
    {
      get => _Texture;
      set
      {
        _Texture = value;
        UpdateDataBuffer();
      }
    }
    public Rectangle? SourceRectangle { get; set; } = null;

    public Point Size => SourceRectangle.HasValue ? SourceRectangle.Value.Size : Texture.Bounds.Size;

    //CRINGE to block sprite color inheritance in SimpleTexture
    public bool ColorIsDefault { get; private set; } = true;

    public static Color DefaultColor => Color.White;

    private Color _ColorTL = DefaultColor;
    private Color _ColorTR = DefaultColor;
    private Color _ColorBR = DefaultColor;
    private Color _ColorBL = DefaultColor;

    public Color ColorTL
    {
      get => _ColorTL;
      set { _ColorTL = value; ColorIsDefault = false; }
    }
    public Color ColorTR
    {
      get => _ColorTR;
      set { _ColorTR = value; ColorIsDefault = false; }
    }
    public Color ColorBR
    {
      get => _ColorBR;
      set { _ColorBR = value; ColorIsDefault = false; }
    }
    public Color ColorBL
    {
      get => _ColorBL;
      set { _ColorBL = value; ColorIsDefault = false; }
    }

    public Color Color
    {
      get => ColorTL;
      set
      {
        ColorTL = value;
        ColorTR = value;
        ColorBR = value;
        ColorBL = value;
      }
    }

    public Color ColorTop { get => ColorTL; set { ColorTL = value; ColorTR = value; } }
    public Color ColorRight { get => ColorTR; set { ColorTR = value; ColorBR = value; } }
    public Color ColorBottom { get => ColorBL; set { ColorBR = value; ColorBL = value; } }
    public Color ColorLeft { get => ColorTL; set { ColorTL = value; ColorBL = value; } }

    public float Rotation { get; set; } = 0.0f;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0.0f;

    public CUISpriteDrawMode DrawMode { get; set; }


    public void Draw(CUISpriteBatch spriteBatch, Rectangle destinationRectangle)
    {
      switch (DrawMode)
      {
        case CUISpriteDrawMode.Resize:
          spriteBatch.Draw(
            Texture,
            destinationRectangle,
            SourceRectangle,
            ColorTL, ColorTR, ColorBR, ColorBL, Rotation, Origin, Effects, LayerDepth
          );
          break;

        case CUISpriteDrawMode.Wrap:
          if (SourceRectangle.HasValue)
          {
            spriteBatch.Draw(
              Texture,
              destinationRectangle,
              new Rectangle(
                SourceRectangle.Value.Left,
                SourceRectangle.Value.Top,
                destinationRectangle.Width,
                destinationRectangle.Height
              ),
              ColorTL, ColorTR, ColorBR, ColorBL, Rotation, Origin, Effects, LayerDepth
            );
          }
          else
          {
            spriteBatch.Draw(
              Texture,
              destinationRectangle,
              new Rectangle(0, 0, destinationRectangle.Width, destinationRectangle.Height),
              ColorTL, ColorTR, ColorBR, ColorBL, Rotation, Origin, Effects, LayerDepth
            );
          }
          break;

        case CUISpriteDrawMode.Static:
          spriteBatch.Draw(
            Texture,
            destinationRectangle,
            destinationRectangle,
            ColorTL, ColorTR, ColorBR, ColorBL, Rotation, Origin, Effects, LayerDepth
          );
          break;

        case CUISpriteDrawMode.StaticDeep:
          spriteBatch.Draw(
            Texture,
            destinationRectangle,
            new Rectangle(
              (int)(destinationRectangle.Left * 0.9f),
              (int)(destinationRectangle.Top * 0.9f),
              destinationRectangle.Width,
              destinationRectangle.Height
            ),
            ColorTL, ColorTR, ColorBR, ColorBL, Rotation, Origin, Effects, LayerDepth
          );
          break;

          // case CUISpriteDrawMode.Zoom:
          //   Rectangle Zoom(Rectangle rect, float z)
          //   {
          //     Vector2 ScreenCenter = CUI.GameScreenRect.Size.ToVector2() / 2.0f;
          //     Vector2 PosDif = new Vector2(rect.Left, rect.Top) - ScreenCenter;
          //     Vector2 newPos = PosDif * z + ScreenCenter;

          //     return new Rectangle(
          //       (int)newPos.X, (int)newPos.Y,
          //       (int)(rect.Width / z), (int)(rect.Height / z)
          //     );
          //   }

          //   spriteBatch.Draw(
          //     Texture,
          //     destinationRectangle,
          //     Zoom(destinationRectangle, 0.6f),
          //     ColorTL, ColorTR, ColorBR, ColorBL, Rotation, Origin, Effects, LayerDepth
          //   );
          //   break;
      }

    }




    public CUISprite() { Texture = CUITexture2D.White; }
    public CUISprite(CUITexture2D texture) { Texture = texture; }
    public CUISprite(CUISprite basedOn)
    {
      Texture = basedOn.Texture;
      SourceRectangle = basedOn.SourceRectangle;
    }
  }
}