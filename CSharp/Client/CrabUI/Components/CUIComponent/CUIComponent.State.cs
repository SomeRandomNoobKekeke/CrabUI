using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System.Xml;
using System.Xml.Linq;
using HarmonyLib;

namespace CrabUI
{
  public partial class CUIComponent : IDisposable
  {
    /// <summary>
    /// Global ID, unique for component
    /// </summary>
    public int ID { get; set; }

    internal bool DebugHighlight { get; set; }

    private CUIMainComponent mainComponent;
    /// <summary>
    /// Link to CUIMainComponent, passed to children
    /// </summary>
    public CUIMainComponent MainComponent
    {
      get => mainComponent;
      set
      {
        mainComponent = value;
        foreach (var child in Children) { child.MainComponent = value; }
      }
    }

    internal int positionalZIndex;
    internal int addedZIndex;

    [Calculated] public bool Focused { get; set; }

    /// <summary>
    /// True when parent has HideChildrenOutsideFrame and child wanders beyond parents border
    /// </summary>
    [Calculated] internal bool CulledOut { get; set; }

    /// <summary>
    /// BackgroundColor != Color.Transparent
    /// </summary>
    protected bool BackgroundVisible { get; set; }

    protected bool OutlineVisible { get; set; }

    // This is for state clones, to protect them from style changes
    internal bool Unreal { get; set; }

    public bool MouseOver { get; set; }
    public bool MousePressed { get; set; }

    /// <summary>
    /// This is used by text to prevent resizing beyond that
    /// and works as AbsoluteMin
    /// </summary>
    [Calculated]
    public CUINullVector2 ForcedMinSize
    {
      get => forsedSize;
      set => SetForcedMinSize(value);
    }
    protected CUINullVector2 forsedSize; internal void SetForcedMinSize(CUINullVector2 value, [CallerMemberName] string memberName = "")
    {
      forsedSize = value;
      CUIDebug.Capture(null, this, "SetForcedMinSize", memberName, "ForcedMinSize", ForcedMinSize.ToString());
      OnPropChanged();//TODO this is the reason why lists with a lot of children lag
      //OnSelfAndParentChanged();
      OnAbsolutePropChanged();
    }

    /// <summary>
    /// This is set by ChildrenOffset when zooming, and iirc consumed by text to adjust text scale
    /// </summary>
    [Calculated]
    public float Scale
    {
      get => scale;
      set => SetScale(value);
    }
    protected float scale = 1f; internal void SetScale(float value, [CallerMemberName] string memberName = "")
    {
      scale = value;
      foreach (var child in Children) { child.Scale = value; }
      // OnDecorPropChanged();
    }

    /// <summary>
    /// Calculated Prop, Real + BorderThickness
    /// </summary>
    protected CUIRect BorderBox { get; set; }
    protected CUIRect OutlineBox { get; set; }
    internal Rectangle? ScissorRect { get; set; }
    /// <summary>
    /// Buffer for texture data, for IgnoreTransparent checks
    /// </summary>
    protected Color[] TextureData;
    /// <summary>
    /// Calculated prop, position on real screen in pixels
    /// Should be fully calculated after CUIMainComponent.Update
    /// </summary>
    [Calculated]
    public CUIRect Real
    {
      get => real;
      set => SetReal(value);
    }



    private CUIRect real; internal void SetReal(CUIRect value, [CallerMemberName] string memberName = "")
    {
      //HACK idk if i need it
      real = new CUIRect(
        (float)Math.Round(value.Left),
        (float)Math.Round(value.Top),
        (float)Math.Round(value.Width),
        (float)Math.Round(value.Height)
      );
      // real = value;
      CUIDebug.Capture(null, this, "SetReal", memberName, "real", real.ToString());


      BorderBox = real;
      // BorderBox = new CUIRect(
      //   real.Left - BorderThickness,
      //   real.Top - BorderThickness,
      //   real.Width + 2 * BorderThickness,
      //   real.Height + 2 * BorderThickness
      // );

      OutlineBox = new CUIRect(
        real.Left - OutlineThickness,
        real.Top - OutlineThickness,
        real.Width + 2 * OutlineThickness,
        real.Height + 2 * OutlineThickness
      );

      if (HideChildrenOutsideFrame)
      {
        Rectangle SRect = real.Box;

        // //HACK Remove these + 1
        // Rectangle SRect = new Rectangle(
        //   (int)real.Left + 1,
        //   (int)real.Top + 1,
        //   (int)real.Width - 2,
        //   (int)real.Height - 2
        // );

        if (Parent?.ScissorRect != null)
        {
          ScissorRect = Rectangle.Intersect(Parent.ScissorRect.Value, SRect);
        }
        else
        {
          ScissorRect = SRect;
        }
      }
      else ScissorRect = Parent?.ScissorRect;
    }
  }
}