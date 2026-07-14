using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using BaroJunk;
namespace CUICodeGenerator
{

  public interface IComponent
  {
    public void InjectModules() { }
    public void InjectParts() { }
    public void RunInitMethods() { }
    public void InitParts() { }
    public void InitModules() { }
    public void InjectProps() { }
    public void NotifyAwareObjects() { }

    public void Inject()
    {
      try
      {
        InjectParts();
        InjectModules();
        InjectProps();
        NotifyAwareObjects();
        RunInitMethods();
        InitParts();
        InitModules();
      }
      catch (Exception e)
      {
        Logger.Default.Error($"CG| failed to inject [{this}]\n{e}");
      }
    }
  }

  public interface IPart { }
  public interface IModule { }
  public interface IEndPart : IPart { }
  public interface IChimeraPart : IEndPart, IModule { }
  public interface IAdapterPart : IEndPart, IModule { }

  public interface IPropContainer { }
  public interface IProp { }

  public interface IAware
  {
    public object HostComponent { get; set; }
    public string HostPropName { get; set; }
  }

  public static class IComponent_Extensions
  {
    public static void Inject(this IComponent component) => component.Inject();
  }
}
