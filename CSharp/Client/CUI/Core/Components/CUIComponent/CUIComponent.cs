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
  public partial class CUIComponent : CUIVisualComponent, IComponent
  {
    public class Part : IPart { public CUIComponent Self { get; set; } }

    public CUIComponent() : base()
    {
      Layout = new PlainLayout();
    }
  }
}