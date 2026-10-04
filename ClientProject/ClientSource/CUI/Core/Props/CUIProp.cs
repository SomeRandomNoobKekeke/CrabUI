using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CursedUI
{
  public class CUIProp<T>
  {
    public object HostComponent { get; set; }
    public string HostPropName { get; set; }

    protected T _value;
    public virtual T Value
    {
      get => _value;
      set
      {
        _value = value;

        // debug nodes are a bit too heavy for this
        // if ((HostComponent as CUIVisualComponent)?.Debug == true)
        // {
        //   CUI.Logger.Log($"{HostComponent}.{HostPropName} = [{value}]");
        // }
      }
    }

    public T DefaultValue { set { _value = value; } }
  }
}