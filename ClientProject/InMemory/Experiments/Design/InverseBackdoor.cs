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
  /// So, idea is to instead of placing internals inside the host
  /// create full host class with all the stuff and make the Host a facade to its public part
  /// </summary>
  public class InverseBackdoor : Experiment
  {
    public class FullHost
    {
      public class Host
      {
        protected FullHost fullHost;

        public string SuperPublic
        {
          get => fullHost.SuperPublic;
          set => fullHost.SuperPublic = value;
        }

        public Host(FullHost host) => fullHost = host;
      }

      public class Backdoor
      {
        private FullHost Host;

        public string SuperSecret
        {
          get => Host.SuperSecret;
          set => Host.SuperSecret = value;
        }

        public Backdoor(FullHost host) => Host = host;
      }

      private string SuperSecret { get; set; } = "bruh";
      private string SuperPublic { get; set; } = "kek";
    }


    public override void Run()
    {
      FullHost fullHost = new();
      FullHost.Host host = new(fullHost);
      FullHost.Backdoor backdoor = new(fullHost);

      Mod.Logger.Log(backdoor.SuperSecret);
      Mod.Logger.Log(host.SuperPublic);
    }
  }
}