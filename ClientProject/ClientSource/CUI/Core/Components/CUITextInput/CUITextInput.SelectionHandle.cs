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
using BaroJunk;

namespace CrabUI
{
  public partial class CUITextInput
  {
    private SelectionHandle_Part SelectionHandle { get; } = new();
    public class SelectionHandle_Part : IPart
    {
      public CUITextInput Self { get; set; }

      public void Init()
      {
        Self.MouseDown += (c, e) => HandleMouseDown(e.Pos);
        Self.MouseMoved += (c, e) => HandleMouseMove(e.Pos);
        Self.MouseUp += (c, e) => HandleMouseUp(e.Pos);
      }

      public bool Selecting;
      public Vector2 InitialClickPos;
      public int InitialSelectionIndex;
      public Vector2 CusorPos;
      public int CusorSelectionIndex;

      public void HandleMouseDown(Vector2 mousePos)
      {
        Selecting = true;
      }

      public void HandleMouseMove(Vector2 mousePos)
      {

      }

      public void HandleMouseUp(Vector2 mousePos)
      {

      }

      public ClearableEvent SelectionStart { get; } = new();
      public ClearableEvent SelectionEnd { get; } = new();
      public ClearableEvent SelectionUpdated { get; } = new();
    }


  }
}