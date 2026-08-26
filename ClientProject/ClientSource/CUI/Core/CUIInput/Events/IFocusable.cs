using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;

namespace CursedUI
{
  public interface IFocusable : IEventConsumer
  {
    public bool Focused { get; set; }
    public void Focus();
    public void Blur();
  }
}