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

  public class VisualDrawInstruction : VisualInstruction
  {
    public IVisualElement Element;

    public VisualDrawInstruction(IVisualElement element) => Element = element;
  }
}
