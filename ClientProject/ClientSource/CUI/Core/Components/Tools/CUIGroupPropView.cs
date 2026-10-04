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
  //Test LINK:\ClientProject\InMemory\CUITest\Snapshots\Tests\Components\CUIGroupPropView.cs
  public class CUIGroupPropView : CUIPage
  {

    private CUIVisualComponent _Root; public CUIVisualComponent Root
    {
      get => _Root;
      set
      {
        _Root = value;
        ComponentTree.Root = value;
        PropView.Target = null;
      }
    }

    public CUIComponentTreeSelect ComponentTree { get; }
    public CUIPropView PropView { get; }


    public CUIGroupPropView() : base()
    {
      this["layout"] = new CUIHorizontalList() { Relative = new CUINullRect(0, 0, 1, 1) };
      this["layout"]["component tree"] = ComponentTree = new()
      {
        Flex = 1,
        Borders = new CUISizes(right: 2),
      };
      this["layout"]["props"] = PropView = new CUIPropView()
      {
        Flex = 1,
      };

      ComponentTree.Selected += (c) => PropView.Target = c;
    }
  }
}