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
    public event Action<CUISpriteBatch> DrawBeforeGUI;
    public event Action<CUISpriteBatch> DrawAfterGUI;
    public event Action Update;


    public void RaiseDrawBeforeGUI(CUISpriteBatch spriteBatch) => DrawBeforeGUI?.Invoke(spriteBatch);
    public void RaiseDrawAfterGUI(CUISpriteBatch spriteBatch) => DrawAfterGUI?.Invoke(spriteBatch);
    public void RaiseUpdate() => Update?.Invoke();
  }
}