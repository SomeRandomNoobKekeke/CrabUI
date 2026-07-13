using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUIPages : CUIComponent, IComponent
  {
    public CUIComponent OpenedPage;

    public bool IsOpened(CUIComponent p) => OpenedPage == p;
    public void Dismantle()
    {
      if (Children.Count > 0 && Children[0] is CUIPage)
      {
        (Children[0] as CUIPage).OnClose.Raise();
      }

      Children.Clear();
    }

    /// <summary>
    /// Adds page as its only child
    /// </summary>
    public void Open(CUIComponent page)
    {
      Dismantle();

      Children.Add(page);

      page.Relative = new CUINullRect(0, 0, 1, 1);
      OpenedPage = page;

      if (page is CUIPage)
      {
        (page as CUIPage).OnOpen.Raise();
      }
    }
  }
}