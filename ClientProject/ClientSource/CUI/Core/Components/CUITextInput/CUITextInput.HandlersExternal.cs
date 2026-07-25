using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CUICodeGenerator;
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

        if (CUICore.Input.Keyboard.IsKeyDown(Keys.LeftControl))
        {
          HandleCtrlCommand(e.Args.Key);
          return;
        }

        if (CUICore.Input.Keyboard.IsKeyDown(Keys.LeftShift))
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

    public void HandleDoubleClick(CUIComponent c, CUIMouseDoubleClickEvent e)
    {
      SelectAll();
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
      MainComponent?.GlobalEvents.AfterUpdate.Add(HandleUpdate);
      UpdateVisualState();
      LastSomethingHappenedTime = Timing.TotalTime;
    }

    private void HandleFocusLost()
    {
      MainComponent?.GlobalEvents.AfterUpdate.Remove(HandleUpdate);
      ClearSelection();
      UpdateVisualState();
    }

    private void HandleUpdate()
    {
      CaretIsHidden = (Timing.TotalTime - LastSomethingHappenedTime) % CaretBlinkInterval > 0.5 * CaretBlinkInterval;
      CaretTexture.Visible = Focused && SelectionEmpty && !CaretIsHidden;
    }
  }
}