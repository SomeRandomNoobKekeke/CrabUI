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
  /// For stuff that should be in some way refreshed  
  /// This method appears way too many times  
  /// There are also secret public void CascadeRefresh() method in CUIComponent.Events that refreshes all childs recursivelly
  /// </summary>
  public interface IRefreshable
  {
    public void Refresh();
  }
}