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
  public class __CUIGraphicsDevice : CUIGraphicsDevice
  {
    public void SetRenderTarget(CUIRenderTarget2D target)
    {
      if (target is __CUIRenderTarget2D)
      {
        GameMain.Instance.GraphicsDevice.SetRenderTarget(
          ((__CUIRenderTarget2D)target).XNARenderTarget2D
        );
      }
    }
  }
}