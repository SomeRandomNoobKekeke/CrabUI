using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ComponentGenerator;
using Barotrauma.Extensions;

namespace CrabUI
{
  public partial class CUICheckBox : CUIComponent, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUICheckBox>(c =>
    {

    });

    public CUISprite OnSprite { get; set; }
    public CUISprite OffSprite { get; set; }


    public void Toggle() => State = !State;
    private bool _State; public bool State
    {
      get => _State;
      set
      {
        _State = value;
        Background.Sprite = value ? OnSprite : OffSprite;
      }
    }


    public CUICheckBox() : base()
    {
      OnSprite = CUIDefaultSprite.AtPos(0, 1);
      OnSprite.Color = Color.Lime;

      OffSprite = CUIDefaultSprite.AtPos(1, 1);
      OffSprite.Color = Color.Red;

      State = false;
      MouseDown += (c, e) => Toggle();
      ConsumeMouseClicks = true;
    }
  }
}