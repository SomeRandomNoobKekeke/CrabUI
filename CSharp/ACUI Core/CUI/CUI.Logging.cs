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
    public static Logger Logger = new()
    {
      PrintFilePath = false,
    };

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
        CUI.Logger.Log($"CUIProp set {host}.{name} = [{Logger.WrapInColor(value, "white")}]");
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

        LogCUIProps = false;
      }
    }
  }
}