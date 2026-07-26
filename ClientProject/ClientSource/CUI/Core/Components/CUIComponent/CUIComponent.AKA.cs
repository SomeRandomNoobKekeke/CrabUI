using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;
using CUILibs;
namespace CrabUI
{
  public partial class CUIComponent
  {

    /// <summary>
    /// You can access NamedComponents with this indexer
    /// </summary>
    public new CUIComponent this[string name]
    {
      get => Get(name) as CUIComponent;
      set => base[name] = value;
    }
  }
}