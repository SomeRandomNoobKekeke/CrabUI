using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public enum CUITextAlign { Start, Center, End, }
  public enum CUISide { Top, Right, Bottom, Left, }
  public enum CUIDirection { Straight, Reverse, }
  public enum CUIMouseEvent { Down, DClick }
}