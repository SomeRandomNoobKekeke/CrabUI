using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace CrabUI
{
  public partial class CUI
  {
    public const float Pi2 = (float)(Math.PI / 2.0);


    public static SamplerState NoSmoothing = new SamplerState()
    {
      Filter = TextureFilter.Point,
      AddressU = TextureAddressMode.Clamp,
      AddressV = TextureAddressMode.Clamp,
      AddressW = TextureAddressMode.Clamp,
      BorderColor = Color.White,
      MaxAnisotropy = 4,
      MaxMipLevel = 0,
      MipMapLevelOfDetailBias = -0.8f,
      ComparisonFunction = CompareFunction.Never,
      FilterMode = TextureFilterMode.Default,
    };

    public static void DrawTexture(SpriteBatch sb, CUIRect cuirect, Color cl, Texture2D texture, float depth = 0.0f)
    {
      Rectangle sourceRect = new Rectangle(0, 0, (int)cuirect.Width, (int)cuirect.Height);

      sb.Draw(texture, cuirect.Box, sourceRect, cl, 0.0f, Vector2.Zero, SpriteEffects.None, depth);
    }
    public static void DrawRectangle(SpriteBatch sb, CUIRect cuirect, Color cl, CUISprite sprite, float depth = 0.0f)
    {
      Rectangle sourceRect = sprite.DrawMode switch
      {
        CUISpriteDrawMode.Resize => sprite.SourceRect,
        CUISpriteDrawMode.Wrap => new Rectangle(0, 0, (int)cuirect.Width, (int)cuirect.Height),
        CUISpriteDrawMode.Static => cuirect.Box,
        CUISpriteDrawMode.StaticDeep => cuirect.Zoom(0.9f),
        _ => sprite.SourceRect,
      };

      Rectangle rect = new Rectangle(
        (int)(cuirect.Left + sprite.Offset.X * cuirect.Width),
        (int)(cuirect.Top + sprite.Offset.Y * cuirect.Height),
        (int)(cuirect.Width),
        (int)(cuirect.Height)
      );

      //rect = cuirect.Box;

      sb.Draw(sprite.Texture, rect, sourceRect, cl, sprite.Rotation, sprite.Origin, sprite.Effects, depth);
    }

    //TODO i can calculate those rects in advance
    public static void DrawBorders(SpriteBatch sb, CUIComponent component, float depth = 0.0f)
    {
      Texture2D texture = component.BorderSprite.Texture;
      Rectangle sourceRect = texture.Bounds;

      Rectangle targetRect;
      Color cl;
      float rotation = 0.0f;
      float thickness = 1.0f;
      bool visible = false;

      // Right
      visible = component.RigthBorder?.Visible ?? component.Border.Visible;
      thickness = component.RigthBorder?.Thickness ?? component.Border.Thickness;
      cl = component.RigthBorder?.Color ?? component.Border.Color;
      targetRect = CUIRect.CreateRect(
        component.Real.Left + component.Real.Width,
        component.Real.Top,
        component.Real.Height,
        thickness
      );
      sourceRect = CUIRect.CreateRect(
        0, 0,
        targetRect.Width, texture.Height
      );
      rotation = Pi2;
      sb.Draw(texture, targetRect, sourceRect, cl, rotation, Vector2.Zero, SpriteEffects.None, depth);

      //Left
      visible = component.LeftBorder?.Visible ?? component.Border.Visible;
      thickness = component.LeftBorder?.Thickness ?? component.Border.Thickness;
      cl = component.LeftBorder?.Color ?? component.Border.Color;
      targetRect = CUIRect.CreateRect(
        component.Real.Left + thickness,
        component.Real.Top,
        component.Real.Height,
        thickness
      );
      sourceRect = CUIRect.CreateRect(
        0, 0,
        targetRect.Width, texture.Height
      );
      rotation = Pi2;
      sb.Draw(texture, targetRect, sourceRect, cl, rotation, Vector2.Zero, SpriteEffects.FlipVertically, depth);


      //Top
      visible = component.TopBorder?.Visible ?? component.Border.Visible;
      thickness = component.TopBorder?.Thickness ?? component.Border.Thickness;
      cl = component.TopBorder?.Color ?? component.Border.Color;
      targetRect = CUIRect.CreateRect(
        component.Real.Left,
        component.Real.Top,
        component.Real.Width,
        thickness
      );
      sourceRect = CUIRect.CreateRect(
        0, 0,
        targetRect.Width, texture.Height
      );
      rotation = 0.0f;
      sb.Draw(texture, targetRect, sourceRect, cl, rotation, Vector2.Zero, SpriteEffects.None, depth);



      //Bottom
      visible = component.BottomBorder?.Visible ?? component.Border.Visible;
      thickness = component.BottomBorder?.Thickness ?? component.Border.Thickness;
      cl = component.BottomBorder?.Color ?? component.Border.Color;
      targetRect = CUIRect.CreateRect(
        component.Real.Left,
        component.Real.Bottom - thickness,
        component.Real.Width,
        thickness
      );
      sourceRect = CUIRect.CreateRect(
        0, 0,
        targetRect.Width, texture.Height
      );
      rotation = 0;
      sb.Draw(texture, targetRect, sourceRect, cl, rotation, Vector2.Zero, SpriteEffects.FlipVertically, depth);


    }
  }
}