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
  public partial class CUIInput
  {
    private CUIEnvironment Environment;

    public MouseInput Mouse { get; }

    public void Update(double totalTime)
    {
      Mouse.Update(totalTime, Environment.InputScanner.ScanMouse());
    }

    public CUIInput(CUIEnvironment environment, InputSettings settings)
    {
      Environment = environment;
      Mouse = new MouseInput(settings);
    }
  }
}