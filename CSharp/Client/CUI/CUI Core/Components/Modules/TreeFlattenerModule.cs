using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace CrabUI
{
  public class TreeFlattenerModule : IModule
  {
    public IComponentTreeNode Host { get; }

    //TODO
    // public bool ExcludeSelf { get; set; } = true;

    public List<IComponentTreeNode> Flat = new();

    public void Flatten()
    {
      Flat.Clear();

      void FlattenRec(IComponentTreeNode component)
      {
        foreach (var topChild in component.TreeNodeModule.TopChildren)
        {
          FlattenRec(topChild);
        }

        foreach (var child in component.TreeNodeModule.Children)
        {
          FlattenRec(child);
        }

        Flat.Add(component);
      }

      FlattenRec(Host);
    }

    public TreeFlattenerModule(IComponentTreeNode host) => Host = host;
  }
}