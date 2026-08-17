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
      private DummyVisualElement VanillaGUIComponentImage { get; } = new()
      {
        ConsumeMouseEvents = true,
      };

      public EventDispatcher EventDispatcher { get; } = new();

      public void Update(CUIInput Input)
      {
        if (Input.SomethingHappened && Self.Handles.VanillaMouseOnTracker.IsMouseOnVanillaGUIComponent)
        {
          EventDispatcher.Dispatch(VanillaGUIComponentImage, Self.EventConstructor.Events);
        }
      }

      public void CommunicateCUIMouseOnToRunner()
      {
        Self.CUIRunnerHandle.MouseIsOnSomeCUIElement =
          Self.TopMain.EventTargets.TopTarget != null ||
          (
            !Self.Handles.VanillaMouseOnTracker.IsMouseOnVanillaGUIComponent &&
            Self.Main.EventTargets.TopTarget != null
          );
      }
    }

    private VanillaGUILayerImage_Part VanillaGUILayerImage { get; } = new();
  }
}