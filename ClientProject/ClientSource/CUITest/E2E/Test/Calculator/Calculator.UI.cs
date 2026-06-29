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
  public partial class E2ETestPack
  {
    public partial class Calculator : IE2ETest
    {
      public partial class CalculatorUI : CUIDefault.Frame
      {
        public CalculatorCore Core { get; }

        public void Refresh()
        {
          this.Get<CUITextBlock>("layout.header.text").Text = Core.Text;
        }

        public CalculatorUI(CalculatorCore core) : base()
        {
          Core = core;
          Core.Changed += Refresh;

          Commands.ListenFor<string>("number", (s) => Core.AcceptNumber(s));
          Commands.ListenFor<string>("opp", (s) => Core.AcceptOpperation(s));
          Commands.ListenFor<string>("command", (s) => Core.AcceptCommand(s));

          Caption.Text = "Calc";
          this["layout"]["header"] = new CUIHorizontalList()
          {
            Absolute = new CUINullRect(h: 50),
            Background = { Color = Color.Red },
          };
          this["layout"]["header"]["text"] = new CUITextBlock();

          this["layout"]["controls"] = new CUIGrid()
          {
            Flex = 1,
            Background = { Color = Color.Blue },

            RowSizes = new()
            {
              new CUIGridLayout.Line(fraction:1),
              new CUIGridLayout.Line(fraction:1),
              new CUIGridLayout.Line(fraction:1),
              new CUIGridLayout.Line(fraction:1),
            },
            ColumnSizes = new()
            {
              new CUIGridLayout.Line(fraction:1),
              new CUIGridLayout.Line(fraction:1),
              new CUIGridLayout.Line(fraction:1),
              new CUIGridLayout.Line(fraction:2),
            }
          };


          CUICore.Styles.EnterContext<CUIButton>(c => c.Emit = "number");
          this["layout"]["controls"]["1"] = new CUIButton("1") { Grid = (1, 1) };
          this["layout"]["controls"]["2"] = new CUIButton("2") { Grid = (2, 1) };
          this["layout"]["controls"]["3"] = new CUIButton("3") { Grid = (3, 1) };

          this["layout"]["controls"]["4"] = new CUIButton("4") { Grid = (1, 2) };
          this["layout"]["controls"]["5"] = new CUIButton("5") { Grid = (2, 2) };
          this["layout"]["controls"]["6"] = new CUIButton("6") { Grid = (3, 2) };

          this["layout"]["controls"]["7"] = new CUIButton("7") { Grid = (1, 3) };
          this["layout"]["controls"]["8"] = new CUIButton("8") { Grid = (2, 3) };
          this["layout"]["controls"]["9"] = new CUIButton("9") { Grid = (3, 3) };

          this["layout"]["controls"]["0"] = new CUIButton("0") { Grid = (2, 4) };

          CUICore.Styles.EnterContext<CUIButton>(c => c.Emit = "opp");
          this["layout"]["controls"]["+"] = new CUIButton("+") { Grid = (4, 2) };
          this["layout"]["controls"]["-"] = new CUIButton("-") { Grid = (4, 3) };
          this["layout"]["controls"]["*"] = new CUIButton("*") { Grid = (1, 4) };
          this["layout"]["controls"]["/"] = new CUIButton("/") { Grid = (3, 4) };

          CUICore.Styles.EnterContext<CUIButton>(c => c.Emit = "command");
          this["layout"]["controls"]["<"] = new CUIButton("<") { Grid = (4, 1) };
          this["layout"]["controls"]["="] = new CUIButton("=") { Grid = (4, 4) };
          CUICore.Styles.ExitContext();
        }
      }
    }
  }
}