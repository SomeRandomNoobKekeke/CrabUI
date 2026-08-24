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
using Microsoft.Xna.Framework.Input;
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

        public CalculatorUI(CalculatorCore core) : base("Calc")
        {
          Core = core;
          Core.Changed += Refresh;

          this.KeyDownInput += HandleKeyInput;
          this.TextInput += HandleInput;

          Focusable = true;
          OnFocus += () => this["layout"]["handle"].As<CUIComponent>().Background.Color = new Color(0, 0, 200);
          OnBlur += () => this["layout"]["handle"].As<CUIComponent>().Background.Color = new Color(0, 0, 64);


          Absolute = new CUINullRect(w: 400, h: 600);
          AbsoluteMin = new(w: 100, h: 200);

          Commands.ListenFor<string>("number", (s) => Core.AcceptNumber(s));
          Commands.ListenFor<string>("opp", (s) => Core.AcceptOpperation(s));
          Commands.ListenFor<string>("command", (s) => Core.AcceptCommand(s));


          this["layout"]["header"] = new CUIHorizontalList()
          {
            Absolute = new CUINullRect(h: 50),
          };
          this["layout"]["header"]["text"] = new CUITextBlock()
          {
            TextAnchor = CUIAnchor.LeftCenter,
          };

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

          using (new CUIContextStyle<CUIButton>(c =>
          {
            // c.Background.Sprite = CUISprite.Outlined;
            c.Borders.Color = Color.Black * 0.5f;
            c.Borders.Sizes = new CUISizes(1, 1, 1, 1);
            c.Borders.Visible = true;

            c.PlaySound = false;

            c.Commands.ListenFor<string>("command", (s) =>
            {
              if (c.Text == s) c.Click();
            });
          }))
          {
            using (new CUIContextStyle<CUIButton>(c => c.Emit = "number"))
            {
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
            }

            using (new CUIContextStyle<CUIButton>(c => c.Emit = "opp"))
            {
              this["layout"]["controls"]["+"] = new CUIButton("+") { Grid = (4, 2) };
              this["layout"]["controls"]["-"] = new CUIButton("-") { Grid = (4, 3) };
              this["layout"]["controls"]["*"] = new CUIButton("*") { Grid = (1, 4) };
              this["layout"]["controls"]["/"] = new CUIButton("/") { Grid = (3, 4) };
            }

            using (new CUIContextStyle<CUIButton>(c => c.Emit = "command"))
            {
              this["layout"]["controls"]["<"] = new CUIButton("<") { Grid = (4, 1) };
              this["layout"]["controls"]["="] = new CUIButton("=") { Grid = (4, 4) };
            }
          }

          this.DeepPalette = CUIPalette.FromColor(Color.Blue);
        }


        public void HandleKeyInput(CUIKeyDownInputEvent e)
        {
          if (e.Args.Key == Keys.Back) Commands.SendDown("command", "<");
          if (e.Args.Key == Keys.Enter) Commands.SendDown("command", "=");
        }
        public void HandleInput(CUITextInputEvent e)
        {
          string command = e.Args.Character.ToString();

          if (command == "=") return;
          Commands.SendDown("command", command);
        }
      }
    }
  }
}