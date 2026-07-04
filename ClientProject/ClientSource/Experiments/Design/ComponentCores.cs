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
  /// Testing Component core concept
  /// Idea is to put all the logic in cores and make outer component just a wrapper with forwarded prop for the user
  /// User is not supposed to access the core
  /// 
  /// Conclusion: dogshit
  /// 1. There's no way to put components in relations without data duplication or conversion
  /// 2. No way to double inherit, Links to each other has to be created as new prop and will stack
  /// 3. this will force all component to be splitted
  /// </summary>
  public class ComponentCores : Experiment
  {
    public class Component
    {
      public class ComponentCore
      {
        protected List<ComponentCore> Children = new();
        public void Append(ComponentCore child) => Children.Add(child);
      }

      public List<Component> Children = new();
      // I can't forward stuff like this
      // having 2 lists is beyond cringe
      // I can't forward any relation between components that should be mirrored in cores
      public void Append(Component child)
      {
        Children.Add(child);
        Core.Append(child.Core);
      }

      public virtual ComponentCore Core { get; } = new();
    }

    public class SquareComponent : Component
    {
      public class ComponentCore : Component.ComponentCore
      {
        public Rectangle Rect { get; set; }
      }

      // This is sneaky, i can't add setters to these props and they actualy created with new keyword
      // And this is the main reason why this whole idea is a failure
      public override ComponentCore Core { get; } = new();

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

      public override ComponentCore Core { get; } = new();

      public event Action OnClick
      {
        add => Core.OnClick += value;
        remove => Core.OnClick -= value;
      }

      public void Click() => Core.Click();
    }

    // Aggregate components can use only forwarded props
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

    // Now i can't make raw core components, they may work but will be isolated from the ecosystem
    public class TriangleComponent : Component.ComponentCore
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

      Mod.Logger.Log(button.Core.GetType());
      foreach (PropertyInfo pi in typeof(Button).GetProperties())
      {
        Mod.Logger.Log($"{pi.PropertyType} {pi.Name}");
      }

    }
  }
}