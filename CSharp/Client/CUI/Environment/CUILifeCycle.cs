using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;

namespace CrabUI
{
  public class CUILifeCycle
  {
    public event Action<CUISpriteBatch> BeforeDraw;
    public event Action<CUISpriteBatch> AfterDraw;

    public void RaiseBeforeDraw(CUISpriteBatch spriteBatch) => BeforeDraw?.Invoke(spriteBatch);
    public void RaiseAfterDraw(CUISpriteBatch spriteBatch) => AfterDraw?.Invoke(spriteBatch);
  }
}