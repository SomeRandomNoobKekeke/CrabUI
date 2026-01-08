using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;
using BaroJunk;

namespace CrabUI
{
  public class DefaultGameAdapter
  {
    public DefaultInputProvider DefaultInputProvider = new();
    public DefaultLifeCycleAdapter DefaultLifeCycleAdapter = new();

    public CUIEnvironment ConnectedEnvironment { get; private set; }
    public bool IsConnected => ConnectedEnvironment is not null;

    public void Connect(CUIEnvironment environment)
    {
      ConnectedEnvironment = environment;

      DefaultLifeCycleAdapter.Connect(environment);
      environment.InputScanner.InputProvider = DefaultInputProvider;
    }

    public void Disconnect()
    {
      DefaultLifeCycleAdapter.Disconnect();
      ConnectedEnvironment.InputScanner.InputProvider = null;

      ConnectedEnvironment = null;
    }
  }
}