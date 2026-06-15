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
      c.SelectionOverlay.Color = new Color(0, 255, 255, 64);
      c.TextBlock.Anchor = CUIAnchor.LeftCenter;
      c.Background.ConsumeMouseClicks = true;
    });


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

      State.Changed += HandleStateChanged;
      MouseDown += HandleMouseDown;
    }





  }
}