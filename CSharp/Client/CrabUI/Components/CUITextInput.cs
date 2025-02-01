using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using EventInput;
using System.Windows;

namespace CrabUI
{

  /// <summary>
  /// Text input
  /// </summary>
  public class CUITextInput : CUIComponent, IKeyboardSubscriber
  {

    #region IKeyboardSubscriber

    private Keys pressedKey;

    /// <summary>
    /// From IKeyboardSubscriber, don't use it directly
    /// </summary>
    public void ReceiveSpecialInput(Keys key)
    {
      pressedKey = key;
      if (key == Keys.Back) Back();
      if (key == Keys.Delete) Delete();
      if (key == Keys.Left) MoveLeft();
      if (key == Keys.Right) MoveRight();
    }
    /// <summary>
    /// From IKeyboardSubscriber, don't use it directly
    /// </summary>
    public void ReceiveTextInput(char inputChar) => ReceiveTextInput(inputChar.ToString());
    /// <summary>
    /// From IKeyboardSubscriber, don't use it directly
    /// </summary>
    public void ReceiveTextInput(string text)
    {
      try
      {
        CutSelection();
        Text = Text.Insert(CaretPos, text);
        CaretPos = CaretPos + 1;
        OnTextAdded?.Invoke(text);
      }
      catch (Exception e)
      {
        CUI.Log(e);
      }

    }
    /// <summary>
    /// From IKeyboardSubscriber, don't use it directly
    /// </summary>
    public void ReceiveCommandInput(char command)
    {
      if (pressedKey == Keys.A) SelectAll();
      if (pressedKey == Keys.C) Copy();
      if (pressedKey == Keys.V) Paste();
    }

    //Alt+tab?
    /// <summary>
    /// From IKeyboardSubscriber, don't use it directly
    /// </summary>
    public void ReceiveEditingInput(string text, int start, int length)
    {
      //CUI.Log($"ReceiveEditingInput {text} {start} {length}");
    }

    //TODO mb should lose focus here
    /// <summary>
    /// From IKeyboardSubscriber, don't use it directly
    /// </summary>
    public bool Selected { get; set; }
    #endregion

    #region Commands
    public void SelectAll() => Select(0, Text.Length);

    public void Copy()
    {
      if (Selection.IsEmpty) return;
      selectionHandle.Grabbed = false;
      Clipboard.SetText(Text.SubstringSafe(Selection.Start, Selection.End));
    }
    public void Paste()
    {
      ReceiveTextInput(Clipboard.GetText());
    }

    public void AddText(string text) => ReceiveTextInput(text);
    public void MoveLeft()
    {
      CaretPos--;
      Selection = IntRange.Zero;
    }
    public void MoveRight()
    {
      CaretPos++;
      Selection = IntRange.Zero;
    }

    // //TODO
    // public void SelectLeft()
    // {
    //   if (Selection == IntRange.Zero) Selection = new IntRange(CaretPos - 1, CaretPos);
    //   else Selection = new IntRange(Selection.Start - 1, Selection.End);
    // }
    // //TODO
    // public void SelectRight()
    // {
    //   if (Selection == IntRange.Zero) Selection = new IntRange(CaretPos, CaretPos + 1);
    //   else Selection = new IntRange(Selection.Start, Selection.End + 1);
    // }

    public void Back()
    {
      TextInputState oldState = State;
      if (!Selection.IsEmpty) CutSelection();
      else
      {
        string s1 = oldState.Text.SubstringSafe(0, oldState.CaretPos - 1);
        string s2 = oldState.Text.SubstringSafe(oldState.CaretPos);
        Text = s1 + s2;
        CaretPos = oldState.CaretPos - 1;
      }
    }

    public void Delete()
    {
      TextInputState oldState = State;
      if (!Selection.IsEmpty) CutSelection();
      else
      {
        string s1 = oldState.Text.SubstringSafe(0, oldState.CaretPos);
        string s2 = oldState.Text.SubstringSafe(oldState.CaretPos + 1);
        Text = s1 + s2;
        //CaretPos = oldState.CaretPos;
      }
    }

    public void CutSelection()
    {
      if (Selection.IsEmpty) return;
      selectionHandle.Grabbed = false;
      string s1 = Text.SubstringSafe(0, Selection.Start);
      string s2 = Text.SubstringSafe(Selection.End);
      Text = s1 + s2;
      CaretPos = Selection.Start;
      Selection = IntRange.Zero;
    }

    internal int SetCaretPos(Vector2 v)
    {
      int newCaretPos = TextComponent.CaretIndex(v.X);
      CaretPos = newCaretPos;
      return newCaretPos;
    }

    #endregion

    internal class SelectionHandle
    {
      public bool Grabbed;
      public int lastSelectedPos;
    }
    internal SelectionHandle selectionHandle = new SelectionHandle();

    internal record struct TextInputState(string Text, IntRange Selection, int CaretPos)
    {
      public string Text { get; init; } = Text ?? "";
    }
    private TextInputState state; internal TextInputState State
    {
      get => state;
      set
      {
        state = ValidateState(value);
        ApplyState(state);
      }
    }

