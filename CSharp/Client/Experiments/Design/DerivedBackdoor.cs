using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;

using System.Diagnostics;

namespace CrabUIUser
{

  /// <summary>
  /// This doesn't work
  /// Backdoor has to also be defined inside Host
  /// </summary>
  public class DerivedBackdoor : Experiment
  {
    public class Host
    {
      public class BackdoorBase
      {
        protected Host Host;
        public BackdoorBase(Host host) => Host = host;
      }

      private string SuperSecret { get; set; } = "bruh";
    }



    public class Backdoor : Host.BackdoorBase
    {
      public string SuperSecret
      {
        get => Host.SuperSecret;
        set => Host.SuperSecret = value;
      }

      public Backdoor(Host host) : base(host) { }
    }



    public override void Run()
    {
      Host host = new Host();

      Backdoor backdoor = new(host);

      Mod.Logger.Log(backdoor.SuperSecret);
    }
  }
}