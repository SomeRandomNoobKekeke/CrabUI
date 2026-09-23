using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;
using Barotrauma.Extensions;

namespace CursedUI
{
  //Test LINK:\ClientProject\InMemory\CUITest\Snapshots\Tests\Components\CUICheckBox.cs
  public partial class CUICheckBox : CUIToggleIconButton, IComponent
  {
    protected override void InitStyle()
    {
      base.InitStyle();
      Icon = CUISprite.CheckIcon;
      // Padding = new CUISizes(1, 1, 1, 1);
      Borders.Sizes = new CUISizes(1, 1, 1, 1);
      ClickSound = GUISoundType.TickBox;
    }
  }
}