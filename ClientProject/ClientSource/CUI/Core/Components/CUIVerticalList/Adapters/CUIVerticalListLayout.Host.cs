using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using ComponentGenerator;
using BaroJunk;

namespace CrabUI
{
  public partial class CUIVerticalList : CUIComponent, IComponent
  {
    //GIGACRINGE (but at least it works, and gods of c# accesibility domains are not enraged)
    protected partial class CUIVerticalListLayout_Host_Adapter_Part : Adapters_Part.PlainLayout_Host_Part, CUIVerticalListLayout.Host
    {
      private CUIVerticalList _Self; public new CUIVerticalList Self
      {
        get => _Self;
        set
        {
          _Self = value;
          base.Self = value;
        }
      }

      CUIDirection CUIVerticalListLayout.Host.Direction => Self.LayoutProps.Direction.Value;

      float CUIVerticalListLayout.Host.TotalHeight { set => Self.UpdateChildrenOffsetBounds(value); }
    }
  }
}