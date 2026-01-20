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
    public static Logger Logger => Instance.logger;
    private Logger logger = new();

    public static LoggingClass Logging => Instance.logging;
    private LoggingClass logging;

    //TODO there should be another layer of rerouting (-> console, -> some other CUIFrame)
    public class LoggingClass
    {
      private InfoChannel<object, string, object> CUIPropSet = new();
      private InfoChannel<object, string, object> CUILayoutPropSet = new();


      public bool LogCUIProps
      {
        set
        {
          CUIPropSet.Open = value;
          CUILayoutPropSet.Open = value;
        }
      }

      public void LogProp(object host, string name, object value)
      {
        CUI.Logger.LogVars(host, name, value);
      }

      private void MapChannels()
      {
        CUI.InfoChannels.CUIPropSet.Map(CUIPropSet);
        CUI.InfoChannels.CUILayoutPropSet.Map(CUILayoutPropSet);

        CUIPropSet.OnSend = LogProp;
        CUILayoutPropSet.OnSend = LogProp;
      }

      public LoggingClass()
      {
        MapChannels();
      }
    }
  }
}