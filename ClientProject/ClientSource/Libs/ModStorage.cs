#if !SERVER
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;

namespace CUILibs
{
  /// <summary>
  /// Global data repository
  /// Treat it as Dictionary<string, object>  
  /// Data is accessible to all mods and persist between reloads
  /// Works only on client
  /// In fact data is stored in GUI.Canvas.GUIComponent.Userdata
  /// </summary>
  public static class ModStorage
  {
    public static bool Debug { get; set; } = false;

    public static TValue Get<TValue>(string key) => (TValue)Get(key);

    public static object Get(string key)
    {
      Dictionary<string, object> repo = GetOrCreateRepo();
      return repo.GetValueOrDefault(key);
    }

    public static void Set(string key, object value)
    {
      if (Debug)
      {
        Logger.Default.Log($"\n\nModStorage| [{Logger.WrapInColor(key, "white")}] = {Logger.WrapInColor(value, "white")}");
        Logger.Default.PrintStackTrace();
      }

      Dictionary<string, object> repo = GetOrCreateRepo();
      repo[key] = value;
    }

    public static bool Has(string key)
    {
      Dictionary<string, object> repo = GetOrCreateRepo();
      return repo.ContainsKey(key);
    }

    public static void Remove(string key)
    {
      if (Debug)
      {
        Logger.Default.Log($"\n\nModStorage| [{Logger.WrapInColor(key, "white")}] removed");
        Logger.Default.PrintStackTrace();
      }

      Dictionary<string, object> repo = GetOrCreateRepo();
      repo.Remove(key);
    }


    private static Dictionary<string, object> GetOrCreateRepo()
    {
      if (GUI.Canvas.GUIComponent is not GUIButton)
      {
        GUI.Canvas.GUIComponent = new GUIButton(new RectTransform(new Point(0, 0)));
      }

      if (GUI.Canvas.GUIComponent.UserData is not Dictionary<string, object>)
      {
        GUI.Canvas.GUIComponent.UserData = new Dictionary<string, object>();
      }

      return (Dictionary<string, object>)GUI.Canvas.GUIComponent.UserData;
    }
  }
}
#endif
