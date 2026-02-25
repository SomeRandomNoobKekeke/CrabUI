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
  public partial class CUIComponent
  {
    public static int MaxID { get; private set; }
    public int ID { get; set; }


    public DragHandle DragHandle;

    public CUIComponent()
    {
      ID = MaxID++;

      Access = new ComponentAccess(this);

      InjectProps();
      WireUpProps();

      Layout = new PlainLayout();

      DragHandle = new DragHandle()
      {
        Host = this,
        Active = true,
      };
    }

    private void InjectProps()
    {
      LayoutSlot.Host = Access;
      LayoutMarker.Host = Access;
      Internal.MainComponentTracker.Host = Access;

      CUIProps.Absolute.Host = Access; CUIProps.Absolute.Name = "Absolute";
      CUIProps.Relative.Host = Access; CUIProps.Relative.Name = "Relative";
      CUIProps.Rect.Host = Access; CUIProps.Rect.Name = "Rect";

    }

    public override string ToString() => $"{this.GetType().Name} [{this.ID}]";
  }
}