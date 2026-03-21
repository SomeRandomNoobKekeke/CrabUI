using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text;

namespace BaroJunk
{
  public class ReusableTuple<T1>
  {
    public T1 Item1 { get; set; }

    public void Update(T1 item1)
    {
      Item1 = item1;
    }

    public object[] ToArray() => [
      Item1,
    ];

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append('[');
      sb.Append(Item1);
      sb.Append(']');
      return sb.ToString();
    }

    public ReusableTuple() { }
    public ReusableTuple(T1 item1)
    {
      Item1 = item1;
    }
  }

  public class ReusableTuple<T1, T2>
  {
    public T1 Item1 { get; set; }
    public T2 Item2 { get; set; }

    public void Update(T1 item1, T2 item2)
    {
      Item1 = item1;
      Item2 = item2;
    }

    public object[] ToArray() => [
      Item1,
      Item2,
    ];

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append('[');
      sb.Append(Item1);
      sb.Append(", ");
      sb.Append(Item2);
      sb.Append(']');
      return sb.ToString();
    }

    public ReusableTuple() { }
    public ReusableTuple(T1 item1, T2 item2)
    {
      Item1 = item1;
      Item2 = item2;
    }
  }

  public class ReusableTuple<T1, T2, T3>
  {
    public T1 Item1 { get; set; }
    public T2 Item2 { get; set; }
    public T3 Item3 { get; set; }

    public void Update(T1 item1, T2 item2, T3 item3)
    {
      Item1 = item1;
      Item2 = item2;
      Item3 = item3;
    }

    public object[] ToArray() => [
      Item1,
      Item2,
      Item3,
    ];

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append('[');
      sb.Append(Item1);
      sb.Append(", ");
      sb.Append(Item2);
      sb.Append(", ");
      sb.Append(Item3);
      sb.Append(']');
      return sb.ToString();
    }

    public ReusableTuple() { }
    public ReusableTuple(T1 item1, T2 item2, T3 item3)
    {
      Item1 = item1;
      Item2 = item2;
      Item3 = item3;
    }
  }

  public class ReusableTuple<T1, T2, T3, T4>
  {
    public T1 Item1 { get; set; }
    public T2 Item2 { get; set; }
    public T3 Item3 { get; set; }
    public T4 Item4 { get; set; }

    public void Update(T1 item1, T2 item2, T3 item3, T4 item4)
    {
      Item1 = item1;
      Item2 = item2;
      Item3 = item3;
      Item4 = item4;
    }

    public object[] ToArray() => [
      Item1,
      Item2,
      Item3,
      Item4,
    ];

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append('[');
      sb.Append(Item1);
      sb.Append(", ");
      sb.Append(Item2);
      sb.Append(", ");
      sb.Append(Item3);
      sb.Append(", ");
      sb.Append(Item4);
      sb.Append(']');
      return sb.ToString();
    }

    public ReusableTuple() { }
    public ReusableTuple(T1 item1, T2 item2, T3 item3, T4 item4)
    {
      Item1 = item1;
      Item2 = item2;
      Item3 = item3;
      Item4 = item4;
    }
  }

  public class ReusableTuple<T1, T2, T3, T4, T5>
  {
    public T1 Item1 { get; set; }
    public T2 Item2 { get; set; }
    public T3 Item3 { get; set; }
    public T4 Item4 { get; set; }
    public T5 Item5 { get; set; }

    public void Update(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    {
      Item1 = item1;
      Item2 = item2;
      Item3 = item3;
      Item4 = item4;
      Item5 = item5;
    }

    public object[] ToArray() => [
      Item1,
      Item2,
      Item3,
      Item4,
      Item5,
    ];

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append('[');
      sb.Append(Item1);
      sb.Append(", ");
      sb.Append(Item2);
      sb.Append(", ");
      sb.Append(Item3);
      sb.Append(", ");
      sb.Append(Item4);
      sb.Append(", ");
      sb.Append(Item5);
      sb.Append(']');
      return sb.ToString();
    }

    public ReusableTuple() { }
    public ReusableTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    {
      Item1 = item1;
      Item2 = item2;
      Item3 = item3;
      Item4 = item4;
      Item5 = item5;
    }
  }

  public class ReusableTuple<T1, T2, T3, T4, T5, T6>
  {
    public T1 Item1 { get; set; }
    public T2 Item2 { get; set; }
    public T3 Item3 { get; set; }
    public T4 Item4 { get; set; }
    public T5 Item5 { get; set; }
    public T6 Item6 { get; set; }

    public void Update(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
    {
      Item1 = item1;
      Item2 = item2;
      Item3 = item3;
      Item4 = item4;
      Item5 = item5;
      Item6 = item6;
    }

    public object[] ToArray() => [
      Item1,
      Item2,
      Item3,
      Item4,
      Item5,
      Item6,
    ];

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append('[');
      sb.Append(Item1);
      sb.Append(", ");
      sb.Append(Item2);
      sb.Append(", ");
      sb.Append(Item3);
      sb.Append(", ");
      sb.Append(Item4);
      sb.Append(", ");
      sb.Append(Item5);
      sb.Append(", ");
      sb.Append(Item6);
      sb.Append(']');
      return sb.ToString();
    }

    public ReusableTuple() { }
    public ReusableTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
    {
      Item1 = item1;
      Item2 = item2;
      Item3 = item3;
      Item4 = item4;
      Item5 = item5;
      Item6 = item6;
    }
  }

  public class ReusableTuple<T1, T2, T3, T4, T5, T6, T7>
  {
    public T1 Item1 { get; set; }
    public T2 Item2 { get; set; }
    public T3 Item3 { get; set; }
    public T4 Item4 { get; set; }
    public T5 Item5 { get; set; }
    public T6 Item6 { get; set; }
    public T7 Item7 { get; set; }

    public void Update(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
    {
      Item1 = item1;
      Item2 = item2;
      Item3 = item3;
      Item4 = item4;
      Item5 = item5;
      Item6 = item6;
      Item7 = item7;
    }

    public object[] ToArray() => [
      Item1,
      Item2,
      Item3,
      Item4,
      Item5,
      Item6,
      Item7,
    ];

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append('[');
      sb.Append(Item1);
      sb.Append(", ");
      sb.Append(Item2);
      sb.Append(", ");
      sb.Append(Item3);
      sb.Append(", ");
      sb.Append(Item4);
      sb.Append(", ");
      sb.Append(Item5);
      sb.Append(", ");
      sb.Append(Item6);
      sb.Append(", ");
      sb.Append(Item7);
      sb.Append(']');
      return sb.ToString();
    }

    public ReusableTuple() { }
    public ReusableTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
    {
      Item1 = item1;
      Item2 = item2;
      Item3 = item3;
      Item4 = item4;
      Item5 = item5;
      Item6 = item6;
      Item7 = item7;
    }
  }
}