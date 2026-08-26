using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CrabUI;
using Microsoft.Xna.Framework;
using System.IO;

namespace CrabUIUser
{
  public partial class TestManager : CUIFrame
  {
    public CUIButton OpenButton { get; private set; }

    public bool IsOpen
    {
      get => this.Parent != null;
      set
      {
        if (value)
        {
          OpenButton.RemoveSelf();
          this.Open(CUI.TopMain);
          Pages.Open(SnapshotTestManager);
        }
        else
        {
          CUI.TopMain.Children.Add(OpenButton);
          this.Close();
          Pages.Dismantle(); // This should trigger dismantle on concrete page
          ModStorage.Remove("CUITest");
        }
      }
    }


    public CUIPages Pages { get; private set; }



    public void CreateUI()
    {
      OpenButton = new CUIButton()
      {
        Absolute = new CUINullRect(w: 50, h: 30),
        Anchor = CUIAnchor.RightCenter,
        Text = "Test",
        OnMouseDown = (e) => IsOpen = true,
      };

      CUI.TopMain.Children.Add(OpenButton);


      Absolute = new CUINullRect(w: 300, h: 400);
      Background.Color = new Color(32, 32, 32);
      Anchor = CUIAnchor.RightCenter;

      this["layout"] = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
      };

      this["layout"]["header"] = new CUIHorizontalList()
      {
        Direction = CUIDirection.Reverse,
        Absolute = new CUINullRect(h: 30),
        Background = { Color = new Color(32, 32, 32) },
      };

      this["layout"]["header"]["close"] = new CUIButton()
      {
        Text = "X",
        MasterColor = new Color(255, 0, 0),
        Absolute = new CUINullRect(w: 30, h: 30),
        OnMouseDown = (e) => IsOpen = false,
      };

      this["layout"]["header"]["spacer"] = new CUIComponent() { Flex = 1 };

      //It's just easier to use in console
      // this["layout"]["header"]["UTest"] = new CUIButton()
      // {
      //   Text = "UTest",
      //   Padding = new CUISizes(0, 15, 0, 15),
      //   OnMouseDown = (e) => Pages.Open(UTestManager),
      // };

      this["layout"]["header"]["reload"] = new CUIButton()
      {
        Text = "Reload lua",
        Padding = new CUISizes(0, 5, 0, 5),
        MasterColor = Color.Red,
        OnMouseDown = (e) => DebugConsole.ExecuteCommand("cl_reloadlua"),
      };

      this["layout"]["header"]["E2E"] = new CUIButton()
      {
        Text = "E2E",
        Padding = new CUISizes(0, 5, 0, 5),
        OnMouseDown = (e) => Pages.Open(E2ETestManager),
      };

      this["layout"]["header"]["snapshots"] = new CUIButton()
      {
        Text = "Snapshots",
        Padding = new CUISizes(0, 5, 0, 5),
        OnMouseDown = (e) => Pages.Open(SnapshotTestManager),
      };



      this["layout"]["main"] = Pages = new CUIPages()
      {
        Flex = 1,
      };



    }


  }
}