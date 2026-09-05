using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;

namespace CursedUI
{
  /// <summary>
  /// This is additional info about CUIVisualComponent type
  /// </summary>
  public class CUIVisualComponentInfo
  {
    public Type ComponentType { get; set; }
    public Dictionary<string, PropertyPath> SerializableProps { get; set; } = new();
    public ICUIStyle? DefaultStyle { get; set; }


    public bool NoDefault { get; set; }
    private CUIVisualComponent _DefaultValue; public CUIVisualComponent DefaultValue
    {
      get
      {
        if (!AlreadyTriedToCreateDefault)
        {
          _DefaultValue = CreateDefault();
        }
        return _DefaultValue;
      }
    }
    private bool AlreadyTriedToCreateDefault;
    private CUIVisualComponent CreateDefault()
    {
      AlreadyTriedToCreateDefault = true;

      if (NoDefault) return null;
      if (ComponentType.IsAbstract) return null;

      if (ComponentType.GetConstructor([]) is null)
      {
        // CUI.Logger.Warning($"Failed to create default for [{info.ComponentType.Name}]: {info.ComponentType} doesn't have default constructor");
        return null;
      }

      try
      {
        CUICore.TextureManager.DummyMode = true;
        CUIVisualComponent result = (CUIVisualComponent)Activator.CreateInstance(ComponentType);
        CUICore.TextureManager.DummyMode = false;

        return result;
      }
      catch (Exception e)
      {
        CUI.Logger.Warning($"Failed to create default for [{ComponentType.Name}]: {e.InnerException?.Message}");
        if (CUI.ErrorHandlingStrategy == ErrorHandlingStrategy.FailFast) throw;
      }

      return null;
    }

    public override string ToString() => $"{(DefaultStyle == null ? "" : "[has DefaultStyle]")} {(DefaultValue == null ? "" : "[has DefaultValue]")} [{SerializableProps.Count} props]";



  }
}