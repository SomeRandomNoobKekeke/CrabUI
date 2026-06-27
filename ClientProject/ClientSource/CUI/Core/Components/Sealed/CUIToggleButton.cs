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
  public partial class CUIToggleButton : CUIButtonBase, IComponent
  {

    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIToggleButton>((c) =>
    {
      c.MasterColor = Color.Blue;
    });

    [CUISerializable]
    public Color OnColor { get; set; } = new Color(0, 255, 255);

    [CUISerializable]
    public Color OffColor { get; set; } = new Color(0, 0, 255);



    private bool _State; public bool State
    {
      get => _State;
      set
      {
        _State = value;
        DetermineColor();
      }
    }

    public override Color MasterColor
    {
      set
      {
        OnColor = value.Multiply(0.9f);
        OffColor = value.Multiply(0.5f);
        DetermineColor();
      }
    }

    public override void DetermineColor()
    {
      if (State)
      {
        Background.Color = OnColor;
        if (MouseOver) Background.Color = OnColor.Multiply(2.0f);
        if (MousePressed) Background.Color = OnColor.Multiply(3.0f);
      }
      else
      {
        Background.Color = OffColor;
        if (MouseOver) Background.Color = OffColor.Multiply(2.0f);
        if (MousePressed) Background.Color = OffColor.Multiply(3.0f);
      }
    }

    public CUIToggleButton() : base()
    {
      MouseDown += (c, e) => State = !State;
    }
    public CUIToggleButton(string text) : this()
    {
      Text = text;
    }
  }
}