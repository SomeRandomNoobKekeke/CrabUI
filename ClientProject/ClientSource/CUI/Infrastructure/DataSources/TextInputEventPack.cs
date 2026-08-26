using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CursedUI
{
  public record TextInputEventPack(
    TextInputEventArgs[] TextInputEvents,
    TextInputEventArgs[] KeyDownEvents
  );
}