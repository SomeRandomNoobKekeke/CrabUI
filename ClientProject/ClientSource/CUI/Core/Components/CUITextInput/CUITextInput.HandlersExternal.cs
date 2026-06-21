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

    public void HandleMouseMoved(CUIComponent c, CUIMouseMovedEvent e)
    {

      UpdateVisualState();
    }


    public void HandleMouseUp(CUIComponent c, CUIMouseUpEvent e)
    {

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
  }
}