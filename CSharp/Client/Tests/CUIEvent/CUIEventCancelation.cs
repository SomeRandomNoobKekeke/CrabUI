using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using BaroJunk;
using CrabUI;

namespace CUITest
{
  public class CUIEventTest : UTestPack
  {

    public UListTest OnceTest()
    {
      CUIEvent bebebe = new();

      List<string> output = new();

      bebebe.Once(() => output.Add("123"));
      bebebe.Raise();
      bebebe.Raise();
      bebebe.Raise();

      return new UListTest(output, new string[]
      {
        "123","12321312 "
      });
    }
  }
}