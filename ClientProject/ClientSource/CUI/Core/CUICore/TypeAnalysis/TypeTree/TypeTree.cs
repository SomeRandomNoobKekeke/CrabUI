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
  public partial class TypeTree
  {
    public Dictionary<Type, Node> Nodes { get; } = new();
    public Dictionary<string, Type> TypesByName { get; } = new();

    /// <summary>
    /// Slower, but neccessary if you need to add parents after children
    /// </summary>
    public bool AllowConnectingInReverseOrder { get; set; } = true;


    public void Add<T>() => Add(typeof(T));
    public void Add(Type T)
    {
      if (Nodes.ContainsKey(T)) return;
      if (AllowConnectingInReverseOrder) AddBothSides(T); else AddOneSide(T);
    }

    private void AddOneSide(Type T)
    {
      Node node = Nodes[T] = new Node(T);
      TypesByName[Reflection_Extensions.GetFullName(T)] = T;

      if (T.BaseType is not null)
      {
        if (Nodes.TryGetValue(T.BaseType, out Node parentNode))
        {
          parentNode.Children.Add(node);
          node.Parent = parentNode;
        }
      }
    }
    private void AddBothSides(Type T)
    {
      Node node = Nodes[T] = new Node(T);
      TypesByName[Reflection_Extensions.GetFullName(T)] = T;

      if (T.BaseType is not null)
      {
        if (Nodes.TryGetValue(T.BaseType, out Node parentNode))
        {
          parentNode.Children.Add(node);
          node.Parent = parentNode;
        }
      }

      foreach (Node child in Nodes.Values)
      {
        if (child.Type.BaseType == T)
        {
          node.Children.Add(child);
          child.Parent = node;
        }
      }
    }

    public void Add(IEnumerable<Type> types)
    {
      if (AllowConnectingInReverseOrder)
      {
        AddBothSides(types.Except(Nodes.Keys).ToArray());
      }
      else
      {
        AddOneSide(types.Except(Nodes.Keys).ToArray());
      }
    }

    private void AddOneSide(IEnumerable<Type> types)
    {
      foreach (Type T in types)
      {
        Nodes[T] = new Node(T);
        TypesByName[Reflection_Extensions.GetFullName(T)] = T;
      }

      foreach (Type T in types)
      {
        if (T.BaseType is not null)
        {
          if (Nodes.TryGetValue(T.BaseType, out Node parentNode))
          {
            parentNode.Children.Add(Nodes[T]);
            Nodes[T].Parent = parentNode;
          }
        }
      }
    }

    private void AddBothSides(IEnumerable<Type> types)
    {
      foreach (Type T in types)
      {
        Nodes[T] = new Node(T);
        TypesByName[Reflection_Extensions.GetFullName(T)] = T;
      }

      foreach (Node node in Nodes.Values)
      {
        node.Children.Clear();
        node.Parent = null;
      }

      foreach (Node node in Nodes.Values)
      {
        if (node.Type.BaseType is not null)
        {
          if (Nodes.TryGetValue(node.Type.BaseType, out Node parentNode))
          {
            parentNode.Children.Add(node);
            node.Parent = parentNode;
          }
        }
      }
    }

    public ICollection<Type> AllTypes => Nodes.Keys;

    public IEnumerable<Type> GetDerivedTypes<T>(bool includeSelf = true) => GetDerivedTypes(typeof(T), includeSelf);
    public IEnumerable<Type> GetDerivedTypes(Type T, bool includeSelf = true)
    {
      if (includeSelf) return _GetDerivedTypes(T);
      return _GetDerivedTypesNoSelf(T);
    }

    private IEnumerable<Type> _GetDerivedTypesNoSelf(Type T)
    {
      if (Nodes.ContainsKey(T))
      {
        foreach (Node child in Nodes[T].Children)
        {
          foreach (Type derived in _GetDerivedTypes(child.Type))
          {
            yield return derived;
          }
        }
      }
    }

    private IEnumerable<Type> _GetDerivedTypes(Type T)
    {
      yield return T;

      if (Nodes.ContainsKey(T))
      {
        foreach (Node child in Nodes[T].Children)
        {
          foreach (Type derived in _GetDerivedTypes(child.Type))
          {
            yield return derived;
          }
        }
      }
    }

    public IEnumerable<Type> GetBaseTypes<T>(bool includeSelf = true) => GetBaseTypes(typeof(T), includeSelf);
    public IEnumerable<Type> GetBaseTypes(Type T, bool includeSelf = true)
    {
      if (includeSelf) return _GetBaseTypes(T);

      if (T.BaseType != null && T.BaseType != typeof(object))
      {
        return _GetBaseTypes(T.BaseType);
      }

      return [];
    }
    private IEnumerable<Type> _GetBaseTypes(Type T)
    {
      yield return T;

      while (T.BaseType != null && T.BaseType != typeof(object))
      {
        T = T.BaseType;
        yield return T;
      }
    }


    public void Clear() => Nodes.Clear();

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


    public void Reconnect(Type T)
    {
      Node node = Nodes[T];

      if (T.BaseType is not null)
      {
        if (Nodes.TryGetValue(T.BaseType, out Node parentNode))
        {
          parentNode.Children.Add(node);
          node.Parent = parentNode;
        }
      }

      foreach (Node child in Nodes.Values)
      {
        if (child.Type.BaseType == T)
        {
          node.Children.Add(child);
          child.Parent = node;
        }
      }
    }

    public void FullReconnect()
    {
      foreach (Node node in Nodes.Values)
      {
        if (node.Type.BaseType is not null)
        {
          if (Nodes.TryGetValue(node.Type.BaseType, out Node parentNode))
          {
            parentNode.Children.Add(node);
            node.Parent = parentNode;
          }
        }
      }
    }
  }
}