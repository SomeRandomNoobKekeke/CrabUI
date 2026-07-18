# Props {#OverviewProps}



### Commonly used Props of CUIComponent:

#### Layout Props:
Note that props used by some layouts might be ignored in others

~~~~~~~~~~~~~{cs}
public CUINullRect Absolute { get; set; } // - Absolute position and size in pixels
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUINullRect AbsoluteMin { get; set; } // - Min Absolute position and size in pixels
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUINullRect AbsoluteMax { get; set; } // - Max Absolute position and size in pixels
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUINullRect Relative { get; set; } // - Position and size relative to parent size 
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUINullRect RelativeMin { get; set; } // - Min Position and size relative to parent size 
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUINullRect RelativeMax { get; set; } // - Max Position and size relative to parent size 
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUINullRect CrossRelative { get; set; } // - Relative position and size but to the opposite dimension, useful for creating square things
~~~~~~~~~~~~~

Child position is calculated from Anchor position  
Child is attached with its Anchor point to ParentAnchor of parent rect  
if ParentAnchor is null Anchor is used for both
~~~~~~~~~~~~~{cs}
public Vector2 Anchor { get; set; } // - ([0..1],[0..1]) Vector2
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public Vector2? ParentAnchor { get; set; } // - ([0..1],[0..1]) Vector2?
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIDirection Direction { get; set; } // - used in lists to specify direction of child positioning
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public float? Flex { get; set; } // - if not null then child will fill empty space in lists 
                                 // - if there's multiple flex components space will be splitted among them proportionally to their flex
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public Vector2 ChildrenOffset { get; set; } // - All children will be shifted on this vector, used for scroll
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIBoundaries ChildrenOffsetBounds { get; set; } // - ChildrenOffset will be bounded with these, used to limit scroll
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public Func<CUIRect, CUIBoundaries> { get; set; } // - Used to keep children in some bounds, there's static funcs in CUIBoundaries
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIBool2 FitContent { get; set; } // - Parent will resize to fit all children inside of its rect 
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public int GridRow { get; set; } // - Grid row to put this component into
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public int GridColumn { get; set; } // - Grid column to put this component into
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public (int, int) Grid { get; set; } // - Set both at once
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public CUISizes Margin { get; set; } { get; set; } // - Empty space around component
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUISizes Border { get; set; } // - Size of borders
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUISizes Padding { get; set; } // - Inner empty space between border and children
~~~~~~~~~~~~~

Note: these rects are not readonly, but you don't need to set them, they're supposed to be set by parent layout
~~~~~~~~~~~~~{cs}
public CUIRect { get; set; } OuterRect
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIRect Rect { get; set; } // OuterRect - Margin
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIRect InnerRect { get; set; } // Rect - Border
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIRect ChildrenRect { get; set; } // InnerRect - Padding
~~~~~~~~~~~~~

#### Graphics Props:
~~~~~~~~~~~~~{cs}
public SimpleTexture Background { get; }

public class SimpleTexture {
  public CUISprite Sprite { get; set; }
  public bool Visible { get; set; }

  // Note that these props are forwarded from Sprite, so if you change sprite you throw away these as well 
  public CUITexture2D Texture { get; set; }
  public Rectangle? SourceRectangle { get; set; }
  public Color Color { get; set; }
  public float Rotation { get; set; }
  public Vector2 Origin { get; set; }
  public SpriteEffects Effects { get; set; }
  public float LayerDepth { get; set; }
}
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public Borders Borders { get; }

public class Borders {
  public CUISizes Sizes { get; set; }
  public bool Visible { get; set; }
  public Color Color { get; set; }
}
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool Displayed { get; set; } // - If false component won't even be added to the list of visible components
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool CullChildren { get; set; } // - Should children be culled and cropped outside of parent rect
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool IgnoretransparentPixels { get; set; } // - Whether clicks should register on transparent pixels, it buffers texture data to test pixel, it's heavy, use wisely
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool UseReactiveStyles { get; set; } // - Defaults to CUICore.Styles.UseReactiveStyles
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIPaletteRank PaletteRank { get; set; } // - uses palette from CUICore.Palettes of some rank (Primary, Secondary)
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIPalette Palette { get; set; } // - Changes palette, it'll retrigger styles
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public CUIPalette DeepPalette { get; set; } // - Recursively sets palette for children 
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public Action<CUIComponent> Style { get; set; } // - Arbitrary func that sets some props on component, saved in PersonalStyle
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public ICUIStyle PersonalStyle { get; set; } // - Extra style that will be applied after type specific styles
~~~~~~~~~~~~~

#### Other Props:
~~~~~~~~~~~~~{cs}
public bool Focusable { get; set; } // - Can it be focused
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool Draggable { get; set; } // - Can it be dragged
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool Swipeable { get; set; } // - Can it be swiped
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool Resizable { get; set; } // - Can it be resized
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool ConsumeFocus { get; set; } // - Should it consume focus events
~~~~~~~~~~~~~
~~~~~~~~~~~~~{cs}
public bool ConsumeMouseEvents { get; set; } // - Should it consume mouse events
~~~~~~~~~~~~~