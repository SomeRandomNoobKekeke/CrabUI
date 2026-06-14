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
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUITextInput>((c) =>
    {
      c.Focusable = true;
      c.CaretTexture.Color = Color.Cyan;
      c.SelectionOverlay.Color = Color.Cyan;
      c.TextBlock.Anchor = CUIAnchor.LeftCenter;
    });


    private void HandleTextInput(CUITextInputEvent e)
    {
      if (!Focused) return;
      HandleCharacter(e.Args.Character);
    }

    private void HandleKeyDownInput(CUIKeyDownInputEvent e)
    {
      if (!Focused) return;

      HandleKey(e.Args.Key);

      //TODO i should just use CUIInput and check ctrl and shift states, they might have different actions
      if (char.IsControl(e.Args.Character)) //HACK somehow it works
      {
        HandleCommand(e.Args.Key);
      }
    }

    protected override void OnAttachedToMainComponent(CUIMainComponent mainComponent)
    {
      base.OnAttachedToMainComponent(mainComponent);

      mainComponent.GlobalEvents.TextInput.Add(HandleTextInput);
      mainComponent.GlobalEvents.KeyDownInput.Add(HandleKeyDownInput);
    }
    protected override void OnDetachedFromMainComponent(CUIMainComponent mainComponent)
    {
      base.OnDetachedFromMainComponent(mainComponent);

      mainComponent.GlobalEvents.TextInput.Remove(HandleTextInput);
      mainComponent.GlobalEvents.KeyDownInput.Remove(HandleKeyDownInput);
    }

    public CUITextInput() : base()
    {
      OnFocus += HandleFocus;
      OnFocusLost += HandleFocusLost;
    }
  }
}