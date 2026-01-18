using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace CrabUI
{

  public interface ITreeNode
  {
    public object Parent { get; }
    public IList Children { get; }
  }
}