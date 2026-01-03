using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace bruh
{
  public interface IVisualComponent : IStructuralComponent
  {
    public ReadOnlyCollection<IVisualComponent> Children { get; }
    public IVisualComponent Parent { get; }

    public void Draw();
    public void HandleMouse();
  }
}