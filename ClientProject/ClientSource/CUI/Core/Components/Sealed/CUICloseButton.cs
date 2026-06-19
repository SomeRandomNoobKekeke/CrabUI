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
  public partial class CUICloseButton : CUIButton, IComponent
  {
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUICloseButton>((c) =>
    {
      c.MasterColor = Color.Red;
    });

    public CUICloseButton() : base()
    {
      MouseDown += (c, e) => Commands.SendUp("close");
    }
  }
}