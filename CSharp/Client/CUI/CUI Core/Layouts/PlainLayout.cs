using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class PlainLayout : Layout
  {


    public override void UpdateChildren()
    {





      RequireChildrenUpdate = false;
    }

    public override void UpdateParent()
    {
      RequireParentUpdate = false;
    }

    public PlainLayout(CUIVisualComponent host) : base(host) { }
  }
}