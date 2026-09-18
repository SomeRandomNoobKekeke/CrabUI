using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using CUILibs;
using System.Collections;
namespace CursedUI
{
  public class CUIMultiSprite : CUISprite, IEnumerable<ITextureSource>
  {
    public List<ITextureSource> _Sources = new(); public List<ITextureSource> Sources
    {
      get => _Sources;
      set
      {
        _Sources = value;
        CurrentSource = 0;
        SyncTextureSource();
      }
    }

    public void Add(ITextureSource source)
    {
      Sources.Add(source);

      if (CurrentSource == Sources.Count - 1) SyncTextureSource();
    }

    public int _CurrentSource; public int CurrentSource
    {
      get => _CurrentSource;
      set
      {
        _CurrentSource = value;
        SyncTextureSource();
      }
    }

    private void SyncTextureSource()
    {
      int i = CurrentSource % Sources.Count;

      Texture = Sources[i].Texture;
      SourceRectangle = Sources[i].SourceRectangle;
    }

    public IEnumerator<ITextureSource> GetEnumerator() => Sources.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Sources.GetEnumerator();

    public CUIMultiSprite() : base() { }
    public CUIMultiSprite(List<ITextureSource> sources) : base() => Sources = sources;
  }
}