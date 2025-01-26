using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{
  public partial class CUIComponent
  {

    public class CommandAttribute : System.Attribute { }

    /// <summary>
    /// Just an experiment, inspired by WPF commands, not very usefull
    /// </summary>
    /// <param name="Name"></param>
    public record Command(string Name);


    /// <summary>
    /// All commands
    /// </summary>
    public Dictionary<string, Action> Commands { get; set; } = new();

    /// <summary>
    /// Manually adds command
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddCommad(string name, Action action) => Commands.Add(name, action);
    public void RemoveCommand(string name) => Commands.Remove(name);

    /// <summary>
    /// Methods ending in "Command" will be added as commands
    /// </summary>
    public virtual void AddCommands()
    {
      foreach (MethodInfo mi in this.GetType().GetMethods())
      {
        if (Attribute.IsDefined(mi, typeof(CommandAttribute)))
        {
          try
          {
            string name = mi.Name;
            if (name != "Command" && name.EndsWith("Command"))
            {
              name = name.Substring(0, name.Length - "Command".Length);
            }
            AddCommad(name, mi.CreateDelegate<Action>(this));
          }
          catch (Exception e)
          {
            Info($"{e.Message}\nMethod: {this.GetType()}.{mi.Name}");
          }
        }
      }
    }

    /// <summary>
    /// Dispathes command up the component tree until someone consumes it
    /// </summary>
    /// <param name="command"></param>
    public void Dispatch(Command command)
    {
      if (Commands.ContainsKey(command.Name)) Execute(command);
      else Parent?.Dispatch(command);
    }

    /// <summary>
    /// Will execute action corresponding to this command
    /// </summary>
    /// <param name="commandName"></param>
    public void Execute(string commandName) => Commands.GetValueOrDefault(commandName)?.Invoke();
    public void Execute(Command command) => Commands.GetValueOrDefault(command.Name)?.Invoke();
  }
}