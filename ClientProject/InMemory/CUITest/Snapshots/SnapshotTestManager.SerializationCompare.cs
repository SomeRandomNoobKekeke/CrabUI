using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;
using System.IO;

namespace CursedUIUser
{
  public partial class SnapshotTestManager : CUIPage
  {
    public class SerializationCompareFrame : CUIDefault.Frame
    {
      public CUIVisualComponent Root
      {
        get => Compare.Component;
        set => Compare.Component = value;
      }

      public CUIVisualComponent ComponentBefore
      {
        get => Compare.ComponentBefore;
        set => Compare.ComponentBefore = value;
      }

      public CUIVisualComponent ComponentAfter
      {
        get => Compare.ComponentAfter;
        set => Compare.ComponentAfter = value;
      }

      public CUISerializationCompare Compare { get; }

      public SerializationCompareFrame() : base("Serialization Compare")
      {
        Absolute = new CUINullRect(50, 0, 600, 800);
        TargetMainComponent = CUI.TopMain;
        Anchor = CUIAnchor.LeftTop;
        this["layout"]["compare"] = Compare = new CUISerializationCompare() { Flex = 1 };
      }
    }
  }
}