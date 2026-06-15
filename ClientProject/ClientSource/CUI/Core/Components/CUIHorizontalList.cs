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
  public partial class CUIHorizontalList : CUIComponent, IComponent
  {
    public void Clear() => RemoveAllChildren();
    public void Add(CUIComponent child) => Append(child);

    public CUIHorizontalList() : base()
    {
      LayoutSlot.Layout = new CUIHorizontalListLayout();
    }
  }
}