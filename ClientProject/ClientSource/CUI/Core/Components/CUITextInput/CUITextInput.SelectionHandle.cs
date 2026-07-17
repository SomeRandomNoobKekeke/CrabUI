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
using CUILibs;

namespace CrabUI
{
  public partial class CUITextInput
  {
    private SelectionHandle_Part SelectionHandle { get; } = new();
    public class SelectionHandle_Part : Part
    {
      public void Init()
      {
        Self.MouseDown += (c, e) => HandleMouseDown(e);
      }

      public bool Selecting;
      public Vector2 InitialClickPos;
      public int InitialSelectionIndex;
      public Vector2 CusorPos;
      public int CusorSelectionIndex;

      public void HandleMouseDown(CUIMouseDownEvent e)
      {
        Selecting = true;
        InitialClickPos = e.Pos;
        InitialSelectionIndex = Self.TextBlock.CaretIndex(InitialClickPos);
        Self.CaretPos = InitialSelectionIndex;

        Self.MainComponent.GlobalEvents.MouseMoved.Add(HandleMouseMove);
        Self.MainComponent.GlobalEvents.MouseUp.Add(HandleMouseUp);


      }

      public void HandleMouseMove(CUIMouseMovedEvent e)
      {
        if (!Selecting) return;

        CusorPos = e.Pos;
        CusorSelectionIndex = Self.TextBlock.CaretIndex(CusorPos);
        Self.SetSelection(InitialSelectionIndex, CusorSelectionIndex);
        Self.CaretPos = CusorSelectionIndex;
        Self.UpdateVisualState();
      }

      public void HandleMouseUp(CUIMouseUpEvent e)
      {
        if (!Selecting) return;
        Selecting = false;
        Self.MainComponent.GlobalEvents.MouseMoved.Remove(HandleMouseMove);
        Self.MainComponent.GlobalEvents.MouseUp.Remove(HandleMouseUp);

        CusorPos = e.Pos;
        CusorSelectionIndex = Self.TextBlock.CaretIndex(CusorPos);
        Self.SetSelection(InitialSelectionIndex, CusorSelectionIndex);
        Self.CaretPos = CusorSelectionIndex;
        Self.UpdateVisualState();
      }
    }


  }
}