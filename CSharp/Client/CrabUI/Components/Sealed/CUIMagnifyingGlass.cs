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
  /// <summary>
  /// It's a debug tool, you can use it with cuimg command, it's very fps comsuming
  /// </summary>
  [NoDefault]
  public class CUIMagnifyingGlass : CUICanvas
  {


    public static CUIFrame GlassFrame;

    public static void AddToggleButton()
    {
      CUI.TopMain["ToggleMagnifyingGlass"] = new CUIButton("MG")
      {
        Absolute = new CUINullRect(0, 0, 20, 20),
        Anchor = CUIAnchor.CenterLeft,
        AddOnMouseDown = (e) => ToggleEquip(),
      };
    }

    public static void ToggleEquip()
    {
      if (GlassFrame != null)
      {
        GlassFrame.RemoveSelf();
        GlassFrame = null;
      }
      else
      {
        GlassFrame = new CUIFrame()
        {
          ZIndex = 100000,

          BackgroundColor = Color.Transparent,
          Border = new CUIBorder(Color.Cyan, 5),
          Anchor = CUIAnchor.Center,
          Absolute = new CUINullRect(w: 200, h: 200),
        };
        GlassFrame["glass"] = new CUIMagnifyingGlass();
        CUI.TopMain["MagnifyingGlass"] = GlassFrame;
      }
    }

    public override void CleanUp()
    {
      texture.Dispose();
      base.CleanUp();
    }
    Texture2D texture;
    Color[] backBuffer;


    double lastDrawn;
    public override void Draw(SpriteBatch spriteBatch)
    {
      if (Timing.TotalTime - lastDrawn > 0.05)
      {
        lastDrawn = Timing.TotalTime;

        GameMain.Instance.GraphicsDevice.GetBackBufferData<Color>(backBuffer);
        texture.SetData(backBuffer);

        texture.GetData<Color>(
          0, new Rectangle((int)Real.Left, (int)Real.Top, 40, 40), Data, 0, Data.Length
        );
        SetData();
      }




      base.Draw(spriteBatch);
    }

    public CUIMagnifyingGlass() : base()
    {


      Size = new Point(40, 40);
      SamplerState = CUI.NoSmoothing;
      Relative = new CUINullRect(0, 0, 1, 1);

      int w = GameMain.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth;
      int h = GameMain.Instance.GraphicsDevice.PresentationParameters.BackBufferHeight;

      backBuffer = new Color[w * h];

      texture = new Texture2D(GameMain.Instance.GraphicsDevice, w, h, false, GameMain.Instance.GraphicsDevice.PresentationParameters.BackBufferFormat);

    }
  }

}