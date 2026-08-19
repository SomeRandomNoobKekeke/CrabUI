using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Barotrauma;

namespace CrabUI
{
  public partial class CUICore
  {
    public class VanillaGUILayerImage_Part : Part
    {

      public bool MouseOn => Self.Handles.IsMouseOnVanillaGUIComponent;
      public bool Focused => Self.Handles.IsMouseOnVanillaGUIComponent;

      private DummyVisualElement VanillaGUIComponentImage { get; } = new()
      {
        ConsumeMouseEvents = true,
      };

      private EventDispatcher EventDispatcher { get; } = new();

      public void Update(CUIInput Input)
      {
        if (Input.SomethingHappened && Self.Handles.IsMouseOnVanillaGUIComponent)
        {
          EventDispatcher.Dispatch(VanillaGUIComponentImage, Self._EventConstructor.Events);
        }
      }

      public void CommunicateCUIMouseOnToRunner()
      {
        Self.CUIRunnerHandle.MouseIsOnSomeCUIElement =
          Self.TopMain.EventTargets.TopTarget != null ||
          !MouseOn && Self.Main.EventTargets.TopTarget != null;
      }
    }

    private VanillaGUILayerImage_Part VanillaGUILayer { get; } = new();
  }
}