using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;
using CursedUI;
using System.Diagnostics;

namespace CUITests
{
  public class TypeTreeTest : UTestPack
  {
    public class A { }
    public class B1 : A { }
    public class B2 : A { }

    public class C1 : B1 { }
    public class C2 : B1 { }
    public class C3 : B2 { }

    public List<UTest> Usage()
    {
      TypeTree tree = new();
      tree.Add([typeof(A), typeof(B1), typeof(B2), typeof(C1), typeof(C2), typeof(C3)]);

      return new List<UTest>()
      {
        new UListTest(tree.AllTypes, new Type[]
        {
          typeof(A), typeof(B1), typeof(B2), typeof(C1), typeof(C2), typeof(C3)
        }),
        new UListTest(tree.GetDerivedTypes<A>(), new Type[]
        {
          typeof(A), typeof(B1), typeof(C1), typeof(C2), typeof(B2), typeof(C3)
        }),
        new UListTest(tree.GetDerivedTypes<B1>(includeSelf:false), new Type[]
        {
          typeof(C1), typeof(C2)
        }),
        new UListTest(tree.GetBaseTypes<C3>(), new Type[]
        {
          typeof(C3), typeof(B2), typeof(A)
        }),
      };
    }

    public List<UTest> ConnectingInDirectOrder()
    {
      TypeTree tree = new();

      tree.Add(typeof(A));
      tree.Add(typeof(B1));
      tree.Add(typeof(B2));
      tree.Add(typeof(C1));
      tree.Add(typeof(C2));
      tree.Add(typeof(C3));

      return new List<UTest>()
      {
        new UListTest(tree.AllTypes, new Type[]
        {
          typeof(A), typeof(B1), typeof(B2), typeof(C1), typeof(C2), typeof(C3)
        }),
        new UListTest(tree.GetDerivedTypes<A>(), new Type[]
        {
          typeof(A), typeof(B1), typeof(C1), typeof(C2), typeof(B2), typeof(C3)
        }),
        new UListTest(tree.GetDerivedTypes<B1>(), new Type[]
        {
          typeof(B1), typeof(C1), typeof(C2)
        }),
        new UListTest(tree.GetBaseTypes<C3>(), new Type[]
        {
          typeof(C3), typeof(B2), typeof(A)
        }),
      };
    }

    public List<UTest> ConnectingOneByOneInReverseOrder()
    {
      TypeTree tree = new();

      tree.Add(typeof(C1));
      tree.Add(typeof(C2));
      tree.Add(typeof(C3));
      tree.Add(typeof(B1));
      tree.Add(typeof(B2));
      tree.Add(typeof(A));

      return new List<UTest>()
      {
        new UListTest(tree.AllTypes, new Type[]
        {
          typeof(C1), typeof(C2), typeof(C3),typeof(B1), typeof(B2), typeof(A),
        }),
        new UListTest(tree.GetDerivedTypes<A>(), new Type[]
        {
          typeof(A), typeof(B1), typeof(C1), typeof(C2), typeof(B2), typeof(C3)
        }),
        new UListTest(tree.GetDerivedTypes<B1>(), new Type[]
        {
          typeof(B1), typeof(C1), typeof(C2)
        }),
        new UListTest(tree.GetBaseTypes<C3>(), new Type[]
        {
          typeof(C3), typeof(B2), typeof(A)
        }),
      };
    }

    public List<UTest> ConnectingInReverseOrder()
    {
      TypeTree tree = new();

      tree.Add([typeof(C1), typeof(C2), typeof(C3)]);
      tree.Add([typeof(B1), typeof(B2)]);
      tree.Add([typeof(A)]);

      return new List<UTest>()
      {
        new UListTest(tree.AllTypes, new Type[]
        {
          typeof(C1), typeof(C2), typeof(C3),typeof(B1), typeof(B2), typeof(A),
        }),
        new UListTest(tree.GetDerivedTypes<A>(), new Type[]
        {
          typeof(A), typeof(B1), typeof(C1), typeof(C2), typeof(B2), typeof(C3)
        }),
        new UListTest(tree.GetDerivedTypes<B1>(), new Type[]
        {
          typeof(B1), typeof(C1), typeof(C2)
        }),
        new UListTest(tree.GetBaseTypes<C3>(), new Type[]
        {
          typeof(C3), typeof(B2), typeof(A)
        }),
      };
    }

    public List<UTest> AddingOneByOneMultipleTimes()
    {
      TypeTree tree = new();

      tree.Add(typeof(A));
      tree.Add(typeof(B1));
      tree.Add(typeof(B2));
      tree.Add(typeof(C1));
      tree.Add(typeof(C2));
      tree.Add(typeof(C3));

      tree.Add(typeof(C1));
      // tree.Add(typeof(C2));
      tree.Add(typeof(C3));
      // tree.Add(typeof(B1));
      tree.Add(typeof(B2));
      // tree.Add(typeof(A));


      // tree.Add([typeof(C1), typeof(C2), typeof(C3)]);
      // tree.Add([typeof(B1), typeof(B2)]);
      // tree.Add([typeof(A)]);

      return new List<UTest>()
      {
        new UListTest(tree.AllTypes, new Type[]
        {
          typeof(A), typeof(B1), typeof(B2), typeof(C1), typeof(C2), typeof(C3)
        }),
        new UListTest(tree.GetDerivedTypes<A>(), new Type[]
        {
          typeof(A), typeof(B1), typeof(C1), typeof(C2), typeof(B2), typeof(C3)
        }),
        new UListTest(tree.GetDerivedTypes<B1>(), new Type[]
        {
          typeof(B1), typeof(C1), typeof(C2)
        }),
        new UListTest(tree.GetBaseTypes<C3>(), new Type[]
        {
          typeof(C3), typeof(B2), typeof(A)
        }),
      };
    }

