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
      public class CalculatorCore
      {
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
          Text += opp;
        }

        public void AcceptCommand(string command)
        {
          Text += command;
        }
      }
    }
  }
}