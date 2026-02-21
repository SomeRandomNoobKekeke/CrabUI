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
  public interface ICUILayoutProp
  {
    public LayoutMarkPattern Pattern { get; set; }
    public ILayoutHost LayoutHost { get; set; }
  }
}