    public List<UTest> AddingMultipleTimes()
    {
      TypeTree tree = new();

      tree.Add([typeof(A), typeof(B1), typeof(B2), typeof(C1), typeof(C2), typeof(C3)]);
      tree.Add([typeof(C3), typeof(C1), typeof(B2),]);

      return new List<UTest>()
      {
        new UListTest(tree.AllTypes, new Type[]
        {
          typeof(A), typeof(B1), typeof(B2), typeof(C1), typeof(C2), typeof(C3)
        }),
        new UListTest(tree.GetDerivedTypes<A>(), new Type[]
        {
          typeof(A), typeof(B1), typeof(C1), typeof(C2), typeof(B2), typeof(C3)
        }),
        new UListTest(tree.GetDerivedTypes<B1>(), new Type[]
        {
          typeof(B1), typeof(C1), typeof(C2)
        }),
        new UListTest(tree.GetBaseTypes<C3>(), new Type[]
        {
          typeof(C3), typeof(B2), typeof(A)
        }),
      };
    }

    public List<UTest> Performance()
    {
      TypeTree tree = new();
      Stopwatch sw = new Stopwatch();

      #region dryrun
      tree.Clear();
      tree.AllowConnectingInReverseOrder = true;
      sw.Restart();
      tree.Add([typeof(A), typeof(B1), typeof(B2), typeof(C1), typeof(C2), typeof(C3)]);
      sw.Stop();

      tree.Clear();
      tree.AllowConnectingInReverseOrder = false;
      sw.Restart();
      tree.Add([typeof(A), typeof(B1), typeof(B2), typeof(C1), typeof(C2), typeof(C3)]);
      sw.Stop();

      tree.Clear();
      tree.AllowConnectingInReverseOrder = true;
      sw.Restart();
      tree.Add(typeof(C1));
      tree.Add(typeof(C2));
      tree.Add(typeof(C3));
      tree.Add(typeof(B1));
      tree.Add(typeof(B2));
      tree.Add(typeof(A));
      sw.Stop();

      tree.Clear();
      tree.AllowConnectingInReverseOrder = false;
      sw.Restart();
      tree.Add(typeof(C1));
      tree.Add(typeof(C2));
      tree.Add(typeof(C3));
      tree.Add(typeof(B1));
      tree.Add(typeof(B2));
      tree.Add(typeof(A));
      sw.Stop();
      #endregion

      long BulkConnectIn2Directions = 0;
      long BulkConnectIn1Directions = 0;
      long OneByOneConnectIn2Directions = 0;
      long OneByOneConnectIn1Directions = 0;

      Logger.Default.Log("Try 2");
      tree.Clear();
      tree.AllowConnectingInReverseOrder = true;
      sw.Restart();
      tree.Add([typeof(A), typeof(B1), typeof(B2), typeof(C1), typeof(C2), typeof(C3)]);
      sw.Stop();
      BulkConnectIn2Directions = sw.ElapsedTicks;

      tree.Clear();
      tree.AllowConnectingInReverseOrder = false;
      sw.Restart();
      tree.Add([typeof(A), typeof(B1), typeof(B2), typeof(C1), typeof(C2), typeof(C3)]);
      sw.Stop();
      BulkConnectIn1Directions = sw.ElapsedTicks;

      tree.Clear();
      tree.AllowConnectingInReverseOrder = true;
      sw.Restart();
      tree.Add(typeof(C1));
      tree.Add(typeof(C2));
      tree.Add(typeof(C3));
      tree.Add(typeof(B1));
      tree.Add(typeof(B2));
      tree.Add(typeof(A));
      sw.Stop();
      OneByOneConnectIn2Directions = sw.ElapsedTicks;

      tree.Clear();
      tree.AllowConnectingInReverseOrder = false;
      sw.Restart();
      tree.Add(typeof(C1));
      tree.Add(typeof(C2));
      tree.Add(typeof(C3));
      tree.Add(typeof(B1));
      tree.Add(typeof(B2));
      tree.Add(typeof(A));
      sw.Stop();
      OneByOneConnectIn1Directions = sw.ElapsedTicks;

      return new List<UTest>()
      {
        new UTest(true, $"BulkConnectIn2Directions: [{BulkConnectIn2Directions}]"),
        new UTest(true, $"BulkConnectIn1Directions: [{BulkConnectIn1Directions}]"),
        new UTest(true, $"OneByOneConnectIn2Directions: [{OneByOneConnectIn2Directions}]"),
        new UTest(true, $"OneByOneConnectIn1Directions: [{OneByOneConnectIn1Directions}]"),
      };
    }
  }
}