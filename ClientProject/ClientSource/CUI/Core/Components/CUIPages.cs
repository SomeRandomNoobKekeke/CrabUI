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

    public Action<CUIComponent> OnPageOpened { set { PageOpened += value; } }
    public event Action<CUIComponent> PageOpened;
    public Action<CUIComponent> OnPageClosed { set { PageClosed += value; } }
    public event Action<CUIComponent> PageClosed;

    public bool IsOpened(CUIComponent p) => OpenedPage == p;
    public void ClosePage()
    {
      if (Children.Count > 0 && Children[0] is CUIPage)
      {
        PageClosed?.Invoke(Children[0] as CUIComponent);
        (Children[0] as CUIPage).RaiseOnClose();
      }

      Children.Clear();
    }

    [CUISerializableProp]
    public bool ResizeOpenedPage { get; set; } = true;

    /// <summary>
    /// Adds page as its only child
    /// </summary>
    public void OpenPage(CUIComponent page)
    {
      ClosePage();

      _OpenedPage = page;
      if (page is null) return;

      Children.Add(page);

      if (ResizeOpenedPage)
      {
        page.Relative = new CUINullRect(0, 0, 1, 1);
      }

      if (page is CUIPage)
      {
        (page as CUIPage).RaiseOnOpen();
      }
      PageOpened?.Invoke(page);
    }

    private RoutableCommandContract? FindNearestContract()
    {
      return Parents.FirstOrDefault(
        p => p?.CommandsContract != null
      )?.CommandsContract;
    }
  }
}