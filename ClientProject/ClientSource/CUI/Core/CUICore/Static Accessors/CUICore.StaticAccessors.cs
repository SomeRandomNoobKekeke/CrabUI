using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using CUICodeGenerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{

  public partial class CUICore
  {
    public static CUICore Instance => CUI.Core;

    public static void SaveXDoc(XDocument xDoc, string path)
      => Instance.Handles.SaveXDoc(xDoc, path);
    public static XDocument LoadXDoc(string path)
      => Instance.Handles.LoadXDoc(path);

    public static CUIParser Parser => Instance._CUIParser;
    public static CUISerializer Serializer => Instance._CUISerializer;

    public static CUIGraphicsDevice GraphicsDevice => Instance.Handles.GraphicsDevice;
    public static SamplerState SamplerState => Instance.Handles.GUI.SamplerState;
    public static RasterizerState RasterizerState => Instance.Handles.GUI.RasterizerState;

    public static CUIAssemblyAnalyzer CUITypes => Instance._Analyzer;
    public static CUIStyleManager Styles => Instance.CUIStyleManager;
    public static CUIPaletteManager Palettes => Instance.CUIPaletteManager;
    public static AnimationPlayer AnimationPlayer => Instance._AnimationPlayer;
    public static CUIInput Input => Instance._Input;

    public static CUITextureManager TextureManager => Instance.Handles.CUITextureManager;

    public static event Action<double> OnUpdate
    {
      add => Instance.LifeCycle.OnUpdate.Add(value);
      remove => Instance.LifeCycle.OnUpdate.Remove(value);
    }

    public static event Action<CUISpriteBatch> OnDrawAfterGUI
    {
      add => Instance.LifeCycle.OnDrawAfterGUI.Add(value);
      remove => Instance.LifeCycle.OnDrawAfterGUI.Remove(value);
    }

    public static event Action<CUISpriteBatch> OnDrawBeforeGUI
    {
      add => Instance.LifeCycle.OnDrawBeforeGUI.Add(value);
      remove => Instance.LifeCycle.OnDrawBeforeGUI.Remove(value);
    }

    public static void RequestFocus(IFocusable focusable)
      => Instance.GlobalFocusTracker.WantsToBeFocused = focusable;
  }
}