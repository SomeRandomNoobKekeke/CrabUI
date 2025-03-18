using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{

  public class CommandAttribute : System.Attribute { }

  /// <summary>
  /// Can be dispatched up the component tree to notify parent about something 
  /// add pass some event data without creating a hard link
  /// </summary>
  /// <param name="Name"></param>
  public record CUICommand(string Name, object Data = null);

  /// <summary>
  /// Can be dispatched down the component tree to pass some data to the children
  /// without creating a hard link
  /// </summary>
  public record CUIData(string Name, object Data = null);
  public partial class CUIComponent
  {
    private void SetupCommands()
    {
      // This is actually expensive
      //AddCommands();
      OnTreeChanged += UpdateDataTargets;
    }


    /// <summary>
    /// This command will be dispatched up when some component specific event happens
    /// </summary>
    [CUISerializable] public string Command { get; set; }
    /// <summary>
    /// this will be executed on any command
    /// </summary>
    public event Action<CUICommand> OnAnyCommand;
    /// <summary>
    /// Will be executed when receiving any data
    /// </summary>
    public event Action<CUIData> OnAnyData;
    /// <summary>
    /// Happens when appropriate data is received
    /// </summary>
    public event Action<Object> OnConsume;
    /// <summary>
    /// Will consume data with this name
    /// </summary>
    [CUISerializable] public string Consumes { get; set; }

    private bool reflectCommands;
    [CUISerializable]
    public bool ReflectCommands
    {
      get => reflectCommands;
      set
      {
        reflectCommands = value;
        OnAnyCommand += (command) =>
        {
          foreach (CUIComponent child in Children)
          {
            child.DispatchDown(new CUIData(command.Name, command.Data));
          }
        };
      }
    }

    private bool retranslateCommands;
    [CUISerializable]
    public bool RetranslateCommands
    {
      get => retranslateCommands;
      set
      {
        retranslateCommands = value;
        OnAnyCommand += (command) =>
        {
          Parent?.DispatchUp(command);
        };
      }
    }

    /// <summary>
    /// Optimization to data flow  
    /// If not empty component will search for consumers of the data
    /// and pass it directly to them instead of broadcasting it
    /// </summary>
    //[CUISerializable]
    public ObservableCollection<string> Emits
    {
      get => emits;
      set
      {
        emits = value;
        emits.CollectionChanged += (o, e) => UpdateDataTargets();
        UpdateDataTargets();
      }
    }
    private ObservableCollection<string> emits = new();

    private void UpdateDataTargets()
    {
      if (Emits.Count > 0)
      {
        DataTargets.Clear();

        RunRecursiveOn(this, (c) =>
        {
          if (Emits.Contains(c.Consumes))
          {
            if (!DataTargets.ContainsKey(c.Consumes)) DataTargets[c.Consumes] = new();
            DataTargets[c.Consumes].Add(c);
          }
        });
      }
    }

    /// <summary>
    /// Consumers of emmited data, updates on tree change
    /// </summary>
    public Dictionary<string, List<CUIComponent>> DataTargets = new();


    /// <summary>
    /// All commands
    /// </summary>
    public Dictionary<string, Action<object>> Commands { get; set; } = new();

    /// <summary>
    /// Manually adds command
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddCommand(string name, Action<object> action) => Commands.Add(name, action);
    public void RemoveCommand(string name) => Commands.Remove(name);

    /// <summary>
    /// Executed autpmatically on component creation
    /// Methods ending in "Command" will be added as commands
    /// </summary>
    private void AddCommands()
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
            AddCommand(name, mi.CreateDelegate<Action<object>>(this));
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
    public void DispatchUp(CUICommand command)
    {
      if (OnAnyCommand != null) OnAnyCommand?.Invoke(command);
      else if (Commands.ContainsKey(command.Name)) Execute(command);
      else Parent?.DispatchUp(command);
    }

    /// <summary>
    /// Dispathes command down the component tree until someone consumes it
    /// </summary>
    public void DispatchDown(CUIData data)
    {
      if (Emits.Contains(data.Name))
      {
        if (DataTargets.ContainsKey(data.Name))
        {
          foreach (CUIComponent target in DataTargets[data.Name])
          {
            target.OnConsume?.Invoke(data.Data);
          }
        }
      }
      else
      {
        if (Consumes == data.Name) OnConsume?.Invoke(data.Data);
        else if (OnAnyData != null) OnAnyData.Invoke(data);
        else
        {
          foreach (CUIComponent child in Children) child.DispatchDown(data);
        }
      }
    }

    /// <summary>
    /// Will execute action corresponding to this command
    /// </summary>
    /// <param name="commandName"></param>
    public void Execute(CUICommand command)
    {
      Commands.GetValueOrDefault(command.Name)?.Invoke(command.Data);
    }
  }
}