using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class CUIPages : CUIComponent, IComponent
  {
    private CUIComponent _OpenedPage; public CUIComponent OpenedPage
    {
      get => _OpenedPage;
      set
      {
        OpenPage(value);
      }
    }

    public bool IsOpened(CUIComponent p) => OpenedPage == p;
    public void ClosePage()
    {
      if (Children.Count > 0 && Children[0] is CUIPage)
      {
        (Children[0] as CUIPage).RaiseOnClose();
      }

      Children.Clear();
    }

    /// <summary>
    /// Adds page as its only child
    /// </summary>
    public void OpenPage(CUIComponent page)
    {
      ClosePage();

      _OpenedPage = page;
      if (page is null) return;

      Children.Add(page);

      page.Relative = new CUINullRect(0, 0, 1, 1);

      if (page is CUIPage)
      {
        (page as CUIPage).RaiseOnOpen();
      }
    }

    private RoutableCommandContract? FindNearestContract()
    {
      return Parents.FirstOrDefault(
        p => p?.CommandsContract != null
      )?.CommandsContract;
    }
  }
}