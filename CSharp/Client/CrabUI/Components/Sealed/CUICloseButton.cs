using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;
using Barotrauma.Extensions;
namespace CrabUI
{
  // hmm, idk if this should be a prefab or component
  // it's too small for component
  // but in prefab i can't use initializer
  public class CUICloseButton : CUIButton
  {
    public CUICloseButton() : base()
    {
      Command = "Close";
      Text = "";
      ZIndex = 10;
      BackgroundSprite = CUI.TextureManager.GetCUISprite(3, 1);
      Absolute = new CUINullRect(0, 0, 15, 15);
    }

  }
}