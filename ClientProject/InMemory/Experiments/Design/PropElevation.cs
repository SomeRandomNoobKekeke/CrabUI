using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CrabUIUser
{

  /// <summary>
  /// Imagine you have a component with a sprite
  /// And component is mapping some props of sprite
  /// Can i elevate props from sprite to component making it the source of truth?
  /// And how will it scale?
  /// I feel there should be cleaner solution to this
  /// </summary>
  public class PropElevation : Experiment
  {

    #region Before
    public class Sprite
    {
      // This is the source of truth
      public Color Color { get; set; }
      public float Rotation { get; set; }
    }

    public class Component1
    {
      public Sprite Sprite { get; set; }

      // It's only mapping to Sprite Color
      public Color Color
      {
        get => Sprite.Color;
        set => Sprite.Color = value;
      }

      public float Rotation
      {
        get => Sprite.Rotation;
        set => Sprite.Rotation = value;
      }
    }

    #endregion

    #region After
    public class Component2
    {
      private Sprite _Sprite; public Sprite Sprite
      {
        get => _Sprite;
        set
        {
          _Sprite = value;
          _Sprite.Color = Color;
          _Sprite.Rotation = Rotation;
        }
      }

      // Now this is the source of truth, you can change sprite but keep the color, and Sprite class is intact
      private Color _Color; public Color Color
      {
        get => _Color;
        set
        {
          _Color = value;
          _Sprite.Color = value;
        }
      }

      private float _Rotation; public float Rotation
      {
        get => _Rotation;
        set
        {
          _Rotation = value;
          _Sprite.Rotation = value;
        }
      }
    }
    #endregion


    #region What if i extract it in a separate class?
    public class Elevation
    {
      private Sprite _Sprite; public Sprite Sprite
      {
        get => _Sprite;
        set
        {
          _Sprite = value;
          _Sprite.Color = Color;
          _Sprite.Rotation = Rotation;
        }
      }

      private Color _Color; public Color Color
      {
        get => _Color;
        set
        {
          _Color = value;
          _Sprite.Color = value;
        }
      }

      private float _Rotation; public float Rotation
      {
        get => _Rotation;
        set
        {
          _Rotation = value;
          _Sprite.Rotation = value;
        }
      }
    }

    public class Component3
    {
      //Now cringe is hidden inside Elevation
      public Elevation SpriteElevation { get; } = new();

      public Sprite Sprite
      {
        get => SpriteElevation.Sprite;
        set => SpriteElevation.Sprite = value;
      }

      public Color Color
      {
        get => SpriteElevation.Color;
        set => SpriteElevation.Color = value;
      }

      public float Rotation
      {
        get => SpriteElevation.Rotation;
        set => SpriteElevation.Rotation = value;
      }
    }
    #endregion


    #region What if i just pass props carefully
    // this is extra dishonest, it's just unpredictable, you can't set color until there's a sprite
    // And you can't know if color was inherited because prev sprite could've been null
    public class Component4
    {
      private Sprite _Sprite; public Sprite Sprite
      {
        get => _Sprite;
        set
        {
          //just pass the props
          value.Color = _Sprite.Color;
          value.Rotation = _Sprite.Rotation;

          _Sprite = value;
        }
      }

      public Color Color
      {
        get => Sprite.Color;
        set => Sprite.Color = value;
      }

      public float Rotation
      {
        get => Sprite.Rotation;
        set => Sprite.Rotation = value;
      }
    }
    #endregion

    public override void Run()
    {

    }
  }
}