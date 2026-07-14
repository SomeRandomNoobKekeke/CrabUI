using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using BaroJunk;

namespace CrabUI
{
  public class PrintSerializablePropsTest : UTestPack
  {
    public override void CreateTests()
    {
      CUI.Logger.Log(
        Logger.Wrap.IDictionary(
          CUICore.CUITypes.SerializableTypes[typeof(CUIComponent)].SerializableProps
        )
      );
    }
  }
}