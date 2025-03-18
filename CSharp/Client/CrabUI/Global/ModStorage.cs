using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;


namespace CrabUI
{
  /// <summary>
  /// Global data repository  
  /// Treat it as Dictionary<string, object>  
  /// In fact data is stored in GUI.Canvas.GUIComponent.Userdata
  /// </summary>
  public class ModStorage
  {
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

    public static object Get<TValue>(string key) => (TValue)Get(key);
    public static object Get(string key)
    {
      Dictionary<string, object> repo = GetOrCreateRepo();
      return repo.GetValueOrDefault(key);
    }
    public static void Set(string key, object value)
    {
      Dictionary<string, object> repo = GetOrCreateRepo();
      repo[key] = value;
    }
    public static bool Has(string key)
    {
      Dictionary<string, object> repo = GetOrCreateRepo();
      return repo.ContainsKey(key);
    }
  }
}
