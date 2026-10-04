using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;
using CursedUI;

namespace CursedUIUser
{
  public class RoutableCommandNodeTest : UTestPack
  {

    public class Layer1 : CUIComponent
    {
      public const string CreateNewObject = "create new object";
      public const string ObjectCreated = "object created";

      public override RoutableCommandContract? CommandsContract { get; } = new()
      {
        Consumes = [CreateNewObject],
        SendsDown = [ObjectCreated],
      };
    }

    public class Layer2 : CUIComponent
    {
      public override RoutableCommandContract? CommandsContract { get; } = new()
      {
        Consumes = ["confirm", "cancel", Layer1.ObjectCreated],
        SendsUp = [Layer1.CreateNewObject],
      };
    }

    public class Layer3 : CUIComponent
    {
      public override RoutableCommandContract? CommandsContract { get; } = new()
      {
        SendsUp = ["confirm", "cancel", "uncaught"],
      };
    }

    public UTest SendUp()
    {
      CUIComponent a = new();
      CUIComponent b = a["b"] = new CUIComponent();
      CUIComponent c = b["c"] = new CUIComponent();

      a.Commands.ListenFor<string>("msg", (s) => a.Data["msg"] = s);
      c.Commands.SendUp("msg", "123");

      return new UTest(a.Data["msg"], "123");
    }

    public UTest SendDown()
    {
      CUIComponent a = new();
      CUIComponent b = a["b"] = new CUIComponent();
      CUIComponent c = b["c"] = new CUIComponent();

      c.Commands.ListenFor<string>("msg", (s) => c.Data["msg"] = s);
      a.Commands.SendDown("msg", "321");

      return new UTest(c.Data["msg"], "321");
    }

    public List<UTest> Contracts()
    {
      Layer1 layer1 = new();
      Layer2 layer2 = new();
      Layer3 layer3 = new();

      layer1["next"] = layer2;
      layer2["next"] = layer3;

      layer1.Commands.ListenFor(Layer1.CreateNewObject, () =>
      {
        layer1.Data["object"] = new object();
        layer1.Commands.SendDown(Layer1.ObjectCreated, layer1.Data["object"]);
      });

      layer2.Commands.ListenFor<object>(Layer1.ObjectCreated, (o) =>
      {
        layer2.Data["object"] = o;
      });

      layer2.Commands.ListenFor("confirm", () => layer2.Commands.SendUp(Layer1.CreateNewObject));

      layer3.Commands.SendUp("confirm");

      return new List<UTest>()
      {
        new UThrowTest(()=>layer3.Commands.SendUp("cringe"), new ContractBrokenException()),
        new UThrowTest(()=>layer3.Commands.SendUp("uncaught"), new ContractBrokenException()),
        new UThrowTest(()=>layer1.Commands.SendDown("object deleted"), new ContractBrokenException()),
        new UThrowTest(()=>layer1.Commands.ListenFor("confirm",()=>{}), new ContractBrokenException()),
        new UTest(layer1.Data["object"], layer2.Data["object"]),
      };
    }


  }
}