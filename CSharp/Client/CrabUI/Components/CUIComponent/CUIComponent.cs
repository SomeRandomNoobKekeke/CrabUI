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
using System.Threading;

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
      };

      CUI.OnDispose += () =>
      {
        foreach (int id in ComponentsById.Keys)
        {
          CUIComponent component = null;
          ComponentsById[id].TryGetTarget(out component);
          component?.Dispose();
        }

        ComponentsById.Clear();
        ComponentsByType.Clear();


        dummyComponent = null;
      };
    }



    internal static int MaxID;
    public static Dictionary<int, WeakReference<CUIComponent>> ComponentsById = new();
    public static WeakCatalog<Type, CUIComponent> ComponentsByType = new();

    /// <summary>
    /// This is used to trick vanilla GUI into believing that 
    /// mouse is hovering some component and block clicks
    /// </summary>
    public static GUIButton dummyComponent = new GUIButton(new RectTransform(new Point(0, 0)))
    {
      Text = "DUMMY",
    };
    /// <summary>
    ///  designed to be versatile, in fact never used
    /// </summary>
    public static void RunRecursiveOn(CUIComponent component, Action<CUIComponent> action)
    {
      action(component);
      foreach (CUIComponent child in component.Children)
      {
        RunRecursiveOn(child, action);
      }
    }

    public static void ForEach(Action<CUIComponent> action)
    {
      foreach (int id in ComponentsById.Keys)
      {
        CUIComponent component = null;
        ComponentsById[id].TryGetTarget(out component);
        if (component is not null) action(component);
      }
    }

    public static IEnumerable<Type> GetClassHierarchy(Type type)
    {
      while (type != typeof(Object) && type != null)
      {
        yield return type;
        type = type.BaseType;
      }
    }

    public static IEnumerable<Type> GetReverseClassHierarchy(Type type)
      => CUIComponent.GetClassHierarchy(type).Reverse<Type>();

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
      if (BackgroundVisible) CUI.DrawRectangle(spriteBatch, Real, BackgroundColor * Transparency, BackgroundSprite);

      CUI.DrawBorders(spriteBatch, this);
      // if (Border.Visible) GUI.DrawRectangle(spriteBatch, BorderBox.Position, BorderBox.Size, Border.Color, thickness: Border.Thickness);

      if (OutlineVisible) GUI.DrawRectangle(spriteBatch, OutlineBox.Position, OutlineBox.Size, OutlineColor, thickness: OutlineThickness);

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
      if (CUI.Disposed)
      {
        Disposed = true;
        return;
      }

      ID = MaxID++;

      ComponentsById[ID] = new WeakReference<CUIComponent>(this);
      ComponentsByType.Add(this.GetType(), this);

      Vitalize();
      VitalizeProps();

      SetupCommands();

      Layout = new CUILayoutSimple();

      SetupStyles();
      SetupAnimations();
    }

    public CUIComponent(float? x = null, float? y = null, float? w = null, float? h = null) : this()
    {
      Relative = new CUINullRect(x, y, w, h);
    }

    public bool Disposed;
    public void Dispose()
    {
      if (Disposed) return;
      CleanUp();
      Disposed = true;
    }
    public virtual void CleanUp() { }

    ~CUIComponent() => Dispose();

    public override string ToString() => $"{this.GetType().Name}:{ID}:{AKA}";
    #endregion
  }
}