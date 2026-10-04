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
    private static IDictionary<string, object> Repo
    {
      get
      {
        if (GUI.Canvas.GUIComponent?.UserData is not IDictionary<string, object>)
        {
          GUI.Canvas.GUIComponent = new GUIButton(new RectTransform(new Point(0, 0)))
          {
            UserData = new Dictionary<string, object>()
          };
        }

        return GUI.Canvas.GUIComponent?.UserData as IDictionary<string, object>;
      }
    }


    public static void Print() => DeepDictAccess.Print(Repo);
    public static bool Has(string path) => DeepDictAccess.Has(path, Repo);
    public static void Set(string path, object value) => DeepDictAccess.Set(path, value, Repo);
    public static void Remove(string path) => DeepDictAccess.Remove(path, Repo);
    public static T Get<T>(string path) => DeepDictAccess.Get<T>(path, Repo);
    public static object Get(string path) => DeepDictAccess.Get(path, Repo);
    public static bool TryGetValue(string path, out object result) => DeepDictAccess.TryGetValue(path, Repo, out result);
  }
}
#endif
