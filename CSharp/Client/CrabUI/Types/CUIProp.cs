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
  /// <summary>
  /// Wrapper object for prop value, setters, side effects, metadata etc.
  /// </summary>
  /// <typeparam name="T"> Type of the prop </typeparam>
  public class CUIProp<T> : ICUIProp
  {
    public void SetHost(CUIComponent host) => Host = host;
    public void SetName(string name) => Name = name;

    public CUIComponent Host;
    public string Name = "Unknown";
    public Action<T, CUIComponent> OnSet;
    public Func<T, CUIComponent, T> Validate;
    public bool LayoutProp;
    public bool DecorProp;
    public bool AbsoluteProp;
    public bool ChildProp;
    public bool ShowInDebug = true;

    public T Value;
    public void SetValue(T value, [CallerMemberName] string memberName = "")
    {
      if (Host == null)
      {
        CUI.Log($"{Name} CUIProp doens't have a Host! Type: {typeof(T)} Value: {value} memberName: {memberName}", Color.Red);
        return;
      }

      if (Validate == null)
      {
        Value = value;
      }
      else
      {
        Value = Validate.Invoke(value, Host);
      }

      OnSet?.Invoke(value, Host);

      if (ShowInDebug)
      {
        CUIDebug.Capture(null, Host, "SetValue", memberName, Name, Value.ToString());
      }


      if (LayoutProp) Host.OnPropChanged();
      if (DecorProp) Host.OnDecorPropChanged();
      if (AbsoluteProp) Host.OnAbsolutePropChanged();
      if (ChildProp) Host.OnChildrenPropChanged();
    }

    public CUIProp() { }

    public CUIProp(CUIComponent host, string name)
    {
      Name = name;
      Host = host;
    }

  }
}