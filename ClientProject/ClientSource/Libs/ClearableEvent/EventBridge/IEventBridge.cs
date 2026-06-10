using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace BaroJunk
{
  public interface IEventBridge
  {
    // public Delegate AbstractAction { get; set; }
    public bool Opened { get; }
    public void Open();
    public void Close();
  }
}