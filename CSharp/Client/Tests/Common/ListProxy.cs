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
  public class ListProxyTest : UTestPack
  {
    public class A
    {
      public int Value;

      public A(int i) => Value = i;
      public override string ToString() => $"A[{Value}]";
    }

    public class B : A
    {
      public B(int i) : base(i) { }
      public override string ToString() => $"B[{Value}]";
    }

    public override void CreateTests()
    {
      List<B> list = Enumerable.Range(0, 10).Select(i => new B(i)).ToList();

      ListProxy<A> Proxy = new ListProxy<A>(list);

      for (int i = 0; i < list.Count; i++)
      {
        Tests.Add(new UTest(list[i], Proxy[i]));
      }
    }
  }
}