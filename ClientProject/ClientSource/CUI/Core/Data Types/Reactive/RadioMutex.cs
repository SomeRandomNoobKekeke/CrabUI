using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using CUILibs;

namespace CursedUI
{
  public class RadioGroup
  {
    static RadioGroup()
    {
      PluginLifeCycle.Stop += () => Groups.Clear();
    }

    public static Dictionary<string, RadioGroup> Groups { get; } = new();

    public static RadioGroup GetOrCreate(string name)
    {
      if (!Groups.ContainsKey(name)) Groups[name] = new();
      return Groups[name];
    }


    public int MaxID { get; private set; }
    public int Previous { get; private set; } = -1;
    public int Current { get; private set; } = -1;

    public event Action<int> Selected;

    public int GetNewId() => MaxID++;
    public void Select(int id)
    {
      Previous = Current;
      Current = id;
      Selected?.Invoke(id);
    }
    public bool IsSelected(int id) => Current == id;
    public bool WasSelected(int id) => Previous == id;
  }

  public class RadioMutex
  {
    public bool IsSelected => Group.IsSelected(ID);
    public bool WasSelected => Group.WasSelected(ID);
    public void Select() => Group.Select(ID);

    public event Action Selected;
    public event Action Deselected;

    public string Name { get; }
    private RadioGroup Group;
    private int ID;

    public RadioMutex(string name)
    {
      Name = name;
      Group = RadioGroup.GetOrCreate(name);
      ID = Group.GetNewId();

      Group.Selected += (id) =>
      {
        if (id == ID)
        {
          if (!Group.WasSelected(ID) && Group.IsSelected(ID)) Selected?.Invoke();
        }
        else
        {
          if (Group.WasSelected(ID) && !Group.IsSelected(ID)) Deselected?.Invoke();
        }
      };
    }
  }
}