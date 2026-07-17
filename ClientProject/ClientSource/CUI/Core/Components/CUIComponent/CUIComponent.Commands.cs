using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUILibs;
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUIComponent
  {

    public public_Commands_Part Commands { get; } = new();
    public class public_Commands_Part : Part, IModule
    {
      public void ListenFor(string name, Action<object> action) => Self.ProtectedCommands.ListenFor(name, action);
      public void ListenFor<T>(string name, Action<T> action) => Self.ProtectedCommands.ListenFor<T>(name, action);
      public void SendDown(string name, object data = null) => Self.ProtectedCommands.SendDown(name, data);
      public void SendUp(string name, object data = null) => Self.ProtectedCommands.SendUp(name, data);
    }



    protected Protected_Commands_Part ProtectedCommands { get; } = new();
    public class Protected_Commands_Part : Part, IModule
    {
      public RoutableCommandNode Node { get; } = new();

      public void OnAttachToParentHandler(CUIComponent parent)
      {
        parent.ProtectedCommands.Node.AddChild(this.Node);
      }

      public void OnDetachFromParentHandler(CUIComponent parent)
      {
        parent.ProtectedCommands.Node.RemoveChild(this.Node);
      }


      public void ListenFor<T>(string name, Action<T> action)
      {
        Node.Listeners[name] = (o) =>
        {
          if (o is T) action((T)o);
        };
      }
      public void ListenFor(string name, Action<object> action)
      {
        Node.Listeners[name] = action;
      }


      public void SendDown(string name, object data = null)
      {
        Node.SendDown(new RoutableCommand(name, data));
      }

      public void SendUp(string name, object data = null)
      {
        Node.SendUp(new RoutableCommand(name, data));
      }
    }
  }


}