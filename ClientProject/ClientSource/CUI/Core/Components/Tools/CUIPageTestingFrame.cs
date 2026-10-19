using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;

namespace CursedUI
{
  public partial class CUIPageTestingFrame : CUIDefault.Frame
  {
    public static CUIPageTestingFrame Instance
          => CUICore.Singletons.GetOrUpdate<CUIPageTestingFrame>(static () => new());

    public HashSet<Type> PageTypes { get; } = new();

    public CUIPages Pages { get; }

    public void OpenPage(CUIPage page)
    {
      Pages.OpenPage(page);
      Caption = $"[{page.GetType().Name}] page";
    }

    public void ScanForCUIPages()
    {
      foreach (Type T in Assembly.GetCallingAssembly().GetTypes())
      {
        if (T.IsAssignableTo(typeof(CUIPage)))
        {
          PageTypes.Add(T);
        }
      }

      Refresh();
    }

    public override void Refresh()
    {
      PageSelect.Options = PageTypes.Select(T => T.Name);
    }

    private void SelectPage(string typeName)
    {
      Pages.ClosePage();
      Type? pageType = PageTypes.FirstOrDefault(T => T.Name == typeName);

      if (pageType is not null)
      {
        OpenPage((CUIPage)Activator.CreateInstance(pageType));
      }
    }

    private CUIDropDown PageSelect;

    public CUIPageTestingFrame() : base("Page Testing Frame", 400, 600)
    {
      this["layout"]["header"] = new CUIDefault.HorizontalPanel()
      {
        CullChildren = false,
      };

      this["layout"]["main"] = Pages = new CUIPages() { Flex = 1 };

      this["layout"]["header"]["reload lua"] = new ReloadLuaButton();
      this["layout"]["header"]["pages label"] = new CUITextBlock("pages:")
      {
        Margin = new CUISizes(0, 4, 0, 4),
      };
      this["layout"]["header"]["page select"] = PageSelect = new CUIDropDown()
      {
        OnSelect = SelectPage,
        Selected = "Select Page",
      };
    }
  }
}