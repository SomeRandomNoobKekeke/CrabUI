using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace CrabUI
{
  public interface CUITextureManagerInternal : CUITextureManager
  {
    public string LoadedFileDir { set; }
    public Assembly CallingAssembly { set; }
  }
}