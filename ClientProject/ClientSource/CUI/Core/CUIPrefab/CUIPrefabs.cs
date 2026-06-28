using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public static class CUIPrefabs
  {
    public static CUIFrame Frame(string caption = "")
    {
      CUIFrame frame = new CUIFrame();



      return frame;
    }


    public static CUIComponent IntField(string key, int value)
    {
      CUIHorizontalList wrapper = new CUIHorizontalList()
      {

      };

      return wrapper;
    }
  }
}