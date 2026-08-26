using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;
using Barotrauma;
using Microsoft.Xna.Framework;
using CUICodeGenerator;

namespace CursedUI
{

  public partial class SoloCUIRunner
  {
    public __CUITextureManager TextureManager { get; } = new();
    CUITextureManager ICUIRunner.TextureManager => CUITextureManagerPublic;
    public CUITextureManager_PublicPart CUITextureManagerPublic { get; } = new();

    private void LoadDefaultTextures()
    {
      CUITextureManagerPublic.LoadAs("Assets/PNG/dev.png", "BaroDev");
      CUITextureManagerPublic.LoadAs("Assets/PNG/CUI.png", "CUI");
    }
  }
}