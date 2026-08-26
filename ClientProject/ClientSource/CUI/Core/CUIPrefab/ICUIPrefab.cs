using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  public interface ICUIPrefab
  {
    public CUIVisualComponent Instantiate();
  }
}