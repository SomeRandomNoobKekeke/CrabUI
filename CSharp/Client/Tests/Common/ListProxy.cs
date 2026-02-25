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


    public UListTest Match()
    {
      List<B> list = Enumerable.Range(0, 10).Select(i => new B(i)).ToList();

      ListProxy<A> proxy = new ListProxy<A>(list);

      return new UListTest(list, proxy);
    }

    public UTest Performance()
    {
      List<B> list = new List<B>() { new B(123) };
      ListProxy<A> proxy = new ListProxy<A>(list);
      IReadOnlyList<A> lambdaProxy = new ListProxy<B, A>(list, b => b as A);

      B b = null;
      A a = null;
      int repeats = 100000;

      Stopwatch sw = new Stopwatch();




      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        b = list[0];
      }
      sw.Stop();
      long directAccess = sw.ElapsedTicks;

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        a = (A)list[0];
      }
      sw.Stop();
      long castAccess = sw.ElapsedTicks;

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        a = proxy[0];
      }
      sw.Stop();
      long proxyAccess = sw.ElapsedTicks;

      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        a = lambdaProxy[0];
      }
      sw.Stop();
      long lambdaProxyAccess = sw.ElapsedTicks;


      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        a = new ListProxy<A>(list)[0];
      }
      sw.Stop();
      long newProxyAccess = sw.ElapsedTicks;

      return new UTest(false, true, $"directAccess: [{directAccess}] castAccess: [{castAccess}] proxyAccess: [{proxyAccess}] lambdaProxyAccess: [{lambdaProxyAccess}] newProxyAccess: [{newProxyAccess}]");
    }

  }
}