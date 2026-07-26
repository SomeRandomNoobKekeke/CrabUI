using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public static class SpriteBatch_Extensions
  {
    public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, VertexPositionColorTexture[] vertices)
    {
      spriteBatch.CheckValid(texture);

      var item = spriteBatch._batcher.CreateBatchItem();
      item.Texture = texture;
      item.Effect = spriteBatch._effect;

      item.SortKey = 0f;

      item.vertexTL = vertices[0];
      item.vertexTR = vertices[1];
      item.vertexBR = vertices[2];
      item.vertexBL = vertices[3];
    }
  }
}