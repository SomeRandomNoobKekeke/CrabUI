using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;
using Microsoft.Xna.Framework;
using System.IO;

namespace CrabUIUser
{
  public class E2ETestManagerUI : CUIPage
  {
    public E2ETestManagerUI(E2ETestManager manager)
    {
      Manager = manager;
      CreateUI();
    }

    public E2ETestManager Manager { get; set; }

    public CUIVerticalList ButtonList { get; set; }

    public void UpdateTests()
    {
      RemoveAllChildren();
    }


    public void HandleManagerEvent(E2ETestManager.Event e)
    {

    }

    public void CreateUI()
    {
      Manager.Events.Add(HandleManagerEvent);

      OnOpen.Add(() =>
      {
        ButtonList.RemoveAllChildren();
        foreach (var (name, type) in Manager.Repo.Tests)
        {
          ButtonList.Append(new CUIButton()
          {
            Text = type.Name,
            Absolute = new CUINullRect(h: 30),
            MasterColor = new Color(0, 255, 255),
            AddMouseDown = (c, e) => Manager.Run(name),
          });
        }
      });

      OnClose.Add(() =>
      {
        ButtonList.RemoveAllChildren();
        Manager.CleanUp();
      });

      BackgroundColor = new Color(32, 32, 32);

      this["layout"] = new CUIVerticalList()
      {
        Relative = new CUINullRect(0, 0, 1, 1),
      };
      this["layout"]["btnlist"] = ButtonList = new CUIVerticalList()
      {
        Flex = 1,
      };
    }
  }
}