using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Just an example of what CUICanvas can be used for
  /// </summary>
  public class CUIWater : CUICanvas
  {
    public float Omega = 1.999f;

    public float[,] Pool1;
    public float[,] Pool2;
    public float[,] DensityMap;

    public Color[] ColorPalette = new Color[]{
      new Color(0,0,0,0),
      new Color(0,0,64),
      new Color(32,0,64),
      new Color(255,0,255),
      new Color(0,255,255),
    };


    public override Point Size
    {
      get => base.Size;
      set
      {
        base.Size = value;
        Pool1 = new float[Texture.Width, Texture.Height];
        Pool2 = new float[Texture.Width, Texture.Height];
        DensityMap = new float[Texture.Width, Texture.Height];
        RandomizeDensityMap();
      }
    }

    public float NextAmplitude(int x, int y)
    {
      float avg = (
        Pool1[x + 1, y] +
        Pool1[x, y + 1] +
        Pool1[x - 1, y] +
        Pool1[x, y - 1]
      ) / 4.0f;

      return avg * Omega + (1 - Omega) * Pool2[x, y];
    }

    public void Step()
    {
      for (int x = 1; x < Size.X - 1; x++)
      {
        for (int y = 1; y < Size.Y - 1; y++)
        {
          Pool2[x, y] = NextAmplitude(x, y) * DensityMap[x, y];
        }
      }

      (Pool1, Pool2) = (Pool2, Pool1);
    }

    public double UpdateInterval = 1.0 / 60.0;
    private double lastUpdateTime = -1;
    public void Update(double totalTime)
    {
      if (totalTime - lastUpdateTime < UpdateInterval) return;
      UpdateSelf();
      Step();
      lastUpdateTime = totalTime;

      TransferData();
    }

    public virtual void UpdateSelf()
    {

    }

    private void TransferData()
    {
      for (int x = 0; x < Size.X; x++)
      {
        for (int y = 0; y < Size.Y; y++)
        {
          SetPixel(x, y, ToolBox.GradientLerp(Math.Abs(Pool1[x, y]), ColorPalette));
        }
      }

      SetData();
    }

    public void RandomizeDensityMap()
    {
      for (int x = 0; x < Size.X; x++)
      {
        for (int y = 0; y < Size.Y; y++)
        {
          DensityMap[x, y] = 1.0f - CUI.Random.NextSingle() * 0.01f;
        }
      }
    }


    public float DropSize = 16.0f;
    public void Drop(float x, float y)
    {
      int i = (int)Math.Clamp(Math.Round(x * Texture.Width), 1, Texture.Width - 2);
      int j = (int)Math.Clamp(Math.Round(y * Texture.Height), 1, Texture.Height - 2);

      Pool1[i, j] = DropSize;
    }



    public CUIWater(int x, int y) : base(x, y)
    {
      //ConsumeDragAndDrop = true;

      //OnUpdate += Update;
      Pool1 = new float[Texture.Width, Texture.Height];
      Pool2 = new float[Texture.Width, Texture.Height];
      DensityMap = new float[Texture.Width, Texture.Height];
      RandomizeDensityMap();

      // OnMouseOn += (e) =>
      // {
      //   if (!MousePressed) return;
      //   Vector2 v = CUIAnchor.AnchorFromPos(Real, e.MousePosition);
      //   Drop(v.X, v.Y);
      // };
    }

    public CUIWater() : this(256, 256)
    {

    }
  }
}