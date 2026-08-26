using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;

using System.Diagnostics;

namespace CursedUIUser
{

  /// <summary>
  /// only ModuleAClass and ModuleBlass have access to PrivateRoom.PrivateChat
  /// </summary>
  public class PrivateRoom : Experiment
  {
    public class Host
    {
      public class PrivateRoomClass
      {
        public class PrivateChatClass
        {
          public string MsgChannel { get; set; } = "kto prochital tot sdohnet";
        }

        private PrivateChatClass PrivateChat { get; set; } = new();

        public class ModuleAClass
        {
          public Host Host { get; set; }
          protected PrivateRoomClass PrivateRoom => Host.PrivateRoom;

          public void WriteChat() => PrivateRoom.PrivateChat.MsgChannel = "hi lol";
        }

        public class ModuleBClass
        {
          public Host Host { get; set; }
          protected PrivateRoomClass PrivateRoom => Host.PrivateRoom;

          public string ReadChat() => PrivateRoom.PrivateChat.MsgChannel;
        }
      }



      protected PrivateRoomClass PrivateRoom { get; set; } = new();

      public PrivateRoomClass.ModuleAClass ModuleA { get; set; } = new();
      public PrivateRoomClass.ModuleBClass ModuleB { get; set; } = new();

      private void InjectModules()
      {
        ModuleA.Host = this;
        ModuleB.Host = this;
      }

      public Host()
      {
        InjectModules();
      }
    }


    public override void Run()
    {
      Host host = new Host();


      Mod.Logger.Log(host.ModuleB.ReadChat());
      host.ModuleA.WriteChat();
      Mod.Logger.Log(host.ModuleB.ReadChat());
    }
  }
}