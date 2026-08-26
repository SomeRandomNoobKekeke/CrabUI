using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CursedUI
{
  public class __CUIGUI : CUIGUI // :BaroDev:
  {
    public SamplerState SamplerState => GUI.SamplerState;
    public RasterizerState RasterizerState => GameMain.ScissorTestEnable;

    public void DrawLine(CUISpriteBatch spriteBatch, Vector2 start, Vector2 end, float width, Color color)
    {
      try
      {
        if (spriteBatch is __CUISpriteBatch)
        {
          GUI.DrawLine(((__CUISpriteBatch)spriteBatch).XNASpriteBatch, start, end, color, width: width);
        }
      }
      catch (Exception e)
      {
        CUI.Logger.Warning(e);
      }
    }
  }
}