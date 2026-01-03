using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace bruh
{

  public interface IVisualComponent : IVisualElement
  {
    public VisualComponentContext Context { get; }

    public IEnumerable<IVisualElement> Elements();
  }
}
