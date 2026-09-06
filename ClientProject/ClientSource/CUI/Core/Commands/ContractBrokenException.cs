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

  public class ContractBrokenException : System.Exception
  {
    public ContractBrokenException() { }
    public ContractBrokenException(string message) : base(message) { }
    public ContractBrokenException(string message, System.Exception inner) : base(message, inner) { }
    protected ContractBrokenException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}