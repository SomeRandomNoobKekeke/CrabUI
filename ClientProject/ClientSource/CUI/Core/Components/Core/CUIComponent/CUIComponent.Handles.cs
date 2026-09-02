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
using Microsoft.Xna.Framework.Graphics;
namespace CursedUI
{
  public partial class CUIComponent
  {
    [CUISerializableProp]
    public bool Resizable
    {
      get => RightResizeHandle == null;
      set
      {
        if (RightResizeHandle is null && value)
        {
          RightResizeHandle = ResizeHandle.CreateAngle(1, 1);
          AttachChild(RightResizeHandle);
        }

        if (RightResizeHandle is not null && !value)
        {
          DetachChild(RightResizeHandle);
          RightResizeHandle = null;
        }
      }
    }

    [CUISerializableProp]
    public bool ResizableLeft
    {
      get => LeftResizeHandle == null;
      set
      {
        if (LeftResizeHandle is null && value)
        {
          LeftResizeHandle = ResizeHandle.CreateAngle(0, 1);
          AttachChild(LeftResizeHandle);
        }

        if (LeftResizeHandle is not null && !value)
        {
          DetachChild(LeftResizeHandle);
          LeftResizeHandle = null;
        }
      }
    }

    public bool ResizableBoth
    {
      get => Resizable;
      set
      {
        Resizable = value;
        ResizableLeft = value;
      }
    }

    public ResizeHandle? LeftResizeHandle { get; protected set; }
    public ResizeHandle? RightResizeHandle { get; protected set; }
  }
}