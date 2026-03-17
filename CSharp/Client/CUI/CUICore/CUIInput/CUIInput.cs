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

    private InputSettings InputSettings;

    public MouseInput Mouse { get; }
    public bool SomethingHappened => Mouse.SomethingHappened;

    public void Update(double totalTime, MouseState mouse)
    {
      Mouse.Update(totalTime, mouse);
    }

    public CUIInput(InputSettings inputSettings)
    {
      InputSettings = inputSettings;
      Mouse = new MouseInput(inputSettings);
    }
  }
}