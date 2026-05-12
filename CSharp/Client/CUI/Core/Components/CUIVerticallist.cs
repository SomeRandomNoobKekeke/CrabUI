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
  public partial class CUIVerticalList : CUIComponent, IComponent
  {

    public CUIVerticalList() : base()
    {
      LayoutSlot.Layout = new CUIVerticalListLayout();
    }
  }
}