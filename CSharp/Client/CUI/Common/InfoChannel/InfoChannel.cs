using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
namespace CrabUI
{

  public class InfoChannel : InfoChannelBase
  {
    new public void Send() => base.Send();
  }

  public class InfoChannel<T1> : InfoChannelBase<T1>
  {
    public void Send(T1 arg1) => base.Send(arg1);
  }

  public class InfoChannel<T1, T2> : InfoChannelBase<T1, T2>
  {
    public void Send(T1 arg1, T2 arg2) => base.Send(arg1, arg2);
  }

  public class InfoChannel<T1, T2, T3> : InfoChannelBase<T1, T2, T3>
  {
    public void Send(T1 arg1, T2 arg2, T3 arg3) => base.Send(arg1, arg2, arg3);
  }

  public class InfoChannel<T1, T2, T3, T4> : InfoChannelBase<T1, T2, T3, T4>
  {
    public void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => base.Send(arg1, arg2, arg3, arg4);
  }

  public class InfoChannel<T1, T2, T3, T4, T5> : InfoChannelBase<T1, T2, T3, T4, T5>
  {
    public void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => base.Send(arg1, arg2, arg3, arg4, arg5);
  }
}