using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;
using Barotrauma.Extensions;

namespace CUITests
{
  public partial class E2ETestPack
  {
    public partial class SimpleSettings
    {
      public class IntFieldOld : InputFieldOld
      {
        public int Value
        {
          get => int.TryParse(Input.Text, out int i) ? i : 0;
          set => Input.Text = value.ToString();
        }

        public IntFieldOld() : base()
        {
          Validation = (s) => int.TryParse(s, out int _);
        }
      }
    }
  }
}