using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CUILibs;

namespace CursedUI
{
  /// <summary>
  /// It's a frame but only 1 instance can be opened at a time  
  /// How to use:  
  /// - Create derived class  
  /// - Add buttons that emit some commands  
  /// - Add command listeners from outside  
  /// e.g. you can listen for close command 
  /// </summary>
  public abstract class CUIDialog : CUIDefault.Frame
  {
    private static ConditionalWeakTable<Type, CUIDialog> OpenedDialogs { get; } = new();
    private static bool AlreadyOpened(Type T)
    {
      OpenedDialogs.TryGetValue(T, out CUIDialog dialog);
      return dialog is not null;
    }

    public override void Open(CUIComponent Host = null)
    {
      if (SingleInstance && AlreadyOpened(this.GetType())) return;
      OpenedDialogs.Add(this.GetType(), this);
      base.Open(Host);
    }

    public override void Close()
    {
      OpenedDialogs.Remove(this.GetType());
      base.Close();
    }


    protected virtual bool SingleInstance => true;

    public CUIDialog(string caption, float width, float height) : base(caption, width, height)
    {

    }
  }

}