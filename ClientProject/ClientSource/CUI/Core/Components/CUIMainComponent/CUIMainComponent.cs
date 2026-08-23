using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using CUILibs;
using CUICodeGenerator;

namespace CrabUI
{
  [GeneratedComponent]
  public partial class CUIMainComponent : CUIComponent, IComponent
  {
    public class Part : IPart { public CUIMainComponent Self { get; set; } }

    public bool Frozen { get; set; }
    public double UpdateInterval = 1.0 / 60.0;
    public int MaxLayoutCalcItterations { get; set; } = 10;

    protected VisualFlattener VisualFlattener { get; } = new();
    protected LayoutFlattener LayoutFlattener { get; } = new();
    protected ChainDrawer ChainDrawer { get; } = new();
    protected EventDispatcher EventDispatcher { get; } = new();
    public EventTargets EventTargets { get; } = new();


    public EventConstructor EventConstructor { get; }
    public GrabbedHandleTracker GrabbedHandleTracker { get; } = new();  //BRUH should this be public?

    public CUIMainComponent(CUICore core) : base()
    {
      ChildrenBounds = CUIBoundaries.Box;
      EventConstructor = core._EventConstructor;
    }
  }
}