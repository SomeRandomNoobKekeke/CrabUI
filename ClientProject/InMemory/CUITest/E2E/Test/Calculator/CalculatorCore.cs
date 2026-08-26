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
  public partial class E2ETestPack
  {
    public partial class Calculator : IE2ETest
    {
      public class CalculatorCore
      {

        public Random Random = new Random();

        public event Action Changed;
        private string _Text; public string Text
        {
          get => _Text;
          set
          {
            _Text = value;
            Changed?.Invoke();
          }
        }

        public void AcceptNumber(string number)
        {
          Text += number;
        }

        public void AcceptOpperation(string opp)
        {
          Text += $" {opp} ";
        }

        public void AcceptCommand(string command)
        {
          if (command == "<")
          {
            if (Text.Length == 0) return;
            Text = Text.Substring(0, Text.Length - 1);
          }

          if (command == "=")
          {
            Text = $"mb [{Random.NextSingle() * 100}]?";
          }
        }
      }
    }
  }
}