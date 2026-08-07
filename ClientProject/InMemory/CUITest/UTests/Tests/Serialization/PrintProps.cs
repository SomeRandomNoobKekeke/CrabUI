using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;

namespace CrabUI
{
  public class PrintSerializablePropsTest : CUISerializationTest
  {
    public override void CreateTests()
    {
      CUI.Logger.Log(
        Logger.Wrap.IDictionary(
          CUICore.Reflection.GetComponentInfo(typeof(CUIComponent)).SerializableProps
        )
      );
    }
  }
}