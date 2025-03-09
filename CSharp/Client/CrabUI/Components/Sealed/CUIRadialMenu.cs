using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CrabUI
{
  /// <summary>
  /// Unfinished crap, don't use
  /// </summary>
  public class CUIRadialMenuOption : CUIComponent
  {
    public GUISoundType ClickSound { get; set; } = GUISoundType.Select;

    public Color BaseColor
    {
      get => (Color)this.Animations["hover"].StartValue;
      set => this.Animations["hover"].StartValue = value;
    }

    public Color Hover
    {
      get => (Color)this.Animations["hover"].EndValue;
      set => this.Animations["hover"].EndValue = value;
    }

    public float TextRadius { get; set; } = 0.4f;

    public void SetRotation(float angle)
    {
      BackgroundSprite.Offset = new Vector2(0.5f, 0.5f);
      BackgroundSprite.Rotation = angle;


      this["Text"].Relative = new CUINullRect(
        (float)(TextRadius * Math.Cos(angle - Math.PI / 2)),
        (float)(TextRadius * Math.Sin(angle - Math.PI / 2))
      );
    }

    public Action Callback;

    public CUIRadialMenuOption(string name = "", Action callback = null)
    {
      IgnoreTransparent = true;
      Relative = new CUINullRect(0, 0, 1, 1);

      Callback = callback;
      OnMouseDown += (e) =>
      {
        SoundPlayer.PlayUISound(ClickSound);
        Callback?.Invoke();
      };

      this.Animations["hover"] = new CUIAnimation()
      {
        StartValue = new Color(255, 255, 255, 255),
        EndValue = new Color(0, 255, 255, 255),
        Property = "BackgroundColor",
        Duration = 0.2,
        ReverseDuration = 0.3,
      };

      this.Animations["hover"].ApplyValue();

      OnMouseEnter += (e) => Animations["hover"].Forward();
      OnMouseLeave += (e) => Animations["hover"].Back();

      this["Text"] = new CUITextBlock(name)
      {
        Anchor = CUIAnchor.Center,
        ZIndex = 100,
        TextScale = 1.0f,
      };
    }
  }


  /// <summary>
  /// Unfinished crap, don't use
  /// </summary>
  public class CUIRadialMenu : CUIComponent
  {
    public CUIRadialMenuOption OptionTemplate = new();

    public Dictionary<string, CUIRadialMenuOption> Options = new();

    public CUIRadialMenuOption AddOption(string name, Action callback)
    {
      CUIRadialMenuOption option = new CUIRadialMenuOption(name, callback);
      option.ApplyState(OptionTemplate);
      option.Animations["hover"].Interpolate = OptionTemplate.Animations["hover"].Interpolate;
      option.Animations["hover"].ApplyValue();

      this[name] = Options[name] = option;
      option.OnClick += (e) => Close();

      CalculateRotations();

      return option;
    }

    public void CalculateRotations()
    {
      float delta = (float)(Math.PI * 2 / Options.Count);

      int i = 0;
      foreach (CUIRadialMenuOption option in Options.Values)
      {
        option.SetRotation(delta * i);
        i++;
      }
    }

    public bool IsOpened => Parent != null;

    public void Open(CUIComponent host = null)
    {
      host ??= CUI.Main;
      host.Append(this);
      Animations["fade"].SetToStart();
      Animations["fade"].Forward();
    }

    public void Close()
    {
      // BlockChildrenAnimations();
      // Animations["fade"].SetToEnd();
      // Animations["fade"].Back();
      RemoveSelf();
    }


    public CUIRadialMenu() : base()
    {
      Anchor = CUIAnchor.Center;
      Relative = new CUINullRect(h: 0.8f);
      CrossRelative = new CUINullRect(w: 0.8f);
      BackgroundColor = new Color(255, 255, 255, 255);
      //BackgroundSprite = new CUISprite("RadialMenu.png");

      Animations["fade"] = new CUIAnimation()
      {
        StartValue = 0.0f,
        EndValue = 1.0f,
        Property = "Transparency",
        Duration = 0.2,
        ReverseDuration = 0.1,
      };

      //HACK
      Animations["fade"].OnStop += (dir) =>
      {
        if (dir == CUIDirection.Reverse)
        {
          RemoveSelf();
        }
      };

    }

  }
}