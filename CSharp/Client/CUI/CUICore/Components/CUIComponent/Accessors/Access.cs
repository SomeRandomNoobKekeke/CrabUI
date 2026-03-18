using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using ComponentInjector;

namespace CrabUI
{
  public partial class CUIComponent
  {
    protected Access_Part Access { get; } = new();
  }
}