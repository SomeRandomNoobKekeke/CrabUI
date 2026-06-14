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
    public struct StateStruct
    {
      public string Text;
      public int SelectionStart;
      public int SelectionEnd;
      public int CaretPos;

      public int SelectionLength => SelectionEnd - SelectionStart;
      public bool SelectionEmpty => SelectionLength <= 0;
    }
  }
}