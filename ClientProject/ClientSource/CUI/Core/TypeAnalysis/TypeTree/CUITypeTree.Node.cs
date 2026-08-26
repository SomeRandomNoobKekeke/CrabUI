using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;

namespace CursedUI
{
  public partial class CUITypeTree
  {
    public record Node(Type Type)
    {
      public Node Parent { get; set; }
      public List<Node> Children { get; } = new();
    }
  }
}