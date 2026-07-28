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
namespace CrabUI
{
  public partial record CUIFlexSprite
  {
    private CUITexture2D _Texture; public CUITexture2D Texture
    {
      get => _Texture;
      set
      {
        _Texture = value;
        UpdateTexurevertices();
      }
    }
    private Rectangle? _SourceRectangle; public Rectangle? SourceRectangle
    {
      get => _SourceRectangle;
      set
      {
        _SourceRectangle = value;
        UpdateTexurevertices();
      }
    }

    private void UpdateTexurevertices()
    {
      if (SourceRectangle.HasValue)
      {
        VertexLT.TextureCoordinate = new Vector2(
          SourceRectangle.Value.Left * Texture.TexelWidth,
          SourceRectangle.Value.Top * Texture.TexelHeight
        );
        VertexRT.TextureCoordinate = new Vector2(
          (SourceRectangle.Value.Left + SourceRectangle.Value.Width) * Texture.TexelWidth,
          SourceRectangle.Value.Top * Texture.TexelHeight
        );
        VertexRB.TextureCoordinate = new Vector2(
          (SourceRectangle.Value.Left + SourceRectangle.Value.Width) * Texture.TexelWidth,
          (SourceRectangle.Value.Top + SourceRectangle.Value.Height) * Texture.TexelHeight
        );
        VertexLB.TextureCoordinate = new Vector2(
          SourceRectangle.Value.Left * Texture.TexelWidth,
          (SourceRectangle.Value.Top + SourceRectangle.Value.Height) * Texture.TexelHeight
        );
      }
      else
      {
        VertexLT.TextureCoordinate = new Vector2(0, 0);
        VertexRT.TextureCoordinate = new Vector2(1, 0);
        VertexRB.TextureCoordinate = new Vector2(1, 1);
        VertexLB.TextureCoordinate = new Vector2(0, 1);
      }
    }

    private VertexPositionColorTexture VertexLT = new();
    private VertexPositionColorTexture VertexRT = new();
    private VertexPositionColorTexture VertexRB = new();
    private VertexPositionColorTexture VertexLB = new();

    public Color Color
    {
      get => VertexLT.Color;
      set => SetColors(value, value, value, value);
    }

    public void SetColors(Color lt, Color rt, Color rb, Color lb)
    {
      VertexLT.Color = lt;
      VertexRT.Color = rt;
      VertexRB.Color = rb;
      VertexLB.Color = lb;
    }

    public void Draw(CUISpriteBatch spriteBatch, Vector2 lt, Vector2 rt, Vector2 rb, Vector2 lb)
    {
      VertexLT.Position = new Vector3(lt, 0);
      VertexRT.Position = new Vector3(rt, 0);
      VertexRB.Position = new Vector3(rb, 0);
      VertexLB.Position = new Vector3(lb, 0);

      spriteBatch.Draw(Texture, VertexLT, VertexRT, VertexRB, VertexLB);
    }


    public CUIFlexSprite(CUITexture2D texture)
    {
      Texture = texture;
      Color = Color.White;
    }
  }
}