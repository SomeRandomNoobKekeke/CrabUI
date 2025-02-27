using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Just a tick box
  /// </summary>
  public class CUITickBox : CUIComponent
  {
    public GUISoundType ClickSound { get; set; } = GUISoundType.TickBox;

    public event Action<bool> OnStateChange;
    public void AddOnStateChange(Action<bool> callback) => OnStateChange += callback;

    public CUISprite OnSprite { get; set; }
    public CUISprite OffSprite { get; set; }
    public CUISprite HoverOffSprite { get; set; }
    public CUISprite HoverOnSprite { get; set; }
    public CUISprite DisabledSprite { get; set; }

    private bool state; public bool State
    {
      get => state;
      set
      {
        if (state == value) return;
        state = value;
        ChangeSprite();
      }
    }

    public override bool Disabled
    {
      get => disabled;
      set
      {
        disabled = value;
        ChangeSprite();
      }
    }

    public virtual void ChangeSprite()
    {
      if (Disabled)
      {
        BackgroundSprite = DisabledSprite;
        return;
      }

      if (State)
      {
        BackgroundSprite = OnSprite;
        if (MouseOver) BackgroundSprite = HoverOnSprite;
      }
      else
      {
        BackgroundSprite = OffSprite;
        if (MouseOver) BackgroundSprite = HoverOffSprite;
      }
    }


    public CUITickBox() : base()
    {
      Absolute = new CUINullRect(w: 20, h: 20);
      BackgroundColor = Color.Cyan;
      Border.Color = Color.Transparent;
      ConsumeMouseClicks = true;
      ConsumeDragAndDrop = true;
      ConsumeSwipe = true;


      OffSprite = new CUISprite(CUI.CUITexturePath)
      {
        SourceRect = new Rectangle(0, 0, 32, 32),
      };

      OnSprite = new CUISprite(CUI.CUITexturePath)
      {
        SourceRect = new Rectangle(32, 0, 32, 32),
      };

      HoverOffSprite = new CUISprite(CUI.CUITexturePath)
      {
        SourceRect = new Rectangle(64, 0, 32, 32),
      };
      HoverOnSprite = new CUISprite(CUI.CUITexturePath)
      {
        SourceRect = new Rectangle(96, 0, 32, 32),
      };

      DisabledSprite = new CUISprite(CUI.CUITexturePath)
      {
        SourceRect = new Rectangle(128, 0, 32, 32),
      };

      ChangeSprite();

      OnMouseDown += (e) =>
      {
        if (Disabled) return;

        SoundPlayer.PlayUISound(ClickSound);
        State = !State;
        OnStateChange?.Invoke(State);
        if (Command != null) DispatchUp(new CUICommand(Command, State));
      };

      OnMouseEnter += (e) => ChangeSprite();
      OnMouseLeave += (e) => ChangeSprite();

      OnConsume += (o) =>
      {
        if (o is bool b) State = b;
      };
    }
  }

}