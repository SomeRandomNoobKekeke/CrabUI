using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;


namespace CursedUI
{
  public enum ErrorHandlingStrategy
  {
    FailFast, ForgiveNonCritical
  }
}