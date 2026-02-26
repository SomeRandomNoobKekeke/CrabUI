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
  public partial class CUIComponent : IComponent
  {
    public static int MaxID { get; private set; }
    public int ID { get; set; }


    public DragHandle DragHandle;

    public CUIComponent()
    {
      ID = MaxID++;

      ProtectedAccess = new ProtectedLayerAccess(this);

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
      LayoutSlot.Host = ProtectedAccess;
      LayoutMarker.Host = ProtectedAccess;
      MainComponentTracker.Host = ProtectedAccess;

      CUIProps.Absolute.Host = ProtectedAccess; CUIProps.Absolute.Name = "Absolute";
      CUIProps.Relative.Host = ProtectedAccess; CUIProps.Relative.Name = "Relative";
      CUIProps.Rect.Host = ProtectedAccess; CUIProps.Rect.Name = "Rect";

    }

    public override string ToString() => $"{this.GetType().Name} [{this.ID}]";
  }
}