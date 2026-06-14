using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public record TextInputEventPack(
    TextInputEventArgs[] TextInputEvents,
    TextInputEventArgs[] KeyDownEvents,
    bool SomethingFocusedElsewhere
  );
}