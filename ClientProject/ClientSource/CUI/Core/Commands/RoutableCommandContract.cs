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

    private ImmutableHashSet<string> SendsUpSet = [];
    public IEnumerable<string> SendsUp { set => SendsUpSet = value.ToImmutableHashSet(); }

    private ImmutableHashSet<string> SendsDownSet = [];
    public IEnumerable<string> SendsDown { set => SendsDownSet = value.ToImmutableHashSet(); }

    public bool CanConsume(string name) => ConsumesSet.Contains(name);
    public bool CanSendUp(string name) => SendsUpSet.Contains(name);
    public bool CanSendDown(string name) => SendsDownSet.Contains(name);

    public override string ToString() => $"Consumes: {Logger.Wrap.IEnumerable(ConsumesSet)} SendsUp: {Logger.Wrap.IEnumerable(SendsUpSet)} SendsDown: {Logger.Wrap.IEnumerable(SendsDownSet)}";
  }
}