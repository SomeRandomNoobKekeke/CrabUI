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

namespace CrabUI
{
  public static partial class CUIDefault
  {
    public class IntField : InputField
    {
      public int Value
      {
        get => int.TryParse(Input.Text, out int i) ? i : 0;
        set => Input.Text = value.ToString();
      }

      public IntField() : base()
      {
        Validation = (s) => int.TryParse(s, out int _);
      }
    }
  }
}