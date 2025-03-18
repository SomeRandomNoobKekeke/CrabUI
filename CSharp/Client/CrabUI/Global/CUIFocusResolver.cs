using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using EventInput;

namespace CrabUI
{
  public class CUIFocusResolver
  {
    private CUIComponent focusedCUIComponent;
    public CUIComponent FocusedCUIComponent
    {
      get => focusedCUIComponent;
      set
      {
        CUIComponent oldFocused = focusedCUIComponent;
        CUIComponent newFocused = value;

        if (oldFocused == newFocused) return;

        if (oldFocused != null)
        {
          oldFocused.Focused = false;
          oldFocused.InvokeOnFocusLost();
        }

        if (newFocused != null)
        {
          newFocused.Focused = true;
          newFocused.InvokeOnFocus();
        }

        if (oldFocused is IKeyboardSubscriber || newFocused is null)
        {
          OnVanillaIKeyboardSubscriberSet(null, true);
        }

        if (newFocused is IKeyboardSubscriber)
        {
          OnVanillaIKeyboardSubscriberSet((IKeyboardSubscriber)newFocused, true);
        }

        focusedCUIComponent = value;
      }
    }

    public void OnVanillaIKeyboardSubscriberSet(IKeyboardSubscriber value, bool callFromCUI = false)
    {
      try
      {
        KeyboardDispatcher _ = GUI.KeyboardDispatcher;

        IKeyboardSubscriber oldSubscriber = _._subscriber;
        IKeyboardSubscriber newSubscriber = value;

        if (newSubscriber == oldSubscriber) { return; }

        // this case should be handled in CUI
        if (!callFromCUI && oldSubscriber is CUIComponent && newSubscriber is null) { return; }

        //CUI.Log($"new IKeyboardSubscriber {oldSubscriber} -> {newSubscriber}");

        if (oldSubscriber != null)
        {
          TextInput.StopTextInput();
          oldSubscriber.Selected = false;
        }

        if (oldSubscriber is CUIComponent component && newSubscriber is GUITextBox)
        {
          //TODO for some season TextInput doesn't loose focus here
          component.InvokeOnFocusLost();
          component.Focused = false;
          focusedCUIComponent = null;
        }

        if (newSubscriber != null)
        {
          if (newSubscriber is GUITextBox box)
          {
            TextInput.SetTextInputRect(box.MouseRect);
            TextInput.StartTextInput();
            TextInput.SetTextInputRect(box.MouseRect);
          }

          if (newSubscriber is CUIComponent)
          {
            TextInput.StartTextInput();
          }

          newSubscriber.Selected = true;
        }

        _._subscriber = value;
      }
      catch (Exception e)
      {
        CUI.Error(e);
      }
    }



  }
}