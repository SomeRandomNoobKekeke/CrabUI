using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;
using BaroJunk;
using System.IO;
using System.Text.Json;


namespace CrabUI
{

  public class CAComponentModel
  {
    public Type RootType { get; }


    public Dictionary<Type, Dictionary<string, CAPropertyModel>> PublicGet { get; } = new();
    public Dictionary<Type, Dictionary<string, CAPropertyModel>> ProtectedGet { get; } = new();

    private void Analyze()
    {
      FillRec(PublicGet, typeModel => typeModel.PublicGet);
      FillRec(ProtectedGet, typeModel => typeModel.ProtectedGet);
    }



    private void FillRec(
      Dictionary<Type, Dictionary<string, CAPropertyModel>> props,
      Func<CATypeModel, List<PropertyInfo>> getTargetProps
    )
    {
      void HandleCollision(CAPropertyModel propertyModel)
      {
        if (props[propertyModel.Type][propertyModel.Name].Property == propertyModel.Property)
        {
          Logger.Default.Warning($"Component prop loop: [{propertyModel.StringPath}]");
        }
        else
        {
          Logger.Default.Warning($"Component prop collision: [{props[propertyModel.Type][propertyModel.Name].StringPath}] [{propertyModel.StringPath}]");
        }
      }

      void HandleDuplicateModule(CAPropertyModel propertyModel)
      {
        Logger.Default.Warning($"Component duplicate module: [{propertyModel.Type.n} {propertyModel.StringPath}]");
      }

      bool AlreadyExist(CAPropertyModel propertyModel)
      {
        return props.ContainsKey(propertyModel.Type) && props[propertyModel.Type].ContainsKey(propertyModel.Name);
      }

      void Add(CAPropertyModel propertyModel)
      {
        if (!props.ContainsKey(propertyModel.Type)) props[propertyModel.Type] = new();
        props[propertyModel.Type][propertyModel.Name] = propertyModel;
      }

      bool ShouldBeDugInto(Type T) => T.IsAssignableTo(typeof(IModuleContainer)) || T.IsAssignableTo(typeof(IModule));




      void GoDeeper(Type container, IEnumerable<PropertyInfo> path)
      {
        BreakTheLoop.After(100);

        CATypeModel typeModel = CATypeModel.For(container);

        foreach (PropertyInfo pi in getTargetProps(typeModel))
        {
          CAPropertyModel propertyModel = new CAPropertyModel(pi, path);



          if (AlreadyExist(propertyModel))
          {
            HandleCollision(propertyModel);
          }
          else
          {
            if (ShouldBeDugInto(propertyModel.Type))
            {

              GoDeeper(propertyModel.Type, path.Append(pi));
            }
            else
            {
              Add(propertyModel);
            }
          }
        }
      }

      GoDeeper(RootType, new List<PropertyInfo>());
    }


    public CAComponentModel(Type componentType)
    {
      RootType = componentType;
      Analyze();
    }

    public override string ToString() => $"Public : {Logger.Wrap.IDictionary(
      PublicGet.ToDictionary(
        kvp => kvp.Key,
        kvp => Logger.Wrap.IEnumerable(kvp.Value.Keys)
      )
    )}\nProtected: {Logger.Wrap.IDictionary(
      ProtectedGet.ToDictionary(
        kvp => kvp.Key,
        kvp => Logger.Wrap.IEnumerable(kvp.Value.Keys)
      )
    )}";
  }
}