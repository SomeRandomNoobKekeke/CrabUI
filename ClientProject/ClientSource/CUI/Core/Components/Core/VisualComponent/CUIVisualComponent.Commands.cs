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

    public Commands_Part Commands { get; } = new();
    public class Commands_Part : Part
    {
      private DictOfLists<string, Action<object>> Listeners { get; } = new();

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
        if (Self.CommandsContract?.CanConsume(name) == false)
        {
          throw new ContractBrokenException($"[{Self}] can't listen for [{name}]");
        }

        Listeners.Add(name, action);
      }

      public void Execute(string name, object data = null)
      {
        if (Self.CommandsContract?.CanConsume(name) == false)
        {
          throw new ContractBrokenException($"[{Self}] can't execute [{name}]");
        }

        foreach (Action<object> action in Listeners[name])
        {
          action(data);
        }
      }

      public void SendDown(string name, object data = null)
      {
        if (Self.CommandsContract?.CanSendDown(name) == false)
        {
          throw new ContractBrokenException($"[{Self}] can't send [{name}] down");
        }

        for (int i = Self.Children.Count - 1; i >= 0; i--)
        {
          if (Self.Children[i].Commands.Listeners.ContainsKey(name))
          {
            Self.Children[i].Commands.Execute(name, data);
          }
          else
          {
            Self.Children[i].Commands.SendDown(name, data);
          }
        }
      }

      public void SendUp(string name, object data = null)
      {
        if (Self.CommandsContract?.CanSendUp(name) == false)
        {
          throw new ContractBrokenException($"[{Self}] can't send [{name}] up");
        }

        if (Self.Parent == null) return;

        if (Self.Parent.Commands.Listeners.ContainsKey(name))
        {
          Self.Parent?.Commands.Execute(name, data);
        }
        else
        {
          Self.Parent?.Commands.SendUp(name, data);
        }
      }
    }


  }


}