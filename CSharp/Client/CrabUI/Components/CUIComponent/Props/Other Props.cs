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
    /// Is this a serialization cutoff point  
    /// Parent will serialize children down to this component
    /// Further serialization should be hadled by this component
    /// </summary>
    [CUISerializable] public bool BreakSerialization { get; set; }
    /// <summary>
    /// Some props (like visible) are autopassed to all new childs
    /// see PassPropsToChild
    /// </summary>
    [CUISerializable] public bool ShouldPassPropsToChildren { get; set; } = true;
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
    [CUISerializable] public bool IgnoreParentTransparency { get; set; }

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
    /// Won't react to mouse events
    /// </summary>
    [CUISerializable]
    public bool IgnoreEvents
    {
      get => CUIProps.IgnoreEvents.Value;
      set => CUIProps.IgnoreEvents.SetValue(value);
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
  }
}