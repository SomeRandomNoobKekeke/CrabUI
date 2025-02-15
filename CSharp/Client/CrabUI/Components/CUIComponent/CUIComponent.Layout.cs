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
    #region Layout --------------------------------------------------------

    protected CUILayout layout;
    //[CUISerializable]
    public virtual CUILayout Layout
    {
      get => layout;
      set { layout = value; layout.Host = this; }
    }

    public event Action OnLayoutUpdated;
    public void InvokeOnLayoutUpdated() => OnLayoutUpdated?.Invoke();

    /// <summary>
    /// Triggers recalculation of layouts from parent and below
    /// </summary>
    internal void OnPropChanged([CallerMemberName] string memberName = "")
    {
      Layout.Changed = true;
      CUIDebug.Capture(null, this, "OnPropChanged", memberName, "Layout.Changed", "true");
      MainComponent?.LayoutChanged();
    }
    internal void OnSelfAndParentChanged([CallerMemberName] string memberName = "")
    {
      Layout.SelfAndParentChanged = true;
      CUIDebug.Capture(null, this, "OnSelfAndParentChanged", memberName, "Layout.SelfAndParentChanged", "true");
      MainComponent?.LayoutChanged();
    }

    /// <summary>
    /// Triggers recalc of own pseudo components and nothing else
    /// </summary>
    internal void OnDecorPropChanged([CallerMemberName] string memberName = "")
    {
      Layout.DecorChanged = true;
      CUIDebug.Capture(null, this, "OnDecorPropChanged", memberName, "Layout.DecorChanged", "true");
      MainComponent?.LayoutChanged();
    }
    /// <summary>
    /// Notifies parent (only) than it may need to ResizeToContent
    /// </summary>
    internal void OnAbsolutePropChanged([CallerMemberName] string memberName = "")
    {
      Layout.AbsoluteChanged = true;
      CUIDebug.Capture(null, this, "OnAbsolutePropChanged", memberName, "Layout.AbsoluteChanged", "true");
      MainComponent?.LayoutChanged();
    }
    /// <summary>
    /// Triggers recalculation of layouts from this and below
    /// </summary>
    internal void OnChildrenPropChanged([CallerMemberName] string memberName = "")
    {
      Layout.ChildChanged = true;
      MainComponent?.LayoutChanged();
    }

    #endregion

  }
}