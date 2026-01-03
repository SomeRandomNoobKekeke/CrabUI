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

  public class Body : IVisualElement
  {

  }

  public class Component : IVisualComponent
  {

    public Body Body = new();

    public List<Component> Children = new();
    public Component Parent;

    public List<Component> TopChildren = new();

    public VisualComponentContext Context { get; set; } = new();



    public IEnumerable<IVisualElement> Elements()
    {
      yield return Body;
      foreach (Component child in Children)
      {
        yield return child;
      }
      foreach (Component child in TopChildren)
      {
        yield return child;
      }
    }
  }
}
