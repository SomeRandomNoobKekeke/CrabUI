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
  //LINK:/ClientProject\InMemory\CUITest\Snapshots\Tests\Random\AnimatedSprite.cs
  public partial class CUIAnimatedSprite : CUIMultiSprite
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

    public bool IsRunnung
    {
      get => Animation.IsRunning;
      set => Animation.IsRunning = value;
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


    public override List<ITextureSource> Sources
    {
      get => _Sources;
      set
      {
        base.Sources = value;

        if (Animation is not null) Animation.EndValue = Sources.Count - 1;
      }
    }

    public override void Add(ITextureSource source)
    {
      base.Add(source);
      if (Animation is not null) Animation.EndValue = Sources.Count - 1;
    }


    public CUIAnimatedSprite() : base()
    {
      Animation = new()
      {
        StartValue = 0,
        OnEnd = ActionOnTrackEnd.Repeat,
      };

      Animation.RunForward();
    }

    public CUIAnimatedSprite(
      CUITexture2D texture,
      Point? frames,
      Point? frameSize,
      Point? gap = null,
      Point? start = null
    ) : this()
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
    }

  }
}