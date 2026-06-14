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
      public float CaretOffsetX;
      public float CaretWidth;
      public float SelectionOffsetX;
      public float SelectionWidth;
    }
  }
}