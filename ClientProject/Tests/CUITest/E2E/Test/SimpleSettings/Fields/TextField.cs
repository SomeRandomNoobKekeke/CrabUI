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
      public class TextFieldOld : InputFieldOld
      {
        public string Value
        {
          get => RawValue;
          set => RawValue = value;
        }
      }
    }
  }
}