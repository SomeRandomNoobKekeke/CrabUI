using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;

namespace CursedUI
{
  public record RoutableCommandContract()
  {
    private ImmutableHashSet<string> ConsumesSet = [];
    public IEnumerable<string> Consumes { set => ConsumesSet = value.ToImmutableHashSet(); }

    private ImmutableHashSet<string> EmitsUpSet = [];
    public IEnumerable<string> EmitsUp { set => EmitsUpSet = value.ToImmutableHashSet(); }

    private ImmutableHashSet<string> EmitsDownSet = [];
    public IEnumerable<string> EmitsDown { set => EmitsDownSet = value.ToImmutableHashSet(); }

    public bool CanConsume(string name) => ConsumesSet.Contains(name);
    public bool CanEmitUp(string name) => EmitsUpSet.Contains(name);
    public bool CanEmitDown(string name) => EmitsDownSet.Contains(name);
  }
}