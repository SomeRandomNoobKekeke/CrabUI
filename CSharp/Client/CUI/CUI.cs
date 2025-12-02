using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using BaroJunk;

namespace CrabUI
{
  public static class CUI
  {
    public static CUIEnvironment Environment { get; set; } = new();
    private static CUIEnvironmentConnector Connector = new CUIEnvironmentConnector(Environment);

    public static void Connect() => Connector.Connect();
    public static void Disconnect() => Connector.Disconnect();

    public static CUIMainComponent Main = new CUIMainComponent();
  }
}