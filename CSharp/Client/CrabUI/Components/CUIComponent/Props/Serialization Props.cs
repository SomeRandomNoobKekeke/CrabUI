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
    /// If false then this component and its children won't be serialized
    /// </summary>
    [CUISerializable] public bool Serializable { get; set; } = true;
    /// <summary>
    /// Is this a serialization cutoff point  
    /// Parent will serialize children down to this component and stop
    /// </summary>
    [CUISerializable] public bool SerializeChildren { get; set; } = true;
    /// <summary>
    /// If true it won't be deserialized,  
    /// Instead its children will be detached and attached to the component
    /// with matching AKA on the parent
    /// </summary>
    [CUISerializable] public bool MergeSerialization { get; set; } = false;
    /// <summary>
    /// If true, deserialized component will replace existing component with the 
    /// same AKA instead of creating a duplicate
    /// </summary>
    [CUISerializable] public bool ReplaceSerialization { get; set; } = false;
    [CUISerializable] public static bool ForceSaveAllProps { get; set; } = false;
  }
}