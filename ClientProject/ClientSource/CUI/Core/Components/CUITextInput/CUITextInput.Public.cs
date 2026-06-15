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
  public partial class CUITextInput : CUIComponent, IComponent
  {
    public string Text
    {
      get => State.Text;
      set => State.SetText(value);
    }

    public int SelectionStart
    {
      get => State.SelectionStart;
      set => State.SetSelectionStart(value);
    }

    public int SelectionEnd
    {
      get => State.SelectionEnd;
      set => State.SetSelectionEnd(value);
    }

    public int CaretPos
    {
      get => State.CaretPos;
      set => State.SetCaretPos(value);
    }

    public int SelectionLength => State.SelectionLength;
    public bool SelectionEmpty => State.SelectionEmpty;
  }
}