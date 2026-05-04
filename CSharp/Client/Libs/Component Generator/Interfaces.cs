using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace ComponentGenerator
{

  public interface IComponent
  {
    public void InjectModules() { }
    public void InjectParts() { }
    public void InitParts() { }
    public void InitModules() { }
    public void InjectProps() { }

    public void Inject()
    {
      InjectParts();
      InjectModules();
      InjectProps();
      InitParts();
      InitModules();
    }
  }

  public interface IPart { }
  public interface IModule { }
  public interface IEndPart : IPart { }
  public interface IChimeraPart : IEndPart, IModule { }
  public interface IAdapterPart : IEndPart, IModule { }

  public interface IPropContainer { }
  public interface IProp { }

  public static class IComponent_Extensions
  {
    public static void Inject(this IComponent component) => component.Inject();
  }
}
