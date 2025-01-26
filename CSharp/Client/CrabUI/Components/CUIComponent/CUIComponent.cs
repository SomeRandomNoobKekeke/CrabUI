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
  /// <summary>
  /// Base class for all components
  /// </summary>
  public partial class CUIComponent : IDisposable
  {
    #region Static --------------------------------------------------------
    internal static void InitStatic()
    {
      CUI.OnInit += () =>
      {
        MaxID = 0;
        ComponentsById = new Dictionary<int, CUIComponent>();
      };

      CUI.OnDispose += () =>
      {
        ComponentsById.Clear();
        dummyComponent = null;
      };
    }



    internal static int MaxID;
    public static Dictionary<int, CUIComponent> ComponentsById;

    /// <summary>
    /// This is used to trick vanilla GUI into believing that 
    /// mouse is hovering some component and block clicks
    /// </summary>
    public static GUIButton dummyComponent = new GUIButton(new RectTransform(new Point(0, 0)));
    /// <summary>
    ///  designed to be versatile, in fact never used
    /// </summary>
    public static void RunRecursiveOn(CUIComponent component, Action<CUIComponent, int> action, int depth = 0)
    {
      action(component, depth);
      foreach (CUIComponent child in component.Children)
      {
        RunRecursiveOn(child, action, depth + 1);
      }
    }

    #endregion
    #region Virtual --------------------------------------------------------


    //TODO move to cui props, it's a bit more clampicated than ChildrenBoundaries
    /// <summary>
    /// Bounds for offset, e.g. scroll, zoom
    /// </summary>
    internal virtual CUIBoundaries ChildOffsetBounds => new CUIBoundaries();
    /// <summary>
    /// "Component like" ghost stuff that can't have children and
    /// doesn't impact layout. Drag handles, text etc 
    /// </summary>
    internal virtual void UpdatePseudoChildren()
    {
      LeftResizeHandle.Update();
      RightResizeHandle.Update();
    }
    /// <summary>
    /// Last chance to disagree with proposed size
    /// For stuff that should resize to content
    /// </summary>
    /// <param name="size"> proposed size </param>
    /// <returns> size you're ok with </returns>
    internal virtual Vector2 AmIOkWithThisSize(Vector2 size) => size;
    /// <summary>
    /// Here component should be drawn
    /// </summary>
    /// <param name="spriteBatch"></param>
    public virtual partial void Draw(SpriteBatch spriteBatch);
    /// <summary>
    /// Method for drawing something that should always be on top, e.g. resize handles
    /// </summary>
    /// <param name="spriteBatch"></param>
    public virtual partial void DrawFront(SpriteBatch spriteBatch);

    #endregion
    #region Draw --------------------------------------------------------

    public virtual partial void Draw(SpriteBatch spriteBatch)
    {
      if (BackgroundVisible) CUI.DrawRectangle(spriteBatch, Real, BackgroundColor, BackgroundSprite);

      if (BorderVisible) GUI.DrawRectangle(spriteBatch, BorderBox.Position, BorderBox.Size, BorderColor, thickness: BorderThickness);

      LeftResizeHandle.Draw(spriteBatch);
      RightResizeHandle.Draw(spriteBatch);
    }

    public virtual partial void DrawFront(SpriteBatch spriteBatch)
    {
      if (DebugHighlight)
      {
        GUI.DrawRectangle(spriteBatch, Real.Position, Real.Size, Color.Cyan * 0.5f, isFilled: true);
      }
    }


    #endregion
    #region Constructors --------------------------------------------------------
    internal void Vitalize()
    {
      foreach (FieldInfo fi in this.GetType().GetFields(AccessTools.all))
      {
        if (fi.FieldType.IsAssignableTo(typeof(ICUIVitalizable)))
        {
          ICUIVitalizable prop = (ICUIVitalizable)fi.GetValue(this);
          if (prop == null) continue;
          prop.SetHost(this);
        }
      }
    }
    internal void VitalizeProps()
    {
      foreach (FieldInfo fi in this.GetType().GetFields(AccessTools.all))
      {
        if (fi.FieldType.IsAssignableTo(typeof(ICUIProp)))
        {
          ICUIProp prop = (ICUIProp)fi.GetValue(this);
          if (prop == null) continue; // this is for Main.GrabbedDragHandle
          prop.SetHost(this);
          prop.SetName(fi.Name);
        }
      }

      foreach (FieldInfo fi in typeof(CUIComponentProps).GetFields(AccessTools.all))
      {
        if (fi.FieldType.IsAssignableTo(typeof(ICUIProp)))
        {
          ICUIProp prop = (ICUIProp)fi.GetValue(CUIProps);
          if (prop == null) continue;
          prop.SetHost(this);
          prop.SetName(fi.Name);
        }
      }
    }

    public CUIComponent()
    {
      ID = MaxID++;
      ComponentsById[ID] = this;

      Vitalize();
      VitalizeProps();
      AddCommands();

      BackgroundColor = CUIPallete.Default.Primary.Off;
      BorderColor = CUIPallete.Default.Primary.Border;

      Layout = new CUILayoutSimple();
    }

    public CUIComponent(float? x = null, float? y = null, float? w = null, float? h = null) : this()
    {
      Relative = new CUINullRect(x, y, w, h);
    }

    /// <summary>
    /// Will just RemoveSelf()
    /// </summary>
    public void Dispose()
    {
      RemoveSelf();
    }

    public override string ToString() => $"{this.GetType().Name}:{ID}:{AKA}";
    #endregion
  }
}