using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ComponentGenerator;
using Microsoft.Xna.Framework.Input;

namespace CrabUI
{
  public partial class CUITextInput
  {
    public struct TextMeasurementsStruct
    {
      public float CaretLeft;
      public float SelectionLeft;
      public float SelectionRight;
      public float SelectionWidth => SelectionRight - SelectionLeft;
    }
  }
}