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

    public void Update()
    {
      UpdateMouse();
    }

    public void AttachToEnvironment(CUIEnvironment environment)
    {
      Environment = environment;
      Environment.LifeCycle.Update += Update;
    }
  }
}