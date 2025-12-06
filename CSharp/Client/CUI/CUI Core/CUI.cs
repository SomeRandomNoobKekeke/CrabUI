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

    private DefaultGameAdapter GameAdapter { get; }

    public void Connect() => GameAdapter.Connect();
    public void Disconnect() => GameAdapter.Disconnect();

    public CUIMainComponent Main;

    public CUI()
    {
      Environment = new CUIEnvironment();
      GameAdapter = new DefaultGameAdapter(Environment);

      Main = new CUIMainComponent();
      Environment.LifeCycle.BeforeDraw += Main.DrawChildren;
    }


    public void Dispose() { instance = null; }
  }
}