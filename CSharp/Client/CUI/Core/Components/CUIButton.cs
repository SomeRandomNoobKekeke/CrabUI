using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;

namespace CrabUI
{
  public partial class CUIButton : CUIComponent, IComponent
  {
    public ICUIStyle HoveredStyle = new CUICodeStyle<CUIButton>() { ApplyAction = ApplyHoveredStyle };
    public ICUIStyle MouseDownStyle = new CUICodeStyle<CUIButton>() { ApplyAction = ApplyMouseDownStyle };
    public ICUIStyle MouseUpStyle = new CUICodeStyle<CUIButton>() { ApplyAction = ApplyMouseUpStyle };

    public static void ApplyHoveredStyle(CUIButton button)
    {
      button.Background.Color = Color.Yellow;
    }
    public static void ApplyMouseDownStyle(CUIButton button)
    {
      button.Background.Color = Color.Blue;
    }
    public static void ApplyMouseUpStyle(CUIButton button)
    {
      button.Background.Color = Color.Green;
    }

    public CUIButton() : base()
    {
      MouseDown += (e) => MouseDownStyle.Apply(this);
      MouseUp += (e) => MouseUpStyle.Apply(this);
    }

  }
}