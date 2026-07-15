using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using Microsoft.Xna.Framework;

using System.Diagnostics;

namespace CrabUIUser
{
  public class ProxyPerformance : Experiment
  {

    public class A
    {
      public int Value { get; set; }

      public void UseB(B b)
      {
        Value = b.Value;
      }
    }

    public class B
    {
      public int Value { get; set; } = 123;
    }

    // Proxies
    // Version 1, direct proxy,  useless
    public class AProxy1
    {
      public A A { get; set; }

      public void UseBProxy(BProxy1 b)
      {
        A.Value = b.B.Value;
      }

      public AProxy1(A a) => A = a; // Aaa
    }

    public class BProxy1
    {
      public B B { get; set; }

      public BProxy1(B b) => B = b;
    }


    // Version 2, hidden behind interface, need to use "is" operator to extract the value
    public interface IAProxy2
    {
      public void UseBProxy(IBProxy2 b);
    }
    public interface IBProxy2 { }

    public class AProxy2 : IAProxy2
    {
      public A A { get; set; }

      public void UseBProxy(IBProxy2 b)
      {
        if (b is BProxy2 proxy2)
        {
          A.Value = proxy2.B.Value;
        }
      }

      public AProxy2(A a) => A = a; // Aaa
    }

    public class BProxy2 : IBProxy2
    {
      public B B { get; set; }

      public BProxy2(B b) => B = b;
    }


    // Version 3, polymorphism without interfaces
    public class internalAProxy3
    {
      public A A { get; set; }
      public void UseBProxy(internalBProxy3 b) { A.Value = b.B.Value; }
      public internalAProxy3(A a) => A = a; // Aaa
    }

    public class internalBProxy3
    {
      public B B { get; set; }
      public internalBProxy3(B b) => B = b;
    }

    public class FakeInternalAProxy3
    {
      public A A { get; set; }
      public void UseBProxy(FakeInternalBProxy3 b) { A.Value = b.B.Value; }
      public FakeInternalAProxy3(A a) => A = a; // Aaa
    }

    public class FakeInternalBProxy3
    {
      public B B { get; set; }
      public FakeInternalBProxy3(B b) => B = b;
    }

    // The idea is that i can change base class if i want deeper debug
    public class AProxy3
#if WeNeedToGoDeeper
    : FakeInternalAProxy3
#else
    : internalAProxy3
#endif
    {
      public AProxy3(A a) : base(a) { }
    }
    public class BProxy3 : internalBProxy3
    {
      public BProxy3(B b) : base(b) { }
    }


    // Version 4, use interfaces that expose target object
    public interface IAProxy4
    {
      public A A => null;
      public void UseBProxy(IBProxy4 b);
    }
    public interface IBProxy4
    {
      public B B => null;
    }

    public class AProxy4 : IAProxy4
    {
      public A A { get; set; }

      public void UseBProxy(IBProxy4 b)
      {
        if (b.B is not null)
        {
          A.Value = b.B.Value;
        }
      }

      public AProxy4(A a) => A = a; // Aaa
    }

    public class BProxy4 : IBProxy4
    {
      public B B { get; set; }

      public BProxy4(B b) => B = b;
    }



    public int repeats = 100_000_000;

    public override void Run()
    {
      Stopwatch sw = new Stopwatch();

      A a = new A();
      B b = new B();

      int value = 0;
      int valueb = 123;

      AProxy1 aProxy1 = new(a);
      BProxy1 bProxy1 = new(b);

      AProxy2 aProxy2 = new(a);
      BProxy2 bProxy2 = new(b);

      AProxy3 aProxy3 = new(a);
      BProxy3 bProxy3 = new(b);

      AProxy4 aProxy4 = new(a);
      BProxy4 bProxy4 = new(b);

      // base measurement
      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        value = valueb;
      }
      sw.Stop();
      Mod.Logger.Log($"value = valueb;: [{sw.ElapsedMilliseconds}]");

      // This takes exactly the same amount of ms
      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        a.UseB(b);
      }
      sw.Stop();
      Mod.Logger.Log($"a.UseB(b);: [{sw.ElapsedMilliseconds}]");

      // Exactly the same, 0 performance overhead
      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        aProxy1.UseBProxy(bProxy1);
      }
      sw.Stop();
      Mod.Logger.Log($"aProxy1.UseBProxy(bProxy1): [{sw.ElapsedMilliseconds}]");

      // The only performance overhead is the "is" check
      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        aProxy2.UseBProxy(bProxy2);
      }
      sw.Stop();
      Mod.Logger.Log($"aProxy2.UseBProxy(bProxy2);: [{sw.ElapsedMilliseconds}]");

      // No performance overhead
      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        aProxy3.UseBProxy(bProxy3);
      }
      sw.Stop();
      Mod.Logger.Log($"aProxy3.UseBProxy(bProxy3);: [{sw.ElapsedMilliseconds}]");

      // The only performance overhead is the "is not null" check
      // also "is null" check is slower than "is"
      sw.Restart();
      for (int i = 0; i < repeats; i++)
      {
        aProxy4.UseBProxy(bProxy4);
      }
      sw.Stop();
      Mod.Logger.Log($"aProxy4.UseBProxy(bProxy4);: [{sw.ElapsedMilliseconds}]");
    }
  }
}