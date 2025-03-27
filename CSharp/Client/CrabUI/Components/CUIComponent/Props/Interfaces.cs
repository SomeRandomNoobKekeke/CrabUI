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
  /// <summary>
  /// Ignore this, i'm just thinking how to organize props, this is getting out of hand
  /// </summary>
  public interface ICUIComponent : ISimpleProps, IListProps, ISerializationProps
  {

  }

  /// <summary>
  /// Props used in simple layout
  /// </summary>
  public interface ISimpleProps
  {
    public CUINullRect Relative { get; set; }
    public CUINullRect RelativeMin { get; set; }
    public CUINullRect RelativeMax { get; set; }
    public CUINullRect CrossRelative { get; set; }

    public CUINullRect Absolute { get; set; }
    public CUINullRect AbsoluteMin { get; set; }
    public CUINullRect AbsoluteMax { get; set; }

    public CUIBool2 FitContent { get; set; }
    public CUIBool2 Ghost { get; set; }
  }

  /// <summary>
  /// Props used in list layouts
  /// </summary>
  public interface IListProps
  {
    public CUIBool2 FillEmptySpace { get; set; }
  }

  /// <summary>
  /// Props used in serialization
  /// </summary>
  public interface ISerializationProps
  {
    public bool Serializable { get; set; }
    public bool SerializeChildren { get; set; }
    public bool MergeSerialization { get; set; }
    public bool ReplaceSerialization { get; set; }
    public static bool ForceSaveAllProps { get; set; }
  }
}