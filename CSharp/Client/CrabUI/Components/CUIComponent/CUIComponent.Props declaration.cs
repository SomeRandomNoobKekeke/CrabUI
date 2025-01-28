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
    //HACK This is potentially cursed
    /// <summary>
    /// Arbitrary data
    /// </summary>
    public object Data { get; set; }
    /// <summary>
    /// Will prevent serialization to xml if true
    /// </summary>
    public bool Unserializable { get; set; }
    /// <summary>
    /// Should children be cut off by scissor rect, this is just visual, it's not the same as culling
    /// </summary>
    [CUISerializable] public bool HideChildrenOutsideFrame { get; set; }
    /// <summary>
    /// if child rect doesn't intersect with parent it won't be drawn and won't consume fps
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
    /// Some props (like visible) are autopassed to all new childs
    /// see PassPropsToChild
    /// </summary>
    [CUISerializable] public bool ShouldPassPropsToChildren { get; set; } = true;
    /// <summary>
    /// Ghost components don't affect layout
    /// </summary>
    [CUISerializable] public CUIBool2 Ghost { get; set; }

    /// <summary>
    /// Don't inherit parent Visibility
    /// </summary>
    [CUISerializable] public bool IgnoreParentVisibility { get; set; }
    /// <summary>
    /// Don't inherit parent IgnoreEvents
    /// </summary>
    [CUISerializable] public bool IgnoreParentEventIgnorance { get; set; }
    /// <summary>
    /// Don't inherit parent ZIndex
    /// </summary>
    [CUISerializable] public bool IgnoreParentZIndex { get; set; }

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
    /// Won't react to mouse events
    /// </summary>
    [CUISerializable]
    public bool IgnoreEvents
    {
      get => CUIProps.IgnoreEvents.Value;
      set => CUIProps.IgnoreEvents.SetValue(value);
    }
    /// <summary>
    /// Invisible components are not drawn, but still can be interacted with
    /// </summary>
    [CUISerializable]
    public bool Visible
    {
      get => CUIProps.Visible.Value;
      set => CUIProps.Visible.SetValue(value);
    }

    /// <summary>
    /// Visible + !IgnoreEvents
    /// </summary>
    public bool Revealed
    {
      get => CUIProps.Revealed.Value;
      set => CUIProps.Revealed.SetValue(value);
    }
    //HACK this is meant for buttons, but i want to access it on generic components in CUIMap
    protected bool disabled;
    /// <summary>
    /// Usually means - non interactable, e.g. unclickable gray button
    /// </summary>
    [CUISerializable]
    public virtual bool Disabled
    {
      get => disabled;
      set => disabled = value;
    }
    [CUISerializable]
    public float BorderThickness { get; set; } = 1f;

    /// <summary>
    /// Used for text
    /// </summary>
    [CUISerializable]
    public Vector2 Padding
    {
      get => CUIProps.Padding.Value;
      set => CUIProps.Padding.SetValue(value);
    }
    [CUISerializable]
    public Color BorderColor
    {
      get => CUIProps.BorderColor.Value;
      set => CUIProps.BorderColor.SetValue(value);
    }

    private void TryResizeToSprite()
    {
      if (!resizeToSprite) return;
      Absolute = Absolute with
      {
        Width = backgroundSprite.SourceRect.Width,
        Height = backgroundSprite.SourceRect.Height,
      };
    }

    private bool resizeToSprite;
    /// <summary>
    /// If true component will set it's Absolute size to sprite texture size
    /// </summary>
    [CUISerializable]
    public bool ResizeToSprite
    {
      get => resizeToSprite;
      set
      {
        resizeToSprite = value;
        TryResizeToSprite();
      }
    }

    private CUISprite backgroundSprite = CUISprite.Default;
    /// <summary>
    /// Will be drawn in background with BackgroundColor  
    /// Default is solid white 1x1 texture
    /// </summary>
    //TODO i think if all components will have backup sprite by default it'll simplify many things
    [CUISerializable]
    public CUISprite BackgroundSprite
    {
      get => backgroundSprite;
      set
      {
        backgroundSprite = value;
        TryResizeToSprite();
      }
    }

    /// <summary>
    /// Too lazy to implement sore
    /// </summary>
    //[CUISerializable] public CUISprite BorderSprite { get; set; }


    //TODO i think those colors could be stored inside sprites
    // But then it'll be much harder to apply side effects, think about it
    /// <summary>
    /// Color of BackgroundSprite, default is black  
    /// If you're using custom sprite and don't see it make sure this color is not black
    /// </summary>
    [CUISerializable]
    public Color BackgroundColor
    {
      get => CUIProps.BackgroundColor.Value;
      set => CUIProps.BackgroundColor.SetValue(value);
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