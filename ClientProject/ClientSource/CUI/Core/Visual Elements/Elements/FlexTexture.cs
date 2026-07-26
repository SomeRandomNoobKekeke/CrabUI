using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;
using System.Text.Json;

namespace CrabUI
{
  public class FlexTexture : VisualElementBase, IFocusRequestEventConsumer
  {
    public bool ConsumeFocus { get; set; }
    public ClearableEvent<CUIFocusRequestEvent> FocusProbed { get; }

    public override bool Contains(Vector2 pos)
    {
      throw new NotImplementedException();
    }

    public override void Draw(CUISpriteBatch spriteBatch)
    {
      throw new NotImplementedException();
    }
  }
}