using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;
using System.Text;
namespace CursedUI
{
  public partial class CUITypeTree
  {
    public Dictionary<Type, Node> Nodes { get; } = new();
    public Dictionary<string, Type> TypesByName { get; } = new();

    public void Add(IEnumerable<Type> types)
    {
      foreach (Type T in types)
      {
        Nodes[T] = new Node(T);
        TypesByName[T.GetFullName()] = T;
      }

      //TODO optimize, i just need to check the new types
      Connect();
    }

    public ICollection<Type> AllTypes => Nodes.Keys;

    public IEnumerable<Type> GetDerivedTypes(Type T)
    {
      yield return T;

      if (Nodes.ContainsKey(T))
      {
        foreach (Node child in Nodes[T].Children)
        {
          foreach (Type derived in GetDerivedTypes(child.Type))
          {
            yield return derived;
          }
        }
      }
    }

    public void Connect()
    {
      foreach (var (type, node) in Nodes)
      {
        if (
          node.Parent is null &&
          node.Type.BaseType != null &&
          Nodes.ContainsKey(node.Type.BaseType)
        )
        {
          Nodes[node.Type.BaseType].Children.Add(node);
          node.Parent = Nodes[node.Type.BaseType];
        }
      }
    }

    public void Clear() => Nodes.Clear();

    // public void PrintTree(string offset = "")
    // {
    //   CUI.Logger.Log($"{offset}{this}");
    //   foreach (CUIVisualComponent child in Children)
    //   {
    //     child.PrintTree(offset + "|    ");
    //   }
    // }

    public override string ToString()
    {
      StringBuilder sb = new();

      void NodeToText(Node node, string offset)
      {
        sb.Append($"{offset}{node.Type.Name}\n");

        foreach (Node child in node.Children)
        {
          NodeToText(child, offset + "|      ");
        }
      }

      NodeToText(Nodes[typeof(CUIVisualComponent)], "");

      return sb.ToString();
    }
  }
}