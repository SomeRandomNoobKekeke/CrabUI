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
  [GeneratedComponent]
  public partial class CUITextInput : CUIComponent, IComponent
  {
    public class Part : IPart { public CUITextInput Self { get; set; } }
    public static ICUIStyle DefaultStyle { get; } = new CUIDefaultStyle<CUITextInput>((c) =>
    {
      // c.FocusedColor = Color.Lerp(c.Palette["back"], c.Palette["main"], 0.8f); //TODO2
      c.BluredColor = Color.Lerp(c.Palette["back"], c.Palette["main"], 0.3f);
      c.SelectionColor = c.Palette["selection"] * 0.4f;
      c.CaretColor = c.Palette["selection"];
      c.InvalidColor = c.Palette["invalid"] * 0.5f;
      c.TextBlock.TextColor = c.Palette["text"];
      c.UpdateVisualState();//HACK
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      // Focusable = true;//TODO2
      TextBlock.Anchor = CUIAnchor.LeftCenter;
      ConsumeMouseEvents = true;
      // ConsumeFocus = true;//TODO2
      CullChildren = true;

      BluredSprite = CUISprite.White;
      InvalidSprite = CUISprite.BoxWithALamp;
      FocusedSprite = CUISprite.BoxWithALamp;

      Background.Sprite = BluredSprite;
      Padding = new CUISizes(4, 4, 4, 4);
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
      mainComponent.GlobalEvents.AfterUpdate.Remove(HandleUpdate);
    }



    public CUITextInput() : base()
    {
      // OnFocus += HandleFocus;//TODO2
      // OnFocusLost += HandleFocusLost;//TODO2

      MouseDown += HandleMouseDown;
      MouseDoubleClick += HandleDoubleClick;

      State.Changed += HandleStateChanged;
      State.TextChanged += HandleTextChanged;
    }





  }
}