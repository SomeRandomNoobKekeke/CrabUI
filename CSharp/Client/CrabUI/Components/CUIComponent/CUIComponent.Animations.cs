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
using HarmonyLib;

namespace CrabUI
{
  public partial class CUIComponent : IDisposable
  {
    private void SetupAnimations()
    {
      Animations = new Indexer<string, CUIAnimation>(
        (key) => animations.GetValueOrDefault(key),
        (key, value) => AddAnimation(key, value)
      );
    }
    private Dictionary<string, CUIAnimation> animations = new();
    public Indexer<string, CUIAnimation> Animations;
    public void AddAnimation(string name, CUIAnimation animation)
    {
      animation.Target = this;
      animations[name] = animation;
    }

    public void BlockChildrenAnimations()
    {
      foreach (CUIComponent child in Children)
      {
        foreach (CUIAnimation animation in child.animations.Values)
        {
          animation.Stop();
          animation.Blocked = true;
        }
        child.BlockChildrenAnimations();
      }
    }
  }
}