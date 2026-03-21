using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public class DebugHub
  {
    static DebugHub()
    {
      PluginLifeCycle.Stop += Clear;
    }


    public static ClearableEvent<DebugChannelBase> OnMsg { get; } = new();
    public static Dictionary<string, DebugChannelBase> Channels { get; } = new();

    public static void Clear()
    {
      OnMsg.Clear();

      foreach (DebugChannelBase channel in Channels.Values)
      {
        channel.Dispose();
      }

      Channels.Clear();
    }

    public static void Register(DebugChannelBase channel)
    {
      Channels[channel.Name] = channel;
      channel.OnMsg.Add((c) => OnMsg?.Raise(c));
    }

  }
}