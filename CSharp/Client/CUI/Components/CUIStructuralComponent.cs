using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;

namespace CrabUI
{
  public partial class CUIStructuralComponent : CUIComponentCore
  {
    public List<CUIStructuralComponent> TopChildren { get; } = new();
    public List<CUIStructuralComponent> Children { get; } = new();
  }
}