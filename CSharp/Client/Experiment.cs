using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using BaroJunk;
using CrabUI;

namespace CrabUIUser
{
  public partial class Mod : IAssemblyPlugin
  {


    public enum LogType
    {
      Info, Warning, Error
    }

    public class LogChannel<T> : InfoChannelBase<LogType, T>
    {
      public LogType Type { get; set; } = LogType.Info;
      public void Log(T msg) => Send(Type, msg);
    }

    public class LogChannelRouter<T> : InfoChannelRouter<LogType, T>
    {
      new public void Route(LogChannel<T> channel) => base.Route(channel);
      new public void UnRoute(LogChannel<T> channel) => base.UnRoute(channel);
    }

    public void Experiment()
    {
      LogChannel<string> channel = new LogChannel<string>()
      {
        Open = true,
        OnSend = (type, msg) => Mod.Logger.Log($"Channel| {type}: {msg}")
      };

      LogChannelRouter<string> router = new() { Open = true };

      router.Route(channel);
      router.OnSend = (type, msg) => Mod.Logger.Log($"Router| {type}: {msg}");


      channel.Log("123");

    }
  }
}