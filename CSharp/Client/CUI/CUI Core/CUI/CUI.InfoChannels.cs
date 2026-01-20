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


    public class InfoChannelsWrapper
    {
      public InfoChannel<object, string, object> CUIPropSet = new()
      {
        OnSend = (host, name, value) => CUI.Logger.LogVars(host, name, value),
      };
    }
  }
}