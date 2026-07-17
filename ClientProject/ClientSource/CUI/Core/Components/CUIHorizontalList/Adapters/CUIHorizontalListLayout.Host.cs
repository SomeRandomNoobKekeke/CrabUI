using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;

namespace CrabUI
{
  public partial class CUIHorizontalList : CUIComponent, IComponent
  {
    protected partial class CUIHorizontalListLayout_Host_Adapter_Part : Adapters_Part.PlainLayout_Host_Part, CUIHorizontalListLayout.Host
    {
      private CUIHorizontalList _Self; public new CUIHorizontalList Self
      {
        get => _Self;
        set
        {
          _Self = value;
          base.Self = value;
        }
      }

      CUIDirection CUIHorizontalListLayout.Host.Direction => Self.LayoutProps.Direction.Value;
    }
  }
}