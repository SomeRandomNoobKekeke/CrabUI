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
  public partial class CUIComponent
  {
    /// <summary>
    /// Just a wrapper for CUIProps
    /// idk how to separate them better
    /// </summary>
    //TODO this should be a dict, and cuiprop should have hash
    public CUIComponentProps CUIProps { get; set; } = new();


    public class CUIComponentProps
    {
      public CUIProp<int?> ZIndex = new CUIProp<int?>()
      {
        LayoutProp = true,
        OnSet = (v, host) =>
          {
            foreach (var child in host.Children)
            {
              //HACK think, should i propagate null?
              if (v.HasValue && !child.IgnoreParentZIndex)
              {
                child.ZIndex = v.Value + 1;
              }
            }
          },
      };

      public CUIProp<bool> IgnoreEvents = new CUIProp<bool>()
      {
        OnSet = (v, host) =>
          {
            foreach (var child in host.Children)
            {
              if (!child.IgnoreParentEventIgnorance) child.IgnoreEvents = v;
            }
          },
      };

      public CUIProp<bool> Visible = new CUIProp<bool>()
      {
        Value = true,
        OnSet = (v, host) =>
        {
          foreach (var child in host.Children)
          {
            if (!child.IgnoreParentVisibility) child.Visible = v;
          }
        },
      };

      public CUIProp<bool> Revealed = new CUIProp<bool>()
      {
        Value = true,
        OnSet = (v, host) =>
        {
          // host.TreeChanged = true;
          host.Visible = v;
          host.IgnoreEvents = !v;
        },
      };

      public CUIProp<CUIBool2> Ghost = new CUIProp<CUIBool2>()
      {
        LayoutProp = true,
        AbsoluteProp = true,
      };

      public CUIProp<bool> CullChildren = new CUIProp<bool>()
      {
        OnSet = (v, host) =>
        {
          host.HideChildrenOutsideFrame = v;
        },
      };

      public CUIProp<CUI3DOffset> ChildrenOffset = new CUIProp<CUI3DOffset>()
      {
        ChildProp = true,
        Value = new CUI3DOffset(0, 0, 1), // uuuuuuuuu suka blyat!
        Validate = (v, host) => host.ChildOffsetBounds.Check(v),
        OnSet = (v, host) =>
        {
          foreach (var child in host.Children)
          {
            if (!child.Fixed) child.Scale = v.Z;
          }
        },
      };

      public CUIProp<bool> ResizeToSprite = new CUIProp<bool>()
      {
        LayoutProp = true,
        OnSet = (v, host) =>
        {
          if (v)
          {
            host.Absolute = host.Absolute with
            {
              Width = host.BackgroundSprite.SourceRect.Width,
              Height = host.BackgroundSprite.SourceRect.Height,
            };
          }
        },
      };


      public CUIProp<CUIBool2> FillEmptySpace = new CUIProp<CUIBool2>()
      {
        LayoutProp = true,
      };

      public CUIProp<CUIBool2> FitContent = new CUIProp<CUIBool2>()
      {
        LayoutProp = true,
        AbsoluteProp = true,
      };

      public CUIProp<CUINullRect> Absolute = new CUIProp<CUINullRect>()
      {
        LayoutProp = true,
        AbsoluteProp = true,
      };

      public CUIProp<CUINullRect> AbsoluteMin = new CUIProp<CUINullRect>()
      {
        LayoutProp = true,
        AbsoluteProp = true,
      };

      public CUIProp<CUINullRect> AbsoluteMax = new CUIProp<CUINullRect>()
      {
        LayoutProp = true,
        AbsoluteProp = true,
      };

      public CUIProp<CUINullRect> Relative = new CUIProp<CUINullRect>()
      {
        LayoutProp = true,
      };
      public CUIProp<CUINullRect> RelativeMin = new CUIProp<CUINullRect>()
      {
        LayoutProp = true,
      };
      public CUIProp<CUINullRect> RelativeMax = new CUIProp<CUINullRect>()
      {
        LayoutProp = true,
      };
      public CUIProp<CUINullRect> CrossRelative = new CUIProp<CUINullRect>()
      {
        LayoutProp = true,
      };

      #region Graphic Props --------------------------------------------------------
      #endregion


      public CUIProp<PaletteOrder> Palette = new CUIProp<PaletteOrder>()
      {
        ShowInDebug = false,
        OnSet = (v, host) =>
        {
          //TODO should this be called in deserialize?
          CUIGlobalStyleResolver.OnComponentStyleChanged(host);
          // foreach (var child in host.Children)
          // {
          //   child.Palette = v;
          // }
        },
      };

      public CUIProp<CUISprite> BackgroundSprite = new CUIProp<CUISprite>()
      {
        Value = CUISprite.Default,
        ShowInDebug = false,
        Validate = (v, host) => v ?? CUISprite.Default,
        OnSet = (v, host) =>
        {
          if (host.ResizeToSprite)
          {
            host.Absolute = host.Absolute with
            {
              Width = v.SourceRect.Width,
              Height = v.SourceRect.Height,
            };
          }

          if (host.IgnoreTransparent)
          {
            Rectangle bounds = host.BackgroundSprite.Texture.Bounds;
            host.TextureData = new Color[bounds.Width * bounds.Height];
            host.BackgroundSprite.Texture.GetData<Color>(host.TextureData);
          }
        },
      };

      public CUIProp<bool> IgnoreTransparent = new CUIProp<bool>()
      {
        OnSet = (v, host) =>
        {
          if (v)
          {
            Rectangle bounds = host.BackgroundSprite.Texture.Bounds;
            host.TextureData = new Color[bounds.Width * bounds.Height];
            host.BackgroundSprite.Texture.GetData<Color>(host.TextureData);
          }
          else
          {
            host.TextureData = null;
          }
        },
      };

      public CUIProp<Color> BackgroundColor = new CUIProp<Color>()
      {
        ShowInDebug = false,
        OnSet = (v, host) =>
        {
          host.BackgroundVisible = v != Color.Transparent;
        },
      };

      public CUIProp<Color> OutlineColor = new CUIProp<Color>()
      {
        ShowInDebug = false,
        OnSet = (v, host) =>
        {
          host.OutlineVisible = v != Color.Transparent;
        },
      };

      public CUIProp<Vector2> Padding = new CUIProp<Vector2>()
      {
        Value = new Vector2(2, 2),
        DecorProp = true,
      };
    }


  }
}