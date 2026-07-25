using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
namespace CrabUI
{
  public partial class CUIVisualComponent
  {
    [CUISerializableProp]
    public bool Draggable
    {
      get => DragHandle.Active;
      set => DragHandle.Active = value;
    }
    [CUISerializableProp]
    public bool Swipeable
    {
      get => SwipeHandle.Active;
      set => SwipeHandle.Active = value;
    }

    public DragHandle DragHandle { get; } = new();
    public SwipeHandle SwipeHandle { get; } = new();
  }
}