using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CursedUI
{

  [GeneratedComponent]
  public partial class CUIComponent : CUIVisualComponent, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUIComponent>((c) =>
    {
      c.Borders.Color = c.Palette["border"];
    });

    protected override void InitStyle()
    {
      Background.Sprite = CUISprite.Transparent;
    }

    public CUIComponent() : base()
    {
      if (!_IsDebugTool) Debug_ComponentCreated.Send(this);
    }

    public class Part : IPart { public CUIComponent Self { get; set; } }
  }
}