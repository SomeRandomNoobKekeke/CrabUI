using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace ComponentInjector
{
  public interface IPart { }
  public interface IAccess { }
  public interface IModule { }
  public interface IComponent
  {
    public void InjectModules() { }
    public void InjectParts() { }
    public void InitParts() { }
    public void InjectProps() { }

    public void Inject()
    {
      InjectParts();
      InitParts();
      InjectModules();
      InjectProps();
    }
  }

  public interface IPropContainer { }
  public interface IProp { }

  public static class IComponentExtensions
  {
    public static void Inject(this IComponent component) => component.Inject();
    public static IComponent Self(this IComponent component) => component;
  }
}
