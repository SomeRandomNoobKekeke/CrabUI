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
  /// So i was a bit conserned if 10000 if(componend is IDrawable) checks will affect performance
  /// But it's still faster than running empty functions, so it's not a problem
  /// And it's easy to refactor, also i has nothing to do with the tree
  /// </summary>
  public class StructuralTreeRefactor : Experiment
  {
    // before refactor

    public interface IDrawable
    {
      public void Draw();
    }

    public interface IPlaceable
    {
      public void Place();
    }

    public interface IMouseEventConsumer
    {
      public void Consume();
    }

    public class StructuralComponent
    {
      public List<StructuralComponent> Children = new();

      public IEnumerable<StructuralComponent> DeepChildren
      {
        get
        {
          foreach (StructuralComponent child in Children)
          {
            yield return child;

            foreach (StructuralComponent deepChild in child.DeepChildren)
            {
              yield return deepChild;
            }
          }
        }
      }
    }

    public class Component : StructuralComponent, IDrawable, IPlaceable, IMouseEventConsumer
    {
      public string Name { get; set; }

      public void Draw() { Mod.Logger.Log($"{this}.Draw"); }
      public void Place() { Mod.Logger.Log($"{this}.Place"); }
      public void Consume() { Mod.Logger.Log($"{this}.Consume"); }

      public Component(string name) => Name = name;
      public override string ToString() => Name;
    }


    //After refactor
    public class StructuralComponent2 : IDrawable, IPlaceable, IMouseEventConsumer
    {
      public List<StructuralComponent> Children = new();

      public void Draw() { Mod.Logger.Log($"{this}.Draw"); }
      public void Place() { Mod.Logger.Log($"{this}.Place"); }
      public void Consume() { Mod.Logger.Log($"{this}.Consume"); }

      public IEnumerable<StructuralComponent> DeepChildren
      {
        get
        {
          foreach (StructuralComponent child in Children)
          {
            yield return child;

            foreach (StructuralComponent deepChild in child.DeepChildren)
            {
              yield return deepChild;
            }
          }
        }
      }
    }


    public class MasterComponent : StructuralComponent
    {
      public void Update()
      {
        foreach (StructuralComponent child in DeepChildren)
        {
          if (child is IDrawable drawable) drawable.Draw();
          if (child is IPlaceable placeable) placeable.Place();
          if (child is IMouseEventConsumer consumer) consumer.Consume();
        }
      }
    }


    public override void Run()
    {
      MasterComponent Master = new MasterComponent();
      Component componentA = new("componentA");
      Component componentB = new("componentB");
      Component componentC = new("componentC");



      Master.Children.Add(componentA);
      componentA.Children.Add(componentB);
      componentB.Children.Add(componentC);

      Master.Update();
    }
  }
}