using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
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
    public static bool InputBlockingMenuOpen => Instance.Handles.InputBlockingMenuOpen;

    public static Reflection_Part Reflection => Instance._Reflection;
    public static CUIStyleManager Styles => Instance.CUIStyleManager;
    public static CUIPalettes Palettes => Instance.CUIPalettes;
    public static AnimationPlayer AnimationPlayer => Instance._AnimationPlayer;
    public static CUIInput Input => Instance._Input;

    public static CUITextureManager TextureManager => Instance.Handles.TextureManager;
    public static ResourceIOContextHandle ResourceIOContext => Instance.Handles.ResourceIOContext;

    public static bool Debug
    {
      get => Instance._Debug;
      set => Instance._Debug = value;
    }
    public static DebugHub DebugHub => Instance._DebugHub;

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

    public static void RequestBlur(IFocusable focusable)
      => Instance.GlobalFocusTracker.AddToWantsToBeBlured(focusable);
  }
}