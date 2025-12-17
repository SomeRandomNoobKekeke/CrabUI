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

    public CUIEnvironment Environment { get; }
    public DefaultGameAdapter GameAdapter { get; }

    public CUIInput Input { get; }

    public void Connect() => GameAdapter.Connect(Environment);
    public void Disconnect() => GameAdapter.Disconnect();

    public CUIMainComponent Main;


    public CUI()
    {
      Environment = new CUIEnvironment();
      GameAdapter = new DefaultGameAdapter();

      Input = new CUIInput();
      Input.AttachToEnvironment(Environment);


      Main = new CUIMainComponent();
      Main.AttachToEnvironment(Environment);
    }


    public void Dispose() { instance = null; }
  }
}