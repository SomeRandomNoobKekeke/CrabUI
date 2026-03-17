using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;
using ComponentInjector;
namespace CrabUI
{
  public partial class CUIMainComponent
  {
    protected override IVisualComponent AsVisualComponent => VisualRepresentation;
    protected VisualRepresentation_Part VisualRepresentation { get; } = new();
    public class VisualRepresentation_Part : Part, IVisualComponent
    {
      public IEnumerable<VisualUnit> VisualSplit()
      {
        yield break;
      }
    }
  }
}