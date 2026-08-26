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

namespace CursedUI
{
  public partial class CUIFlexComponent : CUIVisualComponent
  {
    public FlexTexture FlexTexture { get; } = new FlexTexture();

    public override bool MouseOver => FlexTexture.MouseOver;
    public override bool MousePressed => FlexTexture.MousePressed;

    public override bool ConsumeMouseEvents
    {
      get => FlexTexture.ConsumeMouseEvents;
      set => FlexTexture.ConsumeMouseEvents = value;
    }

    public override bool IsPointOnTransparentPixel(Vector2 point)
      => FlexTexture.IsPointOnTransparentPixel(point);

    public override CUIRect OuterRect { get; set; }
    public override CUIRect ChildrenRect { get; set; }
    public override bool Visible { get; set; } = true;
    public override Layout? Layout { get; protected set; } = new CUIPlainLayout();

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      yield return FlexTexture.VisualWrapper;
    }

    public CUIFlexComponent() : base()
    {
      Layout.ConnectTo(new Adapters_Part.CUIPlainLayout_Host_Part() { Self = this });
    }
  }
}