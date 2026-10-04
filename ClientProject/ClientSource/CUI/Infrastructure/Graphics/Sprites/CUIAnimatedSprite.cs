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
  public class CUIAnimatedSprite : CUIMultiSprite
  {
    public double MsPerFrame
    {
      get => SecPerFrame * 1000.0;
      set => SecPerFrame = value / 1000.0;
    }

    public double SecPerFrame
    {
      get => Animation.Duration / Sources.Count;
      set => Animation.Duration = Sources.Count * value;
    }

    private TypedAnimation<int> _Animation; public TypedAnimation<int> Animation
    {
      get => _Animation;
      set
      {
        if (_Animation is not null) _Animation.Changed -= UpdateSource;
        _Animation = value;
        if (_Animation is not null) _Animation.Changed += UpdateSource;
      }
    }

    private void UpdateSource(int i) => CurrentSource = i;


    public CUIAnimatedSprite() : base()
    {
      Animation = new();
    }

    public CUIAnimatedSprite(
      List<ITextureSource> sources,
      TypedAnimation<int> animation
    ) : base()
    {
      Sources = sources;
      Animation = animation;
    }

    public CUIAnimatedSprite(
      CUITexture2D texture,
      Point? frames,
      Point? frameSize,
      Point? gap = null,
      Point? start = null
    ) : base()
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

      Sources = sources;

      Animation = new()
      {
        StartValue = 0,
        EndValue = Sources.Count - 1,
        OnEnd = ActionOnTrackEnd.Repeat,
      };
    }

  }
}