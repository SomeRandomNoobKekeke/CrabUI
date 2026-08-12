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
    public bool Active { get; set; }
    CUITexture2D texture;
    Color[] backBuffer;



    public double UpdateInterval { get; set; } = 1.0 / 20.0f;
    double lastUpdate;

    public void Update()
    {
      if (!Active) return;

      if (Timing.TotalTime - lastUpdate > UpdateInterval)
      {
        lastUpdate = Timing.TotalTime;

        CUICore.GraphicsDevice.GetBackBufferData(backBuffer);
        texture.SetData(backBuffer);

        //TODO
        // Rectangle rect = new Rectangle(
        //   (int)Math.Max(0, Rect.Left),
        //   (int)Math.Max(0, Rect.Top),
        //   (int)Math.Min(CUICore.GraphicsDevice.BackBufferWidth, Rect.Left + Size.X),
        //   (int)Math.Min(CUICore.GraphicsDevice.BackBufferHeight, Rect.Top + Size.Y)
        // );

        try
        {
          texture.GetData(
           0, new Rectangle((int)Rect.Left, (int)Rect.Top, Size.X, Size.Y), Data, 0, Data.Length
          );
          ApplyData();
        }
        catch (Exception e)
        {
          CUI.Logger.Warning($"CUIMG: {e.Message}");
        }
      }
    }

    public CUIMagnifyingGlass() : base()
    {
      VisualBounds.SamplerState = CUI.NoSmoothing;
      Background.Color = Color.White;

      int w = CUICore.GraphicsDevice.BackBufferWidth;
      int h = CUICore.GraphicsDevice.BackBufferHeight;

      backBuffer = new Color[w * h];

      texture = CUICore.TextureManager.CreateNew(w, h, false, CUICore.GraphicsDevice.BackBufferFormat);

      CUI.OnDrawAfterGUI += (sb) => Update();
    }

    public override void Dispose()
    {
      texture.ForgetAndDispose();
      base.Dispose();
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Displayed || CulledOut) yield break;

      yield return VisualBounds.LeftBound;
      yield return Background.VisualWrapper;
      yield return VisualBounds.RightBound;
    }
  }

}