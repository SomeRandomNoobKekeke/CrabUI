using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;

namespace CrabUI
{
  public partial class CUIComponent
  {
    public CUISprite BackgroundSprite
    {
      get => Background.Sprite;
      set => Background.Sprite = value;
    }

    [CUISerializable]
    public bool CullChildren { get; set; }


    public bool ConsumeMouseClicks
    {
      get => Background.ConsumeMouseClicks;
      set => Background.ConsumeMouseClicks = value;
    }
  }
}