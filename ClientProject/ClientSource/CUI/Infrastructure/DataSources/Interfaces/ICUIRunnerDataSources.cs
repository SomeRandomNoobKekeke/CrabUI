using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  public interface ICUIRunnerDataSources
  {
    public void ConnectToGame();
    public void DisconnectFromGame();

    public event Action<SpriteBatch> BeforeGUIDraw;
    public event Action<SpriteBatch> AfterGUIDraw;
    public event Action<GameTime> Update;
    public event Action SyncMouseOn;
    public event Action VanillaGUIElementFocused;

    public void GrabFocus(IFocusable focusable);

    public MouseState ScanMouse();
    public KeyboardState ScanKeyboard();
    public TextInputEventPack ScanTextInput();
  }
}