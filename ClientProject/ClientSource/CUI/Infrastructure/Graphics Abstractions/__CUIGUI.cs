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
  public class __CUIGUI : CUIGUI // :BaroDev:
  {
    public SamplerState SamplerState => GUI.SamplerState;
    public RasterizerState RasterizerState => GameMain.ScissorTestEnable;
  }
}