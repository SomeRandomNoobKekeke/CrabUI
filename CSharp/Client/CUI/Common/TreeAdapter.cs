using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CrabUI
{

  public class TreeAdapter<T> where T : class
  {
    private ITreeNode Node;

    public T Self => Node as T;
    public T Parent => Node.Parent as T;
    public ListProxy<T> Children { get; }

    public TreeAdapter(ITreeNode node)
    {
      Node = node;
      Children = new ListProxy<T>(node.Children);
    }

    public override string ToString() => $"TreeAdapter over [{Node}]";
  }
}