    internal TextInputState ValidateState(TextInputState state)
    {
      //return state with { CaretPos = state.CaretPos.Fit(0, state.Text.Length - 1) };

      string newText = state.Text;

      IntRange newSelection = new IntRange(
        state.Selection.Start.Fit(0, newText.Length),
        state.Selection.End.Fit(0, newText.Length)
      );

      int newCaretPos = state.CaretPos.Fit(0, newText.Length);

      return new TextInputState(newText, newSelection, newCaretPos);
    }

    internal void ApplyState(TextInputState state)
    {
      TextComponent.Text = state.Text;

      SelectionOverlay.Visible = !state.Selection.IsEmpty;
      CaretIndicatorVisible = !SelectionOverlay.Visible;

      if (!state.Selection.IsEmpty)
      {
        SelectionOverlay.Absolute = SelectionOverlay.Absolute with
        {
          Left = TextComponent.CaretPos(state.Selection.Start),
          Width = TextComponent.CaretPos(state.Selection.End) - TextComponent.CaretPos(state.Selection.Start),
        };
      }

      CaretIndicator.Absolute = CaretIndicator.Absolute with
      {
        Left = TextComponent.CaretPos(state.CaretPos),
      };
    }

    internal CUITextBlock TextComponent;
    public string Text
    {
      get => State.Text;
      set
      {
        if (Disabled) return;
        State = new TextInputState(value, State.Selection, State.CaretPos);
        OnTextChanged?.Invoke(State.Text);
      }
    }

    internal CUIComponent SelectionOverlay;
    public IntRange Selection
    {
      get => State.Selection;
      set => State = new TextInputState(State.Text, value, State.CaretPos);
    }
    public void Select(int start, int end) => Selection = new IntRange(start, end);


    public bool CaretIndicatorVisible { get; set; }
    public double CaretBlinkInterval { get; set; } = 0.5;
    internal CUIComponent CaretIndicator;
    public int CaretPos
    {
      get => State.CaretPos;
      set => State = new TextInputState(State.Text, State.Selection, value);
    }

    [CUISerializable] public Color FocusedBorderColor { get; set; } = Color.Aqua;
    //TODO
    //[CUISerializable] public bool PreventOverflow { get; set; } = false;

    public event Action<string> OnTextChanged;
    public Action<string> AddOnTextChanged { set => OnTextChanged += value; }
    public event Action<string> OnTextAdded;
    public Action<string> AddOnTextAdded { set => OnTextAdded += value; }

    public override void Draw(SpriteBatch spriteBatch)
    {
      CaretIndicator.Visible = Focused && CaretIndicatorVisible && Timing.TotalTime % CaretBlinkInterval > CaretBlinkInterval / 2;

      base.Draw(spriteBatch);
    }

    public CUITextInput(string text) : this()
    {
      Text = text;
    }
    public CUITextInput() : base()
    {
      AbsoluteMin = new CUINullRect(w: 50);
      FitContent = new CUIBool2(true, true);
      Focusable = true;
      BorderThickness = 2;
      HideChildrenOutsideFrame = true;
      ConsumeMouseClicks = true;
      ConsumeDragAndDrop = true;
      ConsumeSwipe = true;

      this["TextComponent"] = TextComponent = new CUITextBlock()
      {
        Text = "text",
        Style = new CUIStyle(){
          {"Padding", "[2,2]"},
        },
      };

      this["SelectionOverlay"] = SelectionOverlay = new CUIComponent()
      {
        Style = new CUIStyle(){
          {"BackgroundColor", "0,255,255,128"},
          {"BorderColor", "Transparent"},
        },
        Relative = new CUINullRect(h: 1),
        Ghost = new CUIBool2(true, true),
      };

      this["CaretIndicator"] = CaretIndicator = new CUIComponent()
      {
        Style = new CUIStyle(){
          {"BackgroundColor", "255,255,255,150"},
          {"BorderColor", "Transparent"},
        },
        Relative = new CUINullRect(h: 1),
        Absolute = new CUINullRect(w: 1),
        Ghost = new CUIBool2(true, true),
      };


      //TODO unhardcode
      OnFocus += () =>
      {
        BorderColor = FocusedBorderColor;
      };

      OnFocusLost += () =>
      {
        BorderColor = Color.Transparent;
        Selection = IntRange.Zero;
      };

      OnMouseDown += (e) =>
      {
        int newCaretPos = SetCaretPos(e.MousePosition - Real.Position);
        Selection = IntRange.Zero;
        selectionHandle.lastSelectedPos = newCaretPos;
        selectionHandle.Grabbed = true;
      };

      OnMouseMove += (e) =>
      {
        if (selectionHandle.Grabbed)
        {
          int nextCaretPos = SetCaretPos(e.MousePosition - Real.Position);
          Selection = new IntRange(selectionHandle.lastSelectedPos, nextCaretPos);
        }
      };

      OnDClick += (e) => SelectAll();

      if (CUI.Main is not null)
      {
        CUI.Main.Global.OnMouseUp += (e) => selectionHandle.Grabbed = false;
      }
    }
  }

}