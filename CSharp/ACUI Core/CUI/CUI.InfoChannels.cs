using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;

namespace CrabUI
{
  public partial class CUI : IDisposable
  {
    public static InfoChannelsWrapper InfoChannels => Instance.infoChannels;
    private InfoChannelsWrapper infoChannels = new();


    /// <summary>
    /// Aggregate channels, always open
    /// </summary>
    public class InfoChannelsWrapper
    {
      public InfoChannel<object, string, object> CUIPropSet = new();
      public InfoChannel<object, string, object> CUILayoutPropSet = new();
    }
  }
}