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
      c.FocusedColor = c.Palette.Colors["activeinput"];
      c.BluredColor = c.Palette.Colors["inputbackground"];
      c.SelectionColor = c.Palette.Colors["highlight"] * 0.4f;
      c.CaretColor = c.Palette.Colors["highlight"];
      c.InvalidColor = c.Palette.Colors["invalid"] * 0.5f;
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