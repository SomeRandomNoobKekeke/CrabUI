#if !SERVER
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;

namespace BaroJunk
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
    static ModStorage() => ProjectInfo.Add(new PackageInfo()
    {
      Name = "ModStorage",
      Version = new Version(0, 0, 0)
      {
        Branch = "BaroJunk"
      }
    });

    public static TValue Get<TValue>(string key) => (TValue)Get(key);

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
