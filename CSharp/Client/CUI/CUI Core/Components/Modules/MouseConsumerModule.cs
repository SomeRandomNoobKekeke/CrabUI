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
  public class MouseConsumerModule : IModule
  {
    public IComponentTreeNode Host { get; }

    public void Consume(MouseEventProbe probe)
    {

    }

    public MouseConsumerModule(IComponentTreeNode host) => Host = host;
  }
}