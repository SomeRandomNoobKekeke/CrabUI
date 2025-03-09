using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using EventInput;
using System.Windows;

namespace CrabUI
{
  //TODO move all this to defauld styles
  /// <summary>
  /// CUITextBlock adapted for CUIMenu
  /// </summary>
  public class CUIMenuText : CUITextBlock
  {
    public CUIMenuText(string text) : this() => Text = text;
    public CUIMenuText() : base()
    {
      Anchor = CUIAnchor.Center;
      TextScale = 1.0f;
      ZIndex = 100;
      TextColor = Color.Black;
    }
  }


  /// <summary>
  /// Component with a sprite that will notify parent CUIMenu when clicked
  /// </summary>
  public class CUIMenuOption : CUIComponent
  {
    public GUISoundType ClickSound { get; set; } = GUISoundType.Select;
    /// <summary>
    /// This is the Value that will be send to CUIMenu on click, and will be passed to OnSelect event
    /// </summary>
    [CUISerializable] public string Value { get; set; }

    /// <summary>
    /// Normal background color
    /// </summary>
    [CUISerializable]
    public Color BaseColor
    {
      get => (Color)Animations["hover"].StartValue;
      set
      {
        Animations["hover"].StartValue = value;
        Animations["hover"].ApplyValue();
      }
    }

    /// <summary>
    /// Background color when hovered
    /// </summary>
    [CUISerializable]
    public Color HoverColor
    {
      get => (Color)Animations["hover"].EndValue;
      set => Animations["hover"].EndValue = value;
    }


    public CUIMenuOption()
    {
      BackgroundColor = new Color(255, 255, 255, 255);
      Relative = new CUINullRect(0, 0, 1, 1);

      IgnoreTransparent = true;
      Command = "CUIMenuOption select";
      OnMouseDown += (e) =>
      {
        SoundPlayer.PlayUISound(ClickSound);
        DispatchUp(new CUICommand(Command, Value));
      };

      Animations["hover"] = new CUIAnimation()
      {
        StartValue = new Color(255, 255, 255, 255),
        EndValue = new Color(255, 255, 255, 255),
        Duration = 0.1,
        ReverseDuration = 0.3,
        Property = "BackgroundColor",
      };

      OnMouseEnter += (e) => Animations["hover"].Forward();
      OnMouseLeave += (e) => Animations["hover"].Back();
    }
  }

  public class CUIMenu : CUIComponent, IKeyboardSubscriber
  {
    // this allows it to intercept esc key press naturally, 
    // but it also blocks normal hotkey bindings, so idk
    // ----------------- IKeyboardSubscriber -----------------
    public void ReceiveSpecialInput(Keys key) { if (key == Keys.Escape) Close(); }
    public void ReceiveTextInput(char inputChar) => ReceiveTextInput(inputChar.ToString());
    public void ReceiveTextInput(string text) { }
    public void ReceiveCommandInput(char command) { }
    public void ReceiveEditingInput(string text, int start, int length) { }
    public bool Selected { get; set; }
    // ----------------- IKeyboardSubscriber -----------------

    public static void InitStatic() => CUI.OnDispose += () => Menus.Clear();
    /// <summary>
    /// All loaded menus are stored here by Name
    /// </summary>
    public static Dictionary<string, CUIMenu> Menus = new();


    /// <summary>
    /// Initial fade in animation duration, set to 0 to disable
    /// </summary>
    [CUISerializable]
    public double FadeInDuration
    {
      get => Animations["fade"].Duration;
      set => Animations["fade"].Duration = value;
    }

    /// <summary>
    /// Will be used as key for this menu in CUIMenu.Menus
    /// </summary>
    [CUISerializable] public string Name { get; set; }
    /// <summary>
    /// Does nothing, just a prop so you could get author programmatically
    /// </summary>
    [CUISerializable] public string Author { get; set; }

    /// <summary>
    /// If true will act as IKeyboardSubscriber. Don't
    /// </summary>
    [CUISerializable] public bool BlockInput { get; set; }
    /// <summary>
    /// Happens when some CUIMenuOption is clicked, the value of that option is passed to it
    /// </summary>
    public event Action<string> OnSelect;
    /// <summary>
    /// Will add this as a child to host (CUI.Main) and start fadein animation
    /// </summary>
    public void Open(CUIComponent host = null)
    {
      if (Parent != null) return;
      host ??= CUI.Main;
      host.Append(this);

      if (BlockInput) CUI.FocusedComponent = this;

      Animations["fade"].SetToStart();
      Animations["fade"].Forward();
    }

    public void Close() => RemoveSelf();

    public void Toggle(CUIComponent host = null)
    {
      if (Parent != null) Close();
      else Open(host);
    }

    /// <summary>
    /// Loads CUIMenu from a file to CUIMenu.Menus
    /// </summary>
    public static CUIMenu Load(string path)
    {
      CUIMenu menu = CUIComponent.LoadFromFile<CUIMenu>(path);
      if (menu == null) CUI.Warning($"Couldn't load CUIMenu from {path}");
      if (menu?.Name != null) Menus[menu.Name] = menu;
      return menu;
    }

    public CUIMenu() : base()
    {
      BackgroundColor = new Color(255, 255, 255, 255);
      Anchor = CUIAnchor.Center;
      Transparency = 0.0f;

      AddCommand("CUIMenuOption select", (o) =>
      {
        if (o is string s) OnSelect?.Invoke(s);
        //Close();
      });

      Animations["fade"] = new CUIAnimation()
      {
        StartValue = 0.0f,
        EndValue = 1.0f,
        Duration = 0.2,
        Property = "Transparency",
      };

      if (CUI.Main != null)
      {
        CUI.Main.Global.OnKeyDown += (e) =>
        {
          if (e.PressedKeys.Contains(Keys.Escape)) Close();
        };

        CUI.Main.OnMouseDown += (e) => Close();
      }
    }

  }
}