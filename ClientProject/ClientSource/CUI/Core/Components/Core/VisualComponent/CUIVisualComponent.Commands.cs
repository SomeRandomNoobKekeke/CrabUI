using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class CUIVisualComponent
  {
    public virtual RoutableCommandContract? CommandsContract => null;

    [InitMethod]
    protected void InjectCommandContract()
    {

      CommandNode.CommandsContract = CommandsContract;

      if (CommandsContract != null)
      {
        CUI.Logger.Log($"{this} [{CommandNode.CommandsContract}]");
      }

    }

    public Dictionary<string, Action> Reactions
    {
      set
      {
        foreach (var (name, action) in value)
        {
          Commands.ListenFor(name, action);
        }
      }
    }



    protected RoutableCommandNode CommandNode { get; } = new();

    public public_Commands_Part Commands { get; } = new();
    public class public_Commands_Part : Part, IModule
    {
      public void ListenFor(string name, Action action)
      {
        ListenFor(name, (o) => action());
      }

      public void ListenFor<T>(string name, Action<T> action)
      {
        if (typeof(T).IsValueType)//BRUH idk
        {
          ListenFor(name, (o) => { if (o is T) action((T)o); });
        }
        else
        {
          ListenFor(name, (o) => { if (o is T || o is null) action((T)o); });
        }
      }

      public void ListenFor(string name, Action<object> action)
      {
        if (!Self.CommandNode.ListenFor(name, action))
        {
          throw new ContractBrokenException($"[{Self}] can't listen for [{name}]");
        }
      }

      public void SendDown(string name, object data = null)
      {
        if (!Self.CommandNode.SendDown(new RoutableCommand(name, data)))
        {
          throw new ContractBrokenException($"[{Self}] can't send [{name}] down");
        }
      }

      public void SendUp(string name, object data = null)
      {
        if (!Self.CommandNode.SendUp(new RoutableCommand(name, data)))
        {
          throw new ContractBrokenException($"[{Self}] can't send [{name}] up");
        }
      }

      public void Execute(string name, object data = null)
      {
        if (Self.CommandNode.Execute(new RoutableCommand(name, data)))
        {
          throw new ContractBrokenException($"[{Self}] can't execute [{name}]");
        }
      }
    }
  }


}