using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CursedUI
{
  public partial class CUIComponent
  {
    [InitMethod]
    protected virtual void InitLayout()
    {
      Layout = new CUIPlainLayout();
      Layout.ConnectTo(new Adapters_Part.CUIPlainLayout_Host_Part() { Self = this });
    }

    public override Layout? Layout { get; protected set; }
  }
}