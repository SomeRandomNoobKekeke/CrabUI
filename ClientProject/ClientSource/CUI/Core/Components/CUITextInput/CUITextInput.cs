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
    public TextBlock TextBlock { get; } = new();

    public string Text
    {
      get => TextBlock.Text;
      set => TextBlock.Text = value;
    }


    private void HandleTextInput(CUITextInputEvent e)
    {

      Text += e.Args.Character;
    }

    private void HandleKeyDownInput(CUIKeyDownInputEvent e)
    {
      if (e.Args.Key == Keys.Back)
      {
        Text = Text.Substring(0, Math.Max(0, Text.Length - 1));
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

    protected override void UpdateRect(CUIRect rect)
    {
      base.UpdateRect(rect);
      TextBlock.Rect = rect;
    }

    public override IEnumerable<VisualUnit> VisualSplit()
    {
      if (!Visible || CulledOut) yield break;

      yield return new VisualUnit.PrimitiveVisualElement(Background);
      yield return new VisualUnit.PrimitiveVisualElement(TextBlock);
    }
  }
}