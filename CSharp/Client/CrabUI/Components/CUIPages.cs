using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Container for other components  
  /// Can have only 1 child  
  /// Sets component as it's only child when you open it (as a page)
  /// </summary>
  public class CUIPages : CUIComponent
  {
    public CUIComponent OpenedPage;

    public bool IsOpened(CUIComponent p) => OpenedPage == p;

    /// <summary>
    /// Adds page as its only child
    /// </summary>
    /// <param name="page"></param>
    public void Open(CUIComponent page)
    {
      RemoveAllChildren();
      Append(page);
      page.Relative = new CUINullRect(0, 0, 1, 1);
      OpenedPage = page;
    }

    public CUIPages() : base()
    {
      BackgroundColor = Color.Transparent;
      Border.Color = Color.Transparent;
      CullChildren = false;
    }
  }
}