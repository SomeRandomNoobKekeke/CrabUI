using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;

namespace CrabUI
{
  public partial class CUI : IDisposable
  {
    private static bool creatingInstance;
    private static CUI instance; public static CUI Instance
    {
      get
      {
        if (creatingInstance) throw new Exception("Attempt to access CUI instance during creation");

        if (instance is null)
        {
          creatingInstance = true;
          instance ??= new CUI();
          creatingInstance = false;
          instance.Initialize();
        }

        return instance;
      }
    }




    public CUIEnvironment Environment { get; private set; } = new();
    public DefaultGameAdapter GameAdapter { get; private set; } = new();
    public CUIInput Input { get; private set; }
    public CUIMainComponent Main { get; private set; } = new();
    public InputSettings InputSettings { get; private set; }

    public void Connect() => GameAdapter.Connect(Environment);
    public void Disconnect() => GameAdapter.Disconnect();

    public CUI()
    {

    }
    private void Initialize()
    {
      InputSettings = new InputSettings();
      Input = new CUIInput(Environment, InputSettings);
      logging = new LoggingClass();
      SetupUpdateOrder();
    }

    private void SetupUpdateOrder()
    {
      Environment.LifeCycle.Update += () =>
      {
        try
        {
          Input.Update(Environment.TotalTime);
          Main.Update();
        }
        catch (Exception e)
        {
          Logger.Default.Error(e);
        }
      };

      Environment.LifeCycle.DrawBeforeGUI += (spritebatch) =>
      {
        Main.DrawChildren(spritebatch);
      };
    }


    public void Dispose() { instance = null; }
  }
}