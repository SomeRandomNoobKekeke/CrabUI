using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
namespace CrabUI
{

  public class InfoChannelRouter : InfoChannelBase
  {
    public void Route(InfoChannelBase channel) => channel.AddCallback(Send);
    public void UnRoute(InfoChannelBase channel) => channel.RemoveCallback(Send);
  }

  public class InfoChannelRouter<T1> : InfoChannelBase<T1>
  {
    public void Route(InfoChannelBase<T1> channel) => channel.AddCallback(Send);
    public void UnRoute(InfoChannelBase<T1> channel) => channel.RemoveCallback(Send);
  }

  public class InfoChannelRouter<T1, T2> : InfoChannelBase<T1, T2>
  {
    public void Route(InfoChannelBase<T1, T2> channel) => channel.AddCallback(Send);
    public void UnRoute(InfoChannelBase<T1, T2> channel) => channel.RemoveCallback(Send);
  }

  public class InfoChannelRouter<T1, T2, T3> : InfoChannelBase<T1, T2, T3>
  {
    public void Route(InfoChannelBase<T1, T2, T3> channel) => channel.AddCallback(Send);
    public void UnRoute(InfoChannelBase<T1, T2, T3> channel) => channel.RemoveCallback(Send);
  }

  public class InfoChannelRouter<T1, T2, T3, T4> : InfoChannelBase<T1, T2, T3, T4>
  {
    public void Route(InfoChannelBase<T1, T2, T3, T4> channel) => channel.AddCallback(Send);
    public void UnRoute(InfoChannelBase<T1, T2, T3, T4> channel) => channel.RemoveCallback(Send);
  }

  public class InfoChannelRouter<T1, T2, T3, T4, T5> : InfoChannelBase<T1, T2, T3, T4, T5>
  {
    public void Route(InfoChannelBase<T1, T2, T3, T4, T5> channel) => channel.AddCallback(Send);
    public void UnRoute(InfoChannelBase<T1, T2, T3, T4, T5> channel) => channel.RemoveCallback(Send);
  }
}