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

namespace CUITests
{
  public partial class SnapshotTests
  {
    public static partial class Dynamic
    {
      public static CUIComponent Debouncer()
      {
        var frame = new CUIDefault.Frame("Debouncer", 400, 600);

        CUITextBlock messageBlock = new CUITextBlock("i hope no one will type anything today");

        frame["layout"]["message"] = messageBlock;
        frame["layout"]["input"] = new CUITextInput()
        {
          OnInput = (text) =>
          {
            messageBlock.Text = "omg he's typing again...";

            CUICore.Debounce(() => messageBlock.Text = "are you done typing?", 1000);
            CUICore.Debounce(() => messageBlock.Text = "finaly, he's gone", 3000);
          },
        };

        return frame;
      }
    }
  }
}