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

namespace CrabUI
{
  public partial class CUIComponent
  {
    /// <summary>
    /// Should children be cut off by scissor rect, this is just visual, it's not the same as culling
    /// </summary>
    [CUISerializable] public bool HideChildrenOutsideFrame { get; set; }
    /// <summary>
    /// if child rect doesn't intersect with parent it won't be drawn and won't consume fps  
    /// It also sets HideChildrenOutsideFrame
    /// </summary>
    [CUISerializable]
    public bool CullChildren
    {
      get => CUIProps.CullChildren.Value;
      set => CUIProps.CullChildren.SetValue(value);
    }

    /// <summary>
    /// It shouldn't be culled off even outside of parent bounds and even if parent demands so 
    /// </summary>
    [CUISerializable] public bool UnCullable { get; set; }
    /// <summary>
    /// Will shift all children by this much, e.g. this is how scroll works
    /// It's also 3D
    /// </summary>
    [CUISerializable]
    public CUI3DOffset ChildrenOffset
    {
      get => CUIProps.ChildrenOffset.Value;
      set => CUIProps.ChildrenOffset.SetValue(value);
    }

    /// <summary>
    /// Limits to children positions
    /// </summary>
    public Func<CUIRect, CUIBoundaries> ChildrenBoundaries { get; set; }

    /// <summary>
    /// Should it ignore child offset?
    /// </summary>
    [CUISerializable] public bool Fixed { get; set; }
    /// <summary>
    /// this point of this component
    /// </summary>
    [CUISerializable] public Vector2 Anchor { get; set; }
    /// <summary>
    /// will be attached to this point of parent
    /// </summary>
    [CUISerializable] public Vector2? ParentAnchor { get; set; }

    /// <summary>
    /// Ghost components don't affect layout
    /// </summary>
    [CUISerializable]
    public CUIBool2 Ghost
    {
      get => CUIProps.Ghost.Value;
      set => CUIProps.Ghost.SetValue(value);
    }
    /// <summary>
    /// Components are drawn in order of their ZIndex  
    /// Normally it's derived from component position in the tree, 
    /// but this will override it 
    /// </summary>
    [CUISerializable]
    public int? ZIndex
    {
      get => CUIProps.ZIndex.Value;
      set => CUIProps.ZIndex.SetValue(value);
    }


    /// <summary>
    /// If true component will set it's Absolute size to sprite texture size
    /// </summary>
    [CUISerializable]
    public bool ResizeToSprite
    {
      get => CUIProps.ResizeToSprite.Value;
      set => CUIProps.ResizeToSprite.SetValue(value);
    }

    /// <summary>
    /// Will be resized to fill empty space in list components
    /// </summary>
    [CUISerializable]
    public CUIBool2 FillEmptySpace
    {
      get => CUIProps.FillEmptySpace.Value;
      set => CUIProps.FillEmptySpace.SetValue(value);
    }
    /// <summary>
    /// Will resize itself to fit components with absolute size, e.g. text
    /// </summary>
    [CUISerializable]
    public CUIBool2 FitContent
    {
      get => CUIProps.FitContent.Value;
      set => CUIProps.FitContent.SetValue(value);
    }
    /// <summary>
    /// Absolute size and position in pixels
    /// </summary>
    [CUISerializable]
    public CUINullRect Absolute
    {
      get => CUIProps.Absolute.Value;
      set => CUIProps.Absolute.SetValue(value);
    }
    [CUISerializable]
    public CUINullRect AbsoluteMin
    {
      get => CUIProps.AbsoluteMin.Value;
      set => CUIProps.AbsoluteMin.SetValue(value);
    }
    [CUISerializable]
    public CUINullRect AbsoluteMax
    {
      get => CUIProps.AbsoluteMax.Value;
      set => CUIProps.AbsoluteMax.SetValue(value);
    }
    /// <summary>
    /// Relative to parent size and position, [0..1]
    /// </summary>
    [CUISerializable]
    public CUINullRect Relative
    {
      get => CUIProps.Relative.Value;
      set => CUIProps.Relative.SetValue(value);
    }
    [CUISerializable]
    public CUINullRect RelativeMin
    {
      get => CUIProps.RelativeMin.Value;
      set => CUIProps.RelativeMin.SetValue(value);
    }
    [CUISerializable]
    public CUINullRect RelativeMax
    {
      get => CUIProps.RelativeMax.Value;
      set => CUIProps.RelativeMax.SetValue(value);
    }
    /// <summary>
    /// It's like Relative, but to the opposite dimension  
    /// E.g. Real.Width = CrossRelative.Width * Parent.Real.Height  
    /// Handy for creating square things
    /// </summary>
    [CUISerializable]
    public CUINullRect CrossRelative
    {
      get => CUIProps.CrossRelative.Value;
      set => CUIProps.CrossRelative.SetValue(value);
    }

    /// <summary>
    /// Used in Grid, space separated Row sizes, either in pixels (123) or in % (123%) 
    /// </summary>
    [CUISerializable] public string GridTemplateRows { get; set; }
    /// <summary>
    /// Used in Grid, space separated Columns sizes, either in pixels (123) or in % (123%) 
    /// </summary>
    [CUISerializable] public string GridTemplateColumns { get; set; }
    /// <summary>
    /// Component will be placed in this cell in the grid component
    /// </summary>
    [CUISerializable] public Point? GridStartCell { get; set; }
    /// <summary>
    /// And resized to fit cells from GridStartCell to GridEndCell
    /// </summary>
    [CUISerializable] public Point? GridEndCell { get; set; }
    /// <summary>
    /// Sets both GridStartCell and GridEndCell at once
    /// </summary>
    public Point? GridCell
    {
      get => GridStartCell;
      set
      {
        GridStartCell = value;
        GridEndCell = value;
      }
    }


  }
}