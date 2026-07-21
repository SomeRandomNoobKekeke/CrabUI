using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using CUICodeGenerator;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{
  public partial class CUICore
  {
    public interface CUICoreIOHandle
    {
      public void SaveXDoc(XDocument xDoc, string path);
      public XDocument LoadXDoc(string path);
    }

    /// <summary>
    /// This is how CUICore interacts with CUIRunner
    /// </summary>
    public interface CUICoreHandles : CUICoreIOHandle
    {
      public CUIGraphicsDevice GraphicsDevice { get; }
      public CUIGUI GUI { get; }
      public CUITextureManagerInternal TextureManager { get; }

      /// <summary>
      /// Should steal focus from other GUI Components
      /// </summary>
      public void GrabFocus();
      public void ClearFocus();
    }

    /// <summary>
    /// This is how CUIRunner interacts with CUICore
    /// </summary>
    public class CUIRunnerHandle_Part : Part
    {
      public void Update(double totalTime, MouseState mouse, KeyboardState keyboard, TextInputEventPack textInput)
        => Self.LifeCycle.Update(totalTime, mouse, keyboard, textInput);
      public void DrawBeforeGUI(CUISpriteBatch spriteBatch) => Self.LifeCycle.DrawBeforeGUI(spriteBatch);
      public void DrawAfterGUI(CUISpriteBatch spriteBatch) => Self.LifeCycle.DrawAfterGUI(spriteBatch);
      public bool IsMouseOnSomeCUIComponent() => Self.LifeCycle.IsMouseOnSomeCUIComponent();

      public void AddAssemblyInfo(CUIAssemblyInfo assemblyInfo) => Self._Reflection.AddAssemblyInfo(assemblyInfo);
    }

    /// <summary>
    /// This must be set by CUIRunner
    /// </summary>
    public CUICoreHandles Handles { get; set; }
    public CUIRunnerHandle_Part CUIRunnerHandle { get; } = new();
  }
}