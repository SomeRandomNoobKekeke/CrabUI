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

        HandleKey(e.Args.Key);

        if (CUICore.Instance.Input.Keyboard.IsKeyDown(Keys.LeftControl))
        {
          HandleCtrlCommand(e.Args.Key);
        }

        if (CUICore.Instance.Input.Keyboard.IsKeyDown(Keys.LeftShift))
        {
          HandleShiftCommand(e.Args.Key);
        }
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
      Text = State.Text + c;
      CaretPos = State.CaretPos + 1;
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
          RemoveLeftChar();
        }
        else
        {
          RemoveSelection();
        }
      }

      if (key == Keys.Delete)
      {
        if (State.SelectionEmpty)
        {
          RemoveRightChar();
        }
        else
        {
          RemoveSelection();
        }
      }

      if (key == Keys.Left) CaretPos--;
      if (key == Keys.Right) CaretPos++;
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
    }

  }
}