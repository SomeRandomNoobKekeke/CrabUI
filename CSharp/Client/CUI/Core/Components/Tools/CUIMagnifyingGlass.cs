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
  public class CUIMagnifyingGlass : CUICanvas
  {
    CUITexture2D texture;
    Color[] backBuffer;

    public SamplerState SamplerState { get; set; }

    public double UpdateInterval { get; set; } = 1.0 / 10.0f;
    double lastUpdate;

    public void Update()
    {
      if (Timing.TotalTime - lastUpdate > UpdateInterval)
      {
        lastUpdate = Timing.TotalTime;

        CUICore.GraphicsDevice.GetBackBufferData(backBuffer);
        texture.SetData(backBuffer);

        texture.GetData(
          0, new Rectangle((int)Rect.Left, (int)Rect.Top, Size.X, Size.Y), Data, 0, Data.Length
        );
        ApplyData();
      }
    }

    public CUIMagnifyingGlass() : base()
    {
      SamplerState = CUI.NoSmoothing;

      int w = CUICore.GraphicsDevice.BackBufferWidth;
      int h = CUICore.GraphicsDevice.BackBufferHeight;

      backBuffer = new Color[w * h];

      texture = CUITexture2D.Create(w, h, false, CUICore.GraphicsDevice.BackBufferFormat);

      CUI.OnDrawAfterGUI += (sb) => Update();
    }

    public override void Dispose()
    {
      texture.Dispose();
      base.Dispose();
    }
  }

}