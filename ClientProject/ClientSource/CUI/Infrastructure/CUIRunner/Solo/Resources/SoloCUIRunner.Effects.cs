using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;


using System.IO;
using System.Xml;
using System.Xml.Linq;
using EventInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace CursedUI
{


  public partial class SoloCUIRunner
  {
    public class OtherResources : CUICore.OtherCUICoreResources
    {
      public CUICore.IEffectPack Effects { get; } = new LazyLoadedEffects();
    }

    public OtherResources _OtherResources { get; } = new();
  }

  public class LazyLoadedEffects : CUICore.IEffectPack
  {
    public Effect DamageEffect => GameMain.GameScreen.DamageEffect;

    private BlurEffect _BlurEffect; public BlurEffect BlurEffect
    {
      get => _BlurEffect ??= new BlurEffect(EffectLoader.Load("Effects/blurshader"), 0, 0);
    }

  }
}