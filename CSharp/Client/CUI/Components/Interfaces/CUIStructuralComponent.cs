using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;

namespace CrabUI
{
  public interface CUIStructuralComponent
  {
    public List<CUIStructuralComponent> TopChildren { get; }
    public List<CUIStructuralComponent> Children { get; }
  }
}