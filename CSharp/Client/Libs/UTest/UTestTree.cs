using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text;

namespace BaroJunk
{
  public class UTestTreeNode
  {
    public Type Type;
    public UTestTreeNode Parent;
    public List<UTestTreeNode> Children = new();
    public bool IsLeave => Children.Count == 0;
    public void AddChild(UTestTreeNode child)
    {
      child.Parent = this;
      Children.Add(child);
    }

    //TODO add protection from loops
    public IEnumerable<UTestTreeNode> DeepChildren
    {
      get
      {
        foreach (UTestTreeNode child in Children)
        {
          yield return child;
          foreach (UTestTreeNode deepChild in child.DeepChildren)
          {
            yield return deepChild;
          }
        }
      }
    }

    public string StringPath => String.Join('.', Path.Select(node => node.Type.Name));
    public List<Type> TypePath => Path.Select(node => node.Type).ToList();
    //TODO add protection from loops
    public List<UTestTreeNode> Path
    {
      get
      {
        List<UTestTreeNode> path = new();

        UTestTreeNode node = this;
        path.Add(node);

        while (node.Parent is not null)
        {
          node = node.Parent;
          path.Add(node);
        }

        path.Reverse();

        return path;
      }
    }



    public UTestTreeNode(Type T) => this.Type = T;

    public override string ToString() => StringPath;
  }

  public class UTestTree
  {
    public List<UTestTreeNode> Roots = new();
    public UTestTreeNode RootNode => Roots.Count == 1 ? Roots.First() : null;
    public Type RootType => RootNode?.Type;
    public Dictionary<Type, UTestTreeNode> Nodes = new();
    public Dictionary<string, Type> TypeByPath = new();

    private void _RunRecursive(Action<Type> action, UTestTreeNode startNode)
    {
      action(startNode.Type);
      foreach (UTestTreeNode child in startNode.DeepChildren)
      {
        action(child.Type);
      }
    }

    public void RunRecursive(Action<Type> action, Type start = null)
    {
      if (start is not null)
      {
        _RunRecursive(action, Nodes[start]);
      }
      else
      {
        foreach (UTestTreeNode node in Roots)
        {
          _RunRecursive(action, node);
        }
      }
    }

    private void ScanAssembly(Assembly assembly)
    {
      Nodes.Clear();
      Roots.Clear();

      foreach (Type T in assembly.GetTypes())
      {
        if (T.IsAbstract) continue;
        if (T == typeof(UTestPack)) continue;

        if (T.IsAssignableTo(typeof(UTestPack)))
        {
          UTestCategory meta = (UTestCategory)Attribute.GetCustomAttribute(T, typeof(UTestCategory));
          if (meta?.Category != null && !UTestExplorer.Categories.Contains(meta.Category)) continue;

          Nodes[T] = new UTestTreeNode(T);
        }
      }

      foreach (UTestTreeNode node in Nodes.Values)
      {
        Type parentType = node.Type.BaseType;

        UTestSubPackOf meta = (UTestSubPackOf)Attribute.GetCustomAttribute(node.Type, typeof(UTestSubPackOf));
        if (meta?.SubPackOf is not null)
        {
          parentType = meta?.SubPackOf;
        }

        if (parentType is null) continue;

        if (Nodes.ContainsKey(parentType))
        {
          Nodes[parentType].AddChild(node);
        }
      }

      foreach (UTestTreeNode node in Nodes.Values)
      {
        TypeByPath[node.StringPath] = node.Type;

        if (node.Parent is null) Roots.Add(node);
      }
    }


    public UTestTree() => ScanAssembly(Assembly.GetExecutingAssembly());

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();

      void ToStringReq(UTestTreeNode node, string offset)
      {
        sb.Append($"{offset}{node.Type.Name}\n");
        foreach (UTestTreeNode child in node.Children)
        {
          ToStringReq(child, offset + "|        ");
        }
      }

      try
      {
        foreach (UTestTreeNode node in Roots)
        {
          ToStringReq(node, "");
          sb.Append("\n");
        }
      }
      catch (Exception e) { sb.Append($" >> [{e.Message}]"); }
      return sb.ToString();
    }
  }


}
