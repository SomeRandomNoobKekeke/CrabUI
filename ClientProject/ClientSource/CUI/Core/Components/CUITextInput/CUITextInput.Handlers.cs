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

    public StateClass State { get; } = new();


    #region External Events
    #endregion
    private void HandleTextInput(CUITextInputEvent e)
    {
      try
      {
        if (!Focused) return;
        HandleCharacter(e.Args.Character);
      }
      catch (Exception ex)
      {
        CUI.Logger.Error($"Error in CUITextInput: {State}\n{ex}");
      }
    }

    private void HandleKeyDownInput(CUIKeyDownInputEvent e)
    {
      try
      {
        if (!Focused) return;

        if (CUICore.Instance.Input.Keyboard.IsKeyDown(Keys.LeftControl))
        {
          HandleCtrlCommand(e.Args.Key);
          return;
        }

        if (CUICore.Instance.Input.Keyboard.IsKeyDown(Keys.LeftShift))
        {
          HandleShiftCommand(e.Args.Key);
          return;
        }

        HandleKey(e.Args.Key);
      }
      catch (Exception ex)
      {
        CUI.Logger.Error($"Error in CUITextInput: State: [{State}] key: [{e.Args.Key}] \n{ex}");
      }
    }

    public void HandleMouseDown(CUIComponent c, CUIMouseDownEvent e)
    {
      CaretPos = TextBlock.CaretIndex(e.Pos);
      UpdateVisualState();
    }

    #region Internal Events
    #endregion

    private void HandleStateChanged()
    {
      UpdaTextMeasurements();
      UpdateVisualState();
    }

    private void HandleFocus()
    {
      CaretPos = Text.Length - 1;
    }

    private void HandleFocusLost()
    {
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

    //
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