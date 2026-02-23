using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;


namespace CrabUI
{
  public delegate void CUIEventHandler();
  public delegate void CUIEventHandler<T1>(T1 arg1);
  public delegate void CUIEventHandler<T1, T2>(T1 arg1, T2 arg2);
}