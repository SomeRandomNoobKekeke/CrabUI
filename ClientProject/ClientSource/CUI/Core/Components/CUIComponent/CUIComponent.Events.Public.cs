using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
using Microsoft.Xna.Framework.Input;


namespace CrabUI
{
  public partial class CUIComponent
  {
    public override bool MouseOver => Background.MouseOver;
    public override bool MousePressed => Background.MousePressed;

    public bool ConsumeMouseEvents
    {
      get => Background.ConsumeMouseEvents;
      set => Background.ConsumeMouseEvents = value;
    }
  }
}