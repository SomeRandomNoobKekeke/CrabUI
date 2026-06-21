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
  [GeneratedComponent]
  public partial class CUITextInput : CUIComponent, IComponent
  {
    public class Part : IPart { public CUITextInput Self { get; set; } }
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUITextInput>((c) =>
    {
      c.Focusable = true;
      c.TextBlock.Anchor = CUIAnchor.LeftCenter;
      c.Background.ConsumeMouseClicks = true;

      c.FocusedColor = new Color(0, 255, 255, 64);
      c.BluredColor = new Color(255, 255, 255, 32);
      c.SelectionColor = new Color(0, 255, 255, 64);
      c.CaretColor = new Color(0, 255, 255, 64);
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