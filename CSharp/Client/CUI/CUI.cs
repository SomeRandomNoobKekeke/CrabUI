using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;

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

    public CUIEnvironment Environment { get; }
    private CUIEnvironmentConnector Connector { get; }

    public void Connect() => Connector.Connect();
    public void Disconnect() => Connector.Disconnect();

    public CUIMainComponent Main;

    public CUI()
    {
      Environment = new CUIEnvironment();
      Connector = new CUIEnvironmentConnector(Environment);

      Main = new CUIMainComponent();
      Environment.LifeCycle.AfterDraw += Main.DrawChildren;
    }


    public void Dispose() { instance = null; }
  }
}