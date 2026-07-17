using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public record TextInputEventPack(
    TextInputEventArgs[] TextInputEvents,
    TextInputEventArgs[] KeyDownEvents,
    bool SomethingFocusedElsewhere
  // bool MouseOnSomeVanillaGUI 
  //HACK?
  //TODO Yes, those flags should be in separate record, there's 2 of them now
  );
}