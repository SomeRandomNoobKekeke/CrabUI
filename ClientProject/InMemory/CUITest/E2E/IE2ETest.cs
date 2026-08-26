using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CursedUI;
using Microsoft.Xna.Framework;
using System.Text;
using System.IO;

namespace CursedUIUser
{
  public interface IE2ETest
  {
    public void Initialize();

    public void Dispose();
  }
}