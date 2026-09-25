using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CUILibs;
using Microsoft.Xna.Framework;

namespace CursedUI
{
  //Test LINK:\ClientProject\InMemory\CUITest\Snapshots\Tests\Components\CUISubComponentSelect.cs
  public class CUISubComponentSelect : CUIVerticalList
  {

    private CUIVisualComponent _Target; public CUIVisualComponent Target
    {
      get => _Target;
      set
      {
        _Target = value;
        Refresh();
      }
    }

    public override void Refresh()
    {
      Clear();
      if (Target is null) return;

      List<CUIComponent> toAdd = new();

      void AddChildrenRec(CUIVisualComponent parent, int depth)
      {
        toAdd.Add(new CUIButton(parent.ToString())
        {
          TextAnchor = CUIAnchor.LeftCenter,
          Margin = new CUISizes(left: depth * 20),
        });

        foreach (CUIVisualComponent child in parent.Children)
        {
          AddChildrenRec(child, depth + 1);
        }
      }

      AddChildrenRec(Target, 0);

      foreach (CUIComponent child in toAdd)
      {
        Add(child);
      }
    }

    public CUISubComponentSelect() : base()
    {
      Scrollable = true;
    }
  }
}