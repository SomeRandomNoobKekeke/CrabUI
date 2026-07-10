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
    public class TextField : InputField
    {
      public string Value
      {
        get => RawValue;
        set => RawValue = value;
      }
    }
  }
}