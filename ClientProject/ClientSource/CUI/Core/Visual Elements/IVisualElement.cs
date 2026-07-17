using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CrabUI
{
  public interface IVisualElement : IAware
  {
    public CUIRect Rect { get; }
    public bool Contains(Vector2 pos);
    public void Draw(CUISpriteBatch spriteBatch);
  }
}