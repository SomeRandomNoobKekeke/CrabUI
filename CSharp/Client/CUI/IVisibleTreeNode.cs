using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;

namespace CrabUI
{
  public interface IVisibleTreeNode
  {
    public List<IVisibleTreeNode> TopChildren { get; }
    public List<IVisibleTreeNode> Children { get; }


    public IEnumerable<IVisibleTreeNode> Crawl() => _Crawl(this);
    private IEnumerable<IVisibleTreeNode> _Crawl(IVisibleTreeNode node)
    {
      foreach (IVisibleTreeNode child in node.TopChildren)
      {
        foreach (IVisibleTreeNode n in _Crawl(child))
        {
          yield return n;
        }
      }

      yield return node;

      foreach (IVisibleTreeNode child in node.Children)
      {
        foreach (IVisibleTreeNode n in _Crawl(child))
        {
          yield return n;
        }
      }
    }
  }

  public static class IVisibleTreeNodeExtensions
  {
    public static IEnumerable<IVisibleTreeNode> Crawl(this IVisibleTreeNode node) => node.Crawl();
  }
}