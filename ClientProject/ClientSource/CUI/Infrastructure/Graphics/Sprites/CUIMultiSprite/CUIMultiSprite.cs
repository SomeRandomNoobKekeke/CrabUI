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
    public static CUIMultiSprite FromSpriteAtlas(
      CUITexture2D texture,
      Point? frames,
      Point? frameSize,
      Point? gap = null,
      Point? start = null
    )
    {
      Point _frames = frames ?? new Point(1, 1);
      Point _frameSize = frameSize ?? texture.Bounds.Size;
      Point _gap = gap ?? new Point(0, 0);
      Point _start = start ?? new Point(0, 0);

      List<ITextureSource> sources = new List<ITextureSource>();

      for (int j = 0; j < _frames.Y; j++)
      {
        for (int i = 0; i < _frames.X; i++)
        {
          sources.Add(new TextureSource(
            texture,
            new Rectangle(
              _start.X + i * (_frameSize.X + _gap.X),
              _start.Y + j * (_frameSize.Y + _gap.Y),
              _frameSize.X,
              _frameSize.Y
            )
          ));
        }
      }

      return new CUIMultiSprite(sources);
    }

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
      if (Sources.Count == 0)
      {
        Texture = CUITexture2D.White;
        SourceRectangle = null;
        return;
      }

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