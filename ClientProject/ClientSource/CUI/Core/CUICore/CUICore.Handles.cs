using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;
using ComponentGenerator;

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

    public interface CUICoreTextureHandle
    {
      public CUITexture2D GetTexture(string path);
    }

    public interface CUICoreHandles : CUICoreIOHandle, CUICoreTextureHandle
    {
      public CUIGraphicsDevice GraphicsDevice { get; }
      public CUIGUI GUI { get; }
    }

    public class CUIRunnerHandle_Part : Part
    {
      public void Update(double totalTime, MouseState mouse, KeyboardState keyboard)
        => Self.LifeCycle.Update(totalTime, mouse, keyboard);
      public void DrawBeforeGUI(CUISpriteBatch spriteBatch) => Self.LifeCycle.DrawBeforeGUI(spriteBatch);
      public void DrawAfterGUI(CUISpriteBatch spriteBatch) => Self.LifeCycle.DrawAfterGUI(spriteBatch);
      public bool IsMouseOnSomeCUIComponent() => Self.LifeCycle.IsMouseOnSomeCUIComponent();
    }

    /// <summary>
    /// This must be set by CUIRunner
    /// </summary>
    public CUICoreHandles Handles { get; set; }
    public CUIRunnerHandle_Part CUIRunnerHandle { get; } = new();
  }
}