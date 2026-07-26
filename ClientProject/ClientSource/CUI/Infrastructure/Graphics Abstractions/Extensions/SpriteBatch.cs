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
    public static void Draw(this SpriteBatch spriteBatch,
      Texture2D texture,
      VertexPositionColorTexture lt,
      VertexPositionColorTexture rt,
      VertexPositionColorTexture rb,
      VertexPositionColorTexture lb
    )
    {
      spriteBatch.CheckValid(texture);

      var item = spriteBatch._batcher.CreateBatchItem();
      item.Texture = texture;
      item.Effect = spriteBatch._effect;

      item.SortKey = 0f;

      item.vertexTL = lt;
      item.vertexTR = rt;
      item.vertexBR = rb;
      item.vertexBL = lb;
    }
  }
}