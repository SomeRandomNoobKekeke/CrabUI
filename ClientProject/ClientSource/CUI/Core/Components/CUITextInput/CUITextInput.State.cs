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
    public class StateClass
    {
      public event Action Changed;

      public string Text { get; private set; }
      public int SelectionStart { get; private set; }
      public int SelectionEnd { get; private set; }
      public int CaretPos { get; private set; }


      public int SelectionLength => SelectionEnd - SelectionStart;
      public bool SelectionEmpty => SelectionLength <= 0;

      public void SetText(string value)
      {
        Text = value;

        CaretPos = Math.Clamp(CaretPos, 0, Text.Length);
        SelectionStart = Math.Clamp(SelectionStart, 0, Text.Length);
        SelectionEnd = Math.Clamp(SelectionEnd, 0, Text.Length);

        Changed?.Invoke();
      }

      public void SetSelectionStart(int value) => SetSelection(value, SelectionEnd);
      public void SetSelectionEnd(int value) => SetSelection(SelectionStart, value);

      public void SetSelection(int start, int end)
      {
        end = Math.Max(start, end);

        start = Math.Clamp(start, 0, Text.Length);
        end = Math.Clamp(end, 0, Text.Length);

        SelectionStart = start;
        SelectionEnd = end;

        Changed?.Invoke();
      }

      public void SetCaretPos(int value)
      {
        CaretPos = Math.Clamp(value, 0, Text.Length);
        Changed?.Invoke();
      }

      public override string ToString() => $"Text: [{Text}] SelectionStart: [{SelectionStart}] SelectionEnd: [{SelectionEnd}] CaretPos: [{CaretPos}]";
    }




  }
}