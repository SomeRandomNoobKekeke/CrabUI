using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CursedUIUser
{

  /// <summary>
  /// Testing how hard it is to refactor raw components into cores
  /// Backward refactor is ctrl+x ctrl+v
  /// Forward refactor would be trivial if there was a macros for prop mapping
  /// Also it can be done on one component at a time 
  /// </summary>
  public class ComponentCoresRefactor : Experiment
  {
    public class Component
    {

    }

    public class SquareComponent
    {
      public class ComponentCore : Component
      {
        public Rectangle Rect { get; set; }
      }

      protected virtual ComponentCore Core { get; } = new();

      public Rectangle Rect
      {
        get => Core.Rect;
        set => Core.Rect = value;
      }
    }



    public class Button : SquareComponent
    {
      public class ComponentCore : SquareComponent.ComponentCore
      {
        public string Unexposed { get; set; }

        public event Action OnClick;

        public void Click() => OnClick?.Invoke();
      }

      protected override ComponentCore Core { get; } = new();

      // public string Unexposed { get; set; }

      public event Action OnClick
      {
        add => Core.OnClick += value;
        remove => Core.OnClick -= value;
      }

      public void Click() => Core.Click();
    }

    public class ButtonList : Component
    {
      public List<Button> Buttons = new();

      public void AddButton(Button button) => Buttons.Add(button);

      public void ClickAll()
      {
        foreach (Button button in Buttons)
        {
          button.Click();
        }
      }
    }

    // Works now
    public class TriangleComponent : Component
    {
      public Point A { get; set; }
      public Point B { get; set; }
      public Point C { get; set; }
    }


    public override void Run()
    {
      Button button = new Button();
      button.Rect = new Rectangle(0, 0, 100, 100);
      button.OnClick += () => { };


      // button.Unexposed = "ay yay yay";
    }
  }
}