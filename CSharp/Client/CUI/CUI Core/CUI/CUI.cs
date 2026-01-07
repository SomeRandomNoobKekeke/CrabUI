using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;


namespace CrabUI
{
  public class CUI : IDisposable
  {
    private static CUI instance; public static CUI Instance
    {
      get
      {
        instance ??= new CUI();
        return instance;
      }
    }

    public CUIEnvironment Environment { get; } = new();
    public DefaultGameAdapter GameAdapter { get; } = new();
    public CUIInput Input { get; }
    public CUIMainComponent Main { get; } = new();

    public void Connect() => GameAdapter.Connect(Environment);
    public void Disconnect() => GameAdapter.Disconnect();




    public CUI()
    {
      Input = new CUIInput(Environment);
      SetupUpdateOrder();
    }

    private void SetupUpdateOrder()
    {
      Environment.LifeCycle.Update += () =>
      {
        try
        {
          Input.Update();
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