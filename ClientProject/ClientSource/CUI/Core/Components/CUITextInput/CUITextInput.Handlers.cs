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
    private void HandleStateChanged()
    {
      UpdaTextMeasurements();
      UpdateVisualState();
    }

    private void HandleCharacter(char c)
    {
      Text = Text.Insert(CaretPos, c.ToString());
      CaretPos++;
    }

    private void HandleCtrlCommand(Keys key)
    {
      if (key == Keys.A)
      {
        SelectAll();
      }
    }

    private void HandleShiftCommand(Keys key)
    {
      if (key == Keys.Left)
      {
        HandleSelectionExpansionLeft();
      }

      if (key == Keys.Right)
      {
        HandleSelectionExpansionRight();
      }
    }

    private void HandleSelectionExpansionLeft()
    {
      if (!SomethingSelected) SetSelection(CaretPos, CaretPos);

      if (SelectionStart == CaretPos && CaretPos == SelectionEnd)
      {
        CaretPos--;
        SelectionStart = CaretPos;
        return;
      }

      if (CaretPos > SelectionEnd)
      {
        CaretPos--;
        return;
      }

      if (CaretPos == SelectionEnd)
      {
        CaretPos--;
        SelectionEnd = CaretPos;
        return;
      }

      if (CaretPos < SelectionEnd)
      {
        CaretPos--;
        SelectionStart = Math.Min(CaretPos, SelectionStart);
        return;
      }
    }

    private void HandleSelectionExpansionRight()
    {
      if (!SomethingSelected) SetSelection(CaretPos, CaretPos);

      if (SelectionStart == CaretPos && CaretPos == SelectionEnd)
      {
        CaretPos++;
        SelectionEnd = CaretPos;
        return;
      }

      if (CaretPos < SelectionStart)
      {
        CaretPos++;
        return;
      }

      if (CaretPos == SelectionStart)
      {
        CaretPos++;
        SelectionStart = CaretPos;
        return;
      }

      if (CaretPos > SelectionStart)
      {
        CaretPos++;
        SelectionEnd = Math.Max(CaretPos, SelectionEnd);
        return;
      }
    }

    private void HandleKey(Keys key)
    {
      if (key == Keys.Back)
      {
        if (!SomethingSelected)
        {
          RemoveLeftChar();
        }
        else
        {
          RemoveSelection();
        }
      }

      if (key == Keys.Delete)
      {
        if (!SomethingSelected)
        {
          RemoveRightChar();
        }
        else
        {
          RemoveSelection();
        }
      }

      if (key == Keys.Left)
      {
        CaretPos--;
        ClearSelection();
      }
      if (key == Keys.Right)
      {
        CaretPos++;
        ClearSelection();
      }
    }

    private void SelectAll()
    {
      State.SetSelection(0, Text.Length);
    }

    private void RemoveLeftChar()
    {
      if (CaretPos == 0) return;

      CaretPos = CaretPos - 1;
      Text = Text.Remove(CaretPos, 1);
    }

    private void RemoveRightChar()
    {
      if (CaretPos == Text.Length) return;

      Text = Text.Remove(CaretPos, 1);
    }

    private void RemoveSelection()
    {
      Text = Text.Remove(SelectionStart, SelectionLength);
      CaretPos = SelectionStart;
      ClearSelection();
    }

  }
}