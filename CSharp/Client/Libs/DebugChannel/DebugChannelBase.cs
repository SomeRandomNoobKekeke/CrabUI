using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public abstract class DebugChannelBase : IDisposable
  {
    private string name; public required string Name
    {
      get => name;
      set
      {
        name = value;
        Register();
      }
    }
    public bool Open { get; set; }

    public abstract string Msg { get; }
    public abstract object[] Args { get; }

    public ClearableEvent<DebugChannelBase> OnMsg { get; } = new();

    private void Register()
    {
      DebugHub.Register(this);
    }

    public virtual void Dispose()
    {
      Name = null;
      OnMsg.Clear();
    }

    public override string ToString() => $"Debug channel [{Name}]";
  }
}