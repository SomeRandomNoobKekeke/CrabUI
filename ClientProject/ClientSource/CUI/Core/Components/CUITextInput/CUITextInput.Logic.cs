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
    private StateStruct _State;
    public StateStruct State
    {
      get => _State;
      set
      {
        _State = value;
        UpdaTextMeasurements();
        UpdateVisualState();
      }
    }

    private void HandleFocus()
    {
      State = State with
      {
        CaretPos = State.Text.Length - 1,
      };
    }

    private void HandleFocusLost()
    {
      UpdateVisualState();
    }

    private void HandleCharacter(char c)
    {
      State = State with
      {
        Text = State.Text + c,
        CaretPos = State.CaretPos + 1,
      };
    }

    private void HandleCommand(Keys key)
    {
      if (key == Keys.A)
      {
        SelectAll();
      }
    }

    private void HandleKey(Keys key)
    {
      if (key == Keys.Back)
      {
        if (State.SelectionEmpty)
        {
          Remove1Char();
        }
        else
        {
          RemoveSelection();
        }
      }

      if (key == Keys.Left)
      {
        State = State with
        {
          CaretPos = State.CaretPos - 1,
        };
      }
    }

    private void SelectAll()
    {
      State = State with
      {
        SelectionStart = 0,
        SelectionEnd = State.Text.Length,
      };
    }

    private void Remove1Char()
    {
      if (State.CaretPos == 0) return;

      State = State with
      {
        Text = Text.Remove(State.CaretPos, 1),
        CaretPos = State.CaretPos - 1,
      };
    }

    private void RemoveSelection()
    {
      State = State with
      {
        Text = Text.Remove(State.SelectionStart, State.SelectionLength),
        CaretPos = State.SelectionStart,
      };
    }

  }
}