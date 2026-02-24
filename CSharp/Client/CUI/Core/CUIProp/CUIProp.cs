using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public abstract class CUIProp
  {
    protected object host;
    /// <summary>
    /// Object that contains the prop
    /// </summary>
    public virtual object Host
    {
      get => host;
      set => host = value;
    }

    /// <summary>
    /// Prop name in containing object
    /// </summary>
    public string Name { get; set; }
  }


  public class CUIProp<T> : CUIProp
  {
    public static InfoChannel<object, string, T> OnSet = new()
    {
      // MapList = new List<Action<object, string, T>>(){
      //   (host, name, value) => CUI.InfoChannels.CUIPropSet.Send(host, name, value)
      // },
    };

    protected T _value;
    public virtual T Value
    {
      get => _value;
      set
      {
        _value = value;
        // OnSet.Send(Host, Name, value);
      }
    }
  }
}