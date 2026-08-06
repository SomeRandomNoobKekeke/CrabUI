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
  public partial class E2ETestManager : CUIPage
  {
    public ILogger Logger => CUI.Logger;


    public IE2ETest CurrentTest { get; set; }
    public E2ETestRepo Repo { get; } = new();
    public CUIVerticalList ButtonList { get; set; }

    public void RunAll()
    {
      foreach (string name in Repo.Tests.Keys)
      {
        Run(name);
      }
    }
    public void Run(string name)
    {
      if (!Repo.Tests.ContainsKey(name))
      {
        Logger.Warning($"Can't find E2E test: [{name}]");
        return;
      }

      ModStorage.Set("CUITest", name);
      Run(Repo.Tests[name]);
    }

    public void Run(Type testType)
    {
      try
      {
        CleanUp();

        CurrentTest = (IE2ETest)Activator.CreateInstance(testType);
        CurrentTest.Initialize();
      }
      catch (Exception e)
      {
        Logger.Warning($"Error in CUIE2ETest [{testType}]: {e.Message} {e.InnerException}\n{e.StackTrace}");
      }
    }

    public void Refresh()
    {
      ButtonList.Children.Clear();
      foreach (var (name, type) in Repo.Tests)
      {
        ButtonList.Add(new CUIButton()
        {
          Text = type.Name,
          Absolute = new CUINullRect(h: 30),
          MasterColor = new Color(0, 255, 255),
          OnMouseDown = (c, e) => Run(name),
        });
      }
    }

    public void CleanUp()
    {
      if (CurrentTest != null)
      {
        CurrentTest.Dispose();
        CurrentTest = null;
      }

      CUI.Main.Children.Clear();
    }

    private void CreateUI()
    {
      OnOpen.Add(Refresh);
      OnClose.Add(CleanUp);

      Background.Color = new Color(32, 32, 32);

      this["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1) };
      this["layout"]["btnlist"] = ButtonList = new CUIVerticalList() { Flex = 1 };
    }

    public E2ETestManager()
    {
      CreateUI();
    }



  }
}