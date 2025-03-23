using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System.Xml;
using System.Xml.Linq;

namespace CrabUI
{
  /// <summary>
  /// Props implementing this will be bound to their host 
  /// with reflection after creation of the host
  /// </summary>
  public interface ICUIVitalizable
  {
    public void SetHost(CUIComponent host);
  }

  public interface ICUIProp : ICUIVitalizable
  {
    public void SetName(string name);
  }
}