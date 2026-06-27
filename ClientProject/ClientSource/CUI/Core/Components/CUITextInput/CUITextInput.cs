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
      //TODO use palette colors
      c.FocusedColor = new Color(0, 255, 255, 64);
      c.BluredColor = new Color(0, 0, 0, 64);
      c.SelectionColor = new Color(0, 255, 255, 128);
      c.CaretColor = new Color(200, 255, 255, 200);
      c.InvalidColor = new(255, 0, 0, 200);
    });

    protected override void InitStyle()
    {
      base.InitStyle();
      Focusable = true;
      TextBlock.Anchor = CUIAnchor.LeftCenter;
      Background.ConsumeMouseClicks = true;
      CullChildren = true;
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
      OnFocus += HandleFocus;
      OnFocusLost += HandleFocusLost;

      MouseDown += HandleMouseDown;
      MouseDoubleClick += HandleDoubleClick;

      State.Changed += HandleStateChanged;
      State.TextChanged += HandleTextChanged;
    }





  }
}