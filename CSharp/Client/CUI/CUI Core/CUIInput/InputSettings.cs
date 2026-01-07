using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public class InputSettings
  {
    public double DoubleClickInterval = 0.2;
    public float ScrollSpeed = 0.6f;
    public double ClickInterval = 0.2;
  }
}