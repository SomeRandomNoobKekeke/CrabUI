using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public interface ICUIPrefab
  {
    public CUIComponent Instantiate();
  }
}