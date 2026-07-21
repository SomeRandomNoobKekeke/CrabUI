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
  public interface CUITextureManager
  {
    CUITexture2D Add(CUITexture2D texture, string key);
    void Clear();
    void Dispose();
    void Forget(string key);
    bool Has(string key);
    CUITexture2D Get(string key);
    public CUITexture2D Reload(string key);
    public CUITexture2D LoadAs(string path, string key);
  }